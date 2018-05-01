using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump
{
    public class QueueItem<T>
    {
        public T Value { get; set; }
        public QueueItem<T> Next { get; set; }
        public QueueItem<T> Previous { get; set; }
    }

    public class Queue<T>
    {
        public QueueItem<T> Head { get; private set; }
        public QueueItem<T> Tail { get; private set; }
        public int Count { get; private set; }
        public void Enqueue(T value)
        {
            if (Head == null)
                Tail = Head = new QueueItem<T> { Value = value, Next = null };
            else
            {
                var item = new QueueItem<T> { Value = value, Next = null };
                item.Previous = Tail;
                Tail.Next = item;
                Tail = item;
            }

            Count++;
        }

        public T DequeueFromHead()
        {
            if (Head == null) throw new InvalidOperationException();
            var result = Head.Value;
            Head = Head.Next;
            if (Head == null)
                Tail = null;
            Count--;
            return result;
        }
        public T DequeueFromTail()
        {
            if (Tail == null) throw new InvalidOperationException();
            Tail = Tail.Previous;
            Count--;
            return Tail.Value;
        }

    }
}
