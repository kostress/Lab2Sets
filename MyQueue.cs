using System;

namespace Lab2Sets
{
    public class MyQueue<T> : MyCollection<T>, IMyQueue<T>
    {
        public MyQueue(bool isBounded = false, int capacity = int.MaxValue)
            : base(isBounded, capacity) { }

        // Встать в конец очереди
        public void Enqueue(T item)
        {
            EnsureCanAdd();
            var newNode = new Node<T>(item);

            if (tail == null) // Если очередь пуста
            {
                head = tail = newNode;
            }
            else // Если уже кто-то есть
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }
            count++;
        }

        // Выйти из начала очереди
        public T Dequeue()
        {
            EnsureNotEmpty();
            T data = head!.Data;

            head = head.Next;
            if (head != null)
                head.Prev = null;
            else
                tail = null; // Очередь стала пустой

            count--;
            return data;
        }

        // Посмотреть, кто первый
        public T Peek()
        {
            EnsureNotEmpty();
            return head!.Data;
        }

        // --- ОПЕРАЦИИ НАД МНОЖЕСТВАМИ ---

        // ПЕРЕСЕЧЕНИЕ: элементы из A, которые есть в B. Порядок как в A (с начала).
        public override MyCollection<T> Intersect(MyCollection<T> other)
        {
            var result = new MyQueue<T>();
            var cur = head;
            while (cur != null)
            {
                if (other.Contains(cur.Data) && !result.Contains(cur.Data))
                    result.Enqueue(cur.Data);
                cur = cur.Next;
            }
            return result;
        }

        // ОБЪЕДИНЕНИЕ: добавляем B в конец A (дисциплина очереди!)
        public override MyCollection<T> Union(MyCollection<T> other)
        {
            var result = new MyQueue<T>();

            // Копируем A
            var itemsA = ToArray();
            foreach (var item in itemsA) result.Enqueue(item);

            // Копируем B в конец
            var itemsB = other.ToArray();
            foreach (var item in itemsB) result.Enqueue(item);

            return result;
        }

        // РАЗНОСТЬ: элементы A, которых нет в B.
        public override MyCollection<T> Difference(MyCollection<T> other)
        {
            var result = new MyQueue<T>();
            var cur = head;
            while (cur != null)
            {
                if (!other.Contains(cur.Data) && !result.Contains(cur.Data))
                    result.Enqueue(cur.Data);
                cur = cur.Next;
            }
            return result;
        }
    }
}