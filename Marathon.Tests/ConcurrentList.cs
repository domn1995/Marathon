using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Marathon.Tests
{
    public class ConcurrentList<T> : IList<T>
    {
        private readonly object locker = new object();
        private readonly List<T> storage = new List<T>();

        public IEnumerator<T> GetEnumerator()
        {
            lock (locker)
            {
                return storage.GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (locker)
            {
                return storage.GetEnumerator();
            }
        }

        public void Add(T item)
        {
            lock (locker)
            {
                storage.Add(item);
            }
        }

        public void Clear()
        {
            lock (locker)
            {
                storage.Clear();;
            }
        }

        public bool Contains(T item)
        {
            lock (locker)
            {
                return storage.Contains(item);
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (locker)
            {
                storage.CopyTo(array, arrayIndex);
            }
        }

        public bool Remove(T item)
        {
            lock (locker)
            {
                return storage.Remove(item);
            }
        }

        public int Count
        {
            get
            {
                lock (locker)
                {
                    return storage.Count;
                }
            }
        }

        public bool IsReadOnly
        {
            get
            {
                lock (locker)
                {
                    return ((IList<T>)storage).IsReadOnly;
                }
            }
        }
        public int IndexOf(T item)
        {
            lock (locker)
            {
                return storage.IndexOf(item);
            }
        }

        public void Insert(int index, T item)
        {
            lock (locker)
            {
                storage.Insert(index, item);
            }
        }

        public void RemoveAt(int index)
        {
            lock (locker)
            {
                storage.RemoveAt(index);
            }
        }

        public T this[int index]
        {
            get
            {
                lock (locker)
                {
                    return storage[index];
                }
            }
            set
            {
                lock (locker)
                {
                    storage[index] = value;
                }
            }
        }
    }
}
