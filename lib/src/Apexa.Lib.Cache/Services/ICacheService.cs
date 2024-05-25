using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.Lib.Cache.Services
{
    public interface ICacheService<T> where T : class
    {
        public bool TryGetValue(string key, out T value);

        public void PutValue(string key, T value);

        public void RemoveValue(string key);
    }
}
