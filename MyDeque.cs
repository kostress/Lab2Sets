using System;

namespace Lab2Sets
{
    public class MyDeque<T> : MyCollection<T>, IMyDeque<T>
    {
        public MyDeque(bool isBounded = false, int capacity = int.MaxValue)
            : base(isBounded, capacity) { }

        public void PushFront(T item)
        {
            EnsureCanAdd();
            var newNode = new Node<T>(item);
            if (head == null) head = tail = newNode;
            else
            {
                newNode.Next = head;
                head.Prev = newNode;
                head = newNode;
            }
            count++;
        }

        public void PushBack(T item)
        {
            EnsureCanAdd();
            var newNode = new Node<T>(item);
            if (tail == null) head = tail = newNode;
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }
            count++;
        }

        public T PopFront()
        {
            EnsureNotEmpty();
            T data = head!.Data;
            head = head.Next;
            if (head != null) head.Prev = null;
            else tail = null;
            count--;
            return data;
        }

        public T PopBack()
        {
            EnsureNotEmpty();
            T data = tail!.Data;
            tail = tail.Prev;
            if (tail != null) tail.Next = null;
            else head = null;
            count--;
            return data;
        }

        public T PeekFront() { EnsureNotEmpty(); return head!.Data; }
        public T PeekBack() { EnsureNotEmpty(); return tail!.Data; }

        // --- ОПЕРАЦИИ НАД МНОЖЕСТВАМИ ---
        public override MyCollection<T> Intersect(MyCollection<T> other)
        {
            var result = new MyDeque<T>();
            var cur = head;
            while (cur != null)
            {
                if (other.Contains(cur.Data) && !result.Contains(cur.Data))
                    result.PushBack(cur.Data);
                cur = cur.Next;
            }
            return result;
        }

        public override MyCollection<T> Union(MyCollection<T> other)
        {
            var result = new MyDeque<T>();
            // Сначала A (в конец)
            foreach (var item in ToArray()) result.PushBack(item);
            // Потом B (в конец)
            foreach (var item in other.ToArray()) result.PushBack(item);
            return result;
        }

        public override MyCollection<T> Difference(MyCollection<T> other)
        {
            var result = new MyDeque<T>();
            var cur = head;
            while (cur != null)
            {
                if (!other.Contains(cur.Data) && !result.Contains(cur.Data))
                    result.PushBack(cur.Data);
                cur = cur.Next;
            }
            return result;
        }
    }
}