using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyArrayList
{
    public class _ArrayList
    {
        private Object[] _items;
        private int _size;
        private int _version;
        private const int _defaultCapacity = 4;
        private static readonly Object[] emptyArray = new Object[0];
        public _ArrayList()
        {
            _items = emptyArray;
        }
        public _ArrayList(int capacity)
        {
            if (capacity < 0) throw new ArgumentOutOfRangeException();
            if (capacity == 0)
                _items = emptyArray;
            else
                _items = new Object[capacity];
        }
        public _ArrayList(ICollection c)
        {
            if (c == null)
                throw new ArgumentNullException();
            int count = c.Count;
            if (count == 0)
            {
                _items = emptyArray;
            }
            else
            {
                _items = new Object[count];
                AddRange(c);
            }
        }
        public virtual Object this[int index]
        {
            get
            {
                if (index < 0 || index >= _size) throw new ArgumentOutOfRangeException();
                return _items[index];
            }
            set
            {
                if (index < 0 || index >= _size) throw new ArgumentOutOfRangeException();
                _items[index] = value;
                _version++;
            }
        }
        public virtual int Add(Object value)
        {
            if (_size == _items.Length) EnsureCapacity(_size + 1);
            _items[_size] = value;
            _version++;
            return _size++;
        }
        public virtual int BinarySearch(int index, int count, Object value, IComparer comparer)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index", "ArgumentOutOfRange_NeedNonNegNum");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count", "ArgumentOutOfRange_NeedNonNegNum");
            if (_size - index < count)
                throw new ArgumentException("Argument_InvalidOffLen");

            return Array.BinarySearch((Array)_items, index, count, value, comparer);
        }
        public virtual int BinarySearch(Object value)
        {
            return BinarySearch(0, Count, value, null);
        }
        public virtual int BinarySearch(Object value, IComparer comparer)
        {
            return BinarySearch(0, Count, value, comparer);
        }
        public virtual void Clear()
        {
            if (_size > 0)
            {
                Array.Clear(_items, 0, _size);
                _size = 0;
            }
            _version++;
        }
        public virtual Object Clone()
        {
            _ArrayList la = new _ArrayList(_size);
            la._size = _size;
            la._version = _version;
            Array.Copy(_items, 0, la._items, 0, _size);
            return la;
        }
        public virtual bool Contains(Object item)
        {
            if (item == null)
            {
                for (int i = 0; i < _size; i++)
                    if (_items[i] == null)
                        return true;
                return false;
            }
            else
            {
                for (int i = 0; i < _size; i++)
                    if ((_items[i] != null) && (_items[i].Equals(item)))
                        return true;
                return false;
            }
        }
        public virtual void CopyTo(Array array)
        {
            CopyTo(array, 0);
        }
        public virtual void CopyTo(Array array, int arrayIndex)
        {
            if ((array != null) && (array.Rank != 1))
                throw new ArgumentException();
            Array.Copy(_items, 0, array, arrayIndex, _size);
        }
        public virtual void CopyTo(int index, Array array, int arrayIndex, int count)
        {
            if (_size - index < count)
                throw new ArgumentException();
            if ((array != null) && (array.Rank != 1))
                throw new ArgumentException();
        }
        public virtual int IndexOf(Object value)
        {
            return Array.IndexOf((Array)_items, value, 0, _size);
        }
        public virtual int IndexOf(Object value, int startIndex)
        {
            if (startIndex > _size)
                throw new ArgumentOutOfRangeException();
            return Array.IndexOf((Array)_items, value, startIndex, _size - startIndex);
        }
        public virtual int IndexOf(Object value, int startIndex, int count)
        {
            if (startIndex > _size)
                throw new ArgumentOutOfRangeException();
            if (count < 0 || startIndex > _size - count) throw new ArgumentOutOfRangeException();
            return Array.IndexOf((Array)_items, value, startIndex, count);
        }
        public virtual void Insert(int index, Object value)
        {
            if (index < 0 || index > _size) throw new ArgumentOutOfRangeException();
            if (_size == _items.Length) EnsureCapacity(_size + 1);
            if (index < _size)
            {
                Array.Copy(_items, index, _items, index + 1, _size - index);
            }
            _items[index] = value;
            _size++;
            _version++;
        }
        public virtual void Reverse()
        {
            Reverse(0, Count);
        }
        public virtual void Reverse(int index, int count)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException();
            if (count < 0)
                throw new ArgumentOutOfRangeException();
            if (_size - index < count)
                throw new ArgumentException();
            Array.Reverse(_items, index, count);
            _version++;
        }
        public virtual void AddRange(ICollection c)
        {
            InsertRange(_size, c);
        }
        public virtual void InsertRange(int index, ICollection c)
        {
            if (c == null)
                throw new ArgumentNullException();
            if (index < 0 || index > _size) throw new ArgumentOutOfRangeException();

            int count = c.Count;
            if (count > 0)
            {
                EnsureCapacity(_size + count);
                if (index < _size)
                {
                    Array.Copy(_items, index, _items, index + count, _size - index);
                }
                Object[] itemsToInsert = new Object[count];
                c.CopyTo(itemsToInsert, 0);
                itemsToInsert.CopyTo(_items, index);
                _size += count;
                _version++;
            }
        }
        private void EnsureCapacity(int min)
        {
            if (_items.Length < min)
            {
                int newCapacity = _items.Length == 0 ? _defaultCapacity : _items.Length * 2;
                if ((uint)newCapacity > 0X7FEFFFFF) newCapacity = 0X7FEFFFFF;
                if (newCapacity < min) newCapacity = min;
                Capacity = newCapacity;
            }
        }
        public virtual int Capacity
        {
            get
            {
                return _items.Length;
            }
            set
            {
                if (value < _size)
                {
                    throw new ArgumentOutOfRangeException();
                }
                if (value != _items.Length)
                {
                    if (value > 0)
                    {
                        Object[] newItems = new Object[value];
                        if (_size > 0)
                        {
                            Array.Copy(_items, 0, newItems, 0, _size);
                        }
                        _items = newItems;
                    }
                    else
                    {
                        _items = new Object[_defaultCapacity];
                    }
                }
            }
        }
        public virtual int Count
        {
            get
            {
                return _size;
            }
        }
    }
}
