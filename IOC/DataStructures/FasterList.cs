using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IOC.DataStructures
{
    public struct FasterListEnumerator<T> : IEnumerator<T>
    {
        T[] _buffer;
        int _counter;
        int _size;
        T _current;

        public T Current => _current;

        public FasterListEnumerator(T[] buffer,int size) 
        {
            _size = size;
            _counter = 0;
            _buffer = buffer;
            _current = default(T); 
        }

        object IEnumerator.Current => _current;

        T IEnumerator<T>.Current => _current;

        public void Dispose()
        {
            _buffer = null;
        }

        public bool MoveNext()
        {
            if(_counter<_size)
            {
                _current = _buffer[_counter++];
                return true; 
            }

            _current = default(T);
            return false;
        }

        public void Reset()
        {
            _counter = 0;
        }

        bool IEnumerator.MoveNext() 
        {
            return MoveNext(); 
        }

        void IEnumerator.Reset() 
        {
            Reset(); 
        }
    }

    public struct FasterListEnumeratorCast<T, U> : IEnumerator<T> where T : U
    {
        FasterListEnumerator<U> _buffer;

        public T Current => (T)_buffer.Current;

        public FasterListEnumeratorCast(FasterListEnumerator<U> buffer)
        {
            _buffer = buffer; 
        }

        object IEnumerator.Current => (T)_buffer.Current;

        T IEnumerator<T>.Current => (T)_buffer.Current;

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            return _buffer.MoveNext();
        }

        public void Reset()
        {
            _buffer.Reset();
        }

        bool IEnumerator.MoveNext()
        {
            return MoveNext(); 
        }

        void IEnumerator.Reset()
        {
            Reset(); 
        }
    }

    public class FasterList<T> : IList<T>
    {
        const int MIN_SIZE = 4;
        T[] _buffer;
        int _count;

        public FasterList()
        {
            _count = 0;
            _buffer = new T[MIN_SIZE]; 
        }

        public FasterList(int initialSize)
        {
            _count = 0;
            _buffer = new T[initialSize]; 
        }

        public FasterList(ICollection<T> collection)
        {
            _buffer = new T[collection.Count];
            collection.CopyTo(_buffer,0);
            _count = _buffer.Length; 
        }

        public FasterList(FasterList<T> listCopy)
        {
            _buffer = new T[listCopy.Count];
            listCopy.CopyTo(_buffer,0);
            _count = _buffer.Length; 
        }

        public T this[int index] 
        {
            get 
            {
                DesignByContract.Check.Require(index<_count,"out of bound index");
                return _buffer[index]; 
            }

            set 
            {
                DesignByContract.Check.Require(index < _count, "out of bound index");
                _buffer[index] = value;
            } 
         }

        public int Count => _count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            if (_count == _buffer.Length) AllocateMore();
            _buffer[_count++] = item;
        }

        public static FasterList<T> PreFill<U>(int initialSize) where U:T,new()
        {
            var list = new FasterList<T>(initialSize);
            for (int i = 0; i < initialSize; ++i)
                list.Add(new U());

            list.Clear();
            return list; 
        }

        public void AddRange(IEnumerable<T> items,int count)
        {
            AddRange(items.GetEnumerator(),count); 
        }

        public void AddRange(IEnumerator<T> items,int count)
        {
            if (_count + count >= _buffer.Length) AllocateMore(_count+count);
            while (items.MoveNext()) _buffer[_count++] = items.Current; 
        }

        public void AddRange(ICollection<T> items)
        {
            AddRange(items.GetEnumerator(),items.Count); 
        }

        public void AddRange(FasterList<T> items)
        {
            AddRange(items.ToArrayFast(),items.Count); 
        }

        public void AddRange(T[] items,int count)
        {
            if (count == 0) return;
            if (_count + count >= _buffer.Length) AllocateMore(_count+count);
            Array.Copy(items,0,_buffer,_count,count);
            _count += count; 
        }

        public FasterReadOnlyList<T> AsReadOnly()
        {
            return new FasterReadOnlyList<T>(this); 
        }

        /// <summary>
        /// Careful, you could keep on holding references you don't want to hold to anymore
        /// Use DeepClear in case.
        /// </summary>
        public void Clear()
        {
            _count = 0;
        }

        public void DeepClear()
        {
            Array.Clear(_buffer,0,_buffer.Length);
            _count = 0; 
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(_buffer,0,array,arrayIndex,Count);
        }

        public FasterListEnumerator<T> GetEnumerator()
        {
            return new FasterListEnumerator<T>(_buffer,Count); 
        }

        public int IndexOf(T item)
        {
            var comp = EqualityComparer<T>.Default;
            for(int index = _count-1;index>=0;--index)
            {
                if (comp.Equals(_buffer[index], item)) return index; 
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            DesignByContract.Check.Require(index<_count,"out of bound index");
            if (_count == _buffer.Length) AllocateMore();
            Array.Copy(_buffer,index,_buffer,index+1,_count-index);
            _buffer[index] = item;
            ++_count;
        }

        public void Release()
        {
            _count = 0;
            _buffer = null; 
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index == -1) return false;
            RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            DesignByContract.Check.Require(index < _count, "out of bound index");
            if (index == --_count) return;
            Array.Copy(_buffer,index+1,_buffer,index,_count-index);
            _buffer[_count] = default(T);
        }

        public void Resize(int newSize)
        {
            if (newSize < MIN_SIZE) newSize = MIN_SIZE;
            Array.Resize(ref _buffer,newSize);
            _count = newSize; 
        }

        public void SetAt(int index,T value)
        {
            if (index >= _buffer.Length) AllocateMore(index+1);
            if (_count <= index) _count = index + 1;
            this[index] = value; 
        }

        public void Sort(IComparer<T> comparer)
        {
            Array.Sort(_buffer,0,_count,comparer); 
        }

        public T[] ToArray()
        {
            T[] destinationArray = new T[_count];
            Array.Copy(_buffer,0,destinationArray,0,_count);
            return destinationArray; 
        }

        /// <summary>
        /// This function exists to allow fast iterations. The size of the array returned cannot be
        /// used. The list count must be used instead.
        /// </summary>
        /// <returns></returns>
        public T[] ToArrayFast()
        {
            return _buffer; 
        }

        public bool UnorderredRemove(T item)
        {
            int index = IndexOf(item);
            if (index == -1) return false;
            UnorderredRemoveAt(index);
            return true; 
        }

        public T UnorderredRemoveAt(int index)
        {
            DesignByContract.Check.Require(index < _count && _count > 0, "out of bound index");
            T item = _buffer[index];
            if (index == -_count) return item;

            T swap = _buffer[index];
            _buffer[index] = _buffer[_count];
            _buffer[_count] = swap;

            return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator(); 
        }

        void AllocateMore()
        {
            var newList = new T[Mathf.Max(_buffer.Length << 1, MIN_SIZE)];
            if (_count > 0) _buffer.CopyTo(newList,0);
            _buffer = newList;
        }

        void AllocateMore(int newSize)
        {
            var oldLength = Mathf.Max(_buffer.Length,MIN_SIZE);
            while (oldLength < newSize) oldLength <<= 1;
            var newList = new T[oldLength];
            if (_count > 0) Array.Copy(_buffer,newList,_count);
            _buffer = newList; 
        }

        public void Trim()
        {
            if (_count < _buffer.Length) Resize(_count); 
        }

        public bool Reuse(int index,out T result)
        {
            result = default(T);
            if (index >= _buffer.Length) return false;
            result = _buffer[index];
            return result != null; 
        }
    }

    public struct FasterReadOnlyList<T> : IList<T>
    {
        readonly FasterList<T> _list;

        public FasterReadOnlyList(FasterList<T> list)
        {
            _list = list; 
        }

        public T this[int index] 
        { 
            get => _list[index]; 
            set => throw new NotImplementedException(); 
        }

        public FasterListEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator(); 
        }

        public int Count => _list.Count;

        public bool IsReadOnly => true;

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array,arrayIndex);
        }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public struct FasterReadOnlyListCast<T, U> : IList<U> where U : T
    {
        readonly FasterList<T> _list;

        public static FasterList<T> DefaultList = new FasterList<T>();

        public U this[int index] 
        { 
            get => (U)_list[index]; 
            set => throw new NotImplementedException(); 
        }

        public int Count => _list.Count;

        public bool IsReadOnly => true;

        public FasterReadOnlyListCast(FasterList<T>list)
        {
            _list = list; 
        }

        public FasterListEnumeratorCast<U,T> GetEnumerator()
        {
            return new FasterListEnumeratorCast<U, T>(_list.GetEnumerator()); 
        }

        public void Add(U item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(U item) => _list.Contains(item);

        public void CopyTo(U[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(U item) => _list.IndexOf(item);

        public void Insert(int index, U item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(U item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IEnumerator<U> IEnumerable<U>.GetEnumerator() => GetEnumerator();
    }
}
