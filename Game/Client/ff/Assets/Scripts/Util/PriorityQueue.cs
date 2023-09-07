using System;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public class PriorityQueue<T>
    {
        public PriorityQueue(Func<T, T, long> comparer)
        {
            _comparer = comparer;
            _queue = new List<T>();
        }

        private void Swap(int i, int j)
        {
            (_queue[i], _queue[j]) = (_queue[j], _queue[i]);
        }

        private void DoHeap(int i)
        {
            int length = _queue.Count, x, l, r;
            while (true) {
                x = i; l = GetLeft(i); r = GetRight(i);
                if (l < length && _comparer(_queue[l], _queue[x]) < 0) { x = l; }
                if (r < length && _comparer(_queue[r], _queue[x]) < 0) { x = r; }
                if (x == i) {
                    break;
                }
                Swap(i, x);
                i = x;
            }
        }

        private T Remove(int i)
        {
            var t = _queue[i];
            var b = _queue[_queue.Count - 1];
            _queue.RemoveAt(_queue.Count - 1);
            if (_queue.Count > 0)
            {
                _queue[i] = b;
                DoHeap(i);
            }

            return t;
        }

        public int Push(params T[] elements)
        {
            var i = _queue.Count;
            var e = i + elements.Length;
            int j, p;
            _queue.AddRange(elements);
            for (;  i< e; ++i)
            {
                j = i;
                p = GetParent(i);
                for (;  j > 0 && _comparer(_queue[j], _queue[p]) < 0; j = p, p = GetParent(j))
                {
                    Swap(j, p);
                }
            }
            return _queue.Count;
        }

        public T Pop()
        {
            return Remove(0);
        }
        
        public T Top()
        {
            return _queue[0];
        }

        public int GetLeft(int i)
        {
            return 2 * i + 1;
        }

        public int GetRight(int i)
        {
            return 2 * i + 2;
        }

        public int GetParent(int i)
        {
            // ReSharper disable once PossibleLossOfFraction
            return (int)Mathf.Floor((i + 1) / 2) - 1;
        }
        
        public int GetSize()
        {
            return _queue.Count;
        }
        
        
        private List<T> _queue;
        private readonly Func<T, T, long> _comparer;
    }
}
