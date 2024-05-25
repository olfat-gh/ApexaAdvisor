using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apexa.Lib.Cache.Configuation;
using Apexa.Lib.Cache.Core;
using Microsoft.Extensions.Options;

namespace Apexa.Lib.Cache.Services
{
    public class CacheService<T> : ICacheService<T> where T : class
    {
        private readonly CacheSettings _configuration;
        private readonly Dictionary<string, Node<T>> _table;
        private readonly CacheLinkedList<T> _itemList;
        public CacheService(IOptions<CacheSettings> configuration)
        {
            _configuration = configuration.Value;
            _table = new Dictionary<string, Node<T>>();
            _itemList = new CacheLinkedList<T>();
        }


        public bool TryGetValue(string key, out T value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            if (!_table.ContainsKey(key))
            {
                value = default;
                return false;
            }

            var item = MakeFirstNode(key);
            value = item.Value;

            return true;
        }

        public void PutValue(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            if (_table.ContainsKey(key)) /// update
            {
                var item = MakeFirstNode(key);
                item.Value = value;
            }
            else
            {
                if (_itemList.Count == _configuration.Capacity)
                {
                    var item = _itemList.DeleteItem();  // remove MRU item
                    _table.Remove(item.Key);
                }

                var newItem = new Node<T>(key, value);
                _itemList.AddItem(newItem);
                _table[key] = newItem;

            }
        }

        public void RemoveValue(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
 
            if (_table.ContainsKey(key)) 
            {
                var item = _table[key];
                _itemList.DeleteItem(item);
                _table.Remove(key);
            }
           
        }

        private Node<T> MakeFirstNode(string key)
        {
            var item = _table[key];
            _itemList.DeleteItem(item);
            _itemList.AddItemToFront(item);
            return item;
        }
    }
}
