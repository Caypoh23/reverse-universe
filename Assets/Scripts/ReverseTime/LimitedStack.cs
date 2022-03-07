using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ReverseTime
{
    /// <summary>
    /// Generic stack implementation with a maximum limit
    /// When something is pushed on the last item is removed from the list
    /// </summary>
    [Serializable]
    public class LimitedStack<T>
    {
        #region Fields

        private readonly LinkedList<T> items = new LinkedList<T>();

        private int _capacity;

        #endregion

        public LimitedStack(int capacity) => _capacity = capacity;

        #region Stack Implementation

        public void Push(T item)
        {
            // full
            if (items.Count == _capacity)
            {
                // we should remove first, because some times, if we exceeded the size of the internal array
                // the system will allocate new array.
                items.RemoveFirst();
                items.AddLast(item);
            }
            else
            {
                items.AddLast(new LinkedListNode<T>(item));
            }
        }

        public T Pop()
        {
            var lastItem = items.Last;

            if (items.Count == 0)
            {
                return default;
            }

            items.RemoveLast();

            return lastItem == null ? default : lastItem.Value;
        }

        public int Count
        {
            get { return items.Count; }
        }
        #endregion
    }
}
