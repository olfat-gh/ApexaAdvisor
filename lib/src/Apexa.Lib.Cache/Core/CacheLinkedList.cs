using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.Lib.Cache.Core
{
    public class Node<T>
    {
        public string Key { get; set; }
        public T Value { get; set; }
        public Node<T> Next { get; set; } = null;
        public Node<T> Prev { get; set; } = null;

        public Node(string key, T value)
        {
            Key = key;
            Value = value;
        }
    }

    public class CacheLinkedList<T>
    {
        public Node<T> Head { get; set; }
        public Node<T> Tail { get; set; }

        public int Count = 0;
        public CacheLinkedList()
        {
            Head = null;
        }

        public void AddItem(Node<T> node)
        {
           
            if (Head == null)
            {
                Head = Tail = node;
            }
            else
            {
                Tail.Next = node;
                node.Prev = Tail;
                Tail = node;
            }

            Count++;
        }

        public void AddItemToFront(Node<T> node)
        {
            if (Head == null)
            {
                Head = Tail = node;
            }
            else
            {
                Head.Prev = node;
                node.Next = Head;
                node.Prev = null;
                Head = node;
            }

            Count++;
        }

        public void DeleteItem(Node<T> node)
        {

            if (node == Head)
                Head = Head.Next;
            else if (node == Tail)
            {
                Tail = Tail.Prev;
                Tail.Next = null;
            }
            else
            {
                node.Prev.Next = node.Next;
                node.Next.Prev = node.Prev;
            }

            node.Next = null;
            node.Prev = null;

            Count--;

        }
        public Node<T> DeleteItem()
        {
            var node = Head;
            DeleteItem(Head);
            return node;

        }
    }
}
