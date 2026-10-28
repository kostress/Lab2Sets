using System;

namespace Lab2Sets
{
    // Наследуемся от нашего базового класса и реализуем интерфейс стека
    public class MyStack<T> : MyCollection<T>, IMyStack<T>
    {
        // Конструктор. Если ничего не передать - стек бесконечный.
        // Если передать isBounded: true и capacity: 5 - то влезет максимум 5 элементов.
        public MyStack(bool isBounded = false, int capacity = int.MaxValue)
            : base(isBounded, capacity) { }

        // Положить элемент наверх
        public void Push(T item)
        {
            EnsureCanAdd(); // Проверяем, не переполнен ли (если конечный)

            var newNode = new Node<T>(item);
            newNode.Next = head; // Новый элемент указывает на бывшую вершину

            if (head != null)
                head.Prev = newNode; // Бывшая вершина теперь знает, что над ней новый
            else
                tail = newNode; // Если стек был пуст, то это и начало, и конец

            head = newNode; // Новая вершина - наш элемент
            count++;
        }

        // Снять элемент с вершины
        public T Pop()
        {
            EnsureNotEmpty(); // Проверяем, есть ли что снимать

            T data = head!.Data; // Запоминаем данные
            head = head.Next;    // Сдвигаем вершину вниз

            if (head != null)
                head.Prev = null; // У нового верха больше нет предыдущего
            else
                tail = null;      // Если стек стал пустым

            count--;
            return data;
        }

        // Посмотреть, что наверху (не снимая)
        public T Peek()
        {
            EnsureNotEmpty();
            return head!.Data;
        }

        // ==========================================
        // РЕАЛИЗАЦИЯ ОПЕРАЦИЙ НАД МНОЖЕСТВАМИ (*, +, -)
        // ==========================================

        // ПЕРЕСЕЧЕНИЕ (A * B): элементы, которые есть И в A, И в B.
        // Порядок берем как в A (от вершины вниз).
        public override MyCollection<T> Intersect(MyCollection<T> other)
        {
            var result = new MyStack<T>();
            var cur = head;

            while (cur != null)
            {
                // Если элемент есть в другом стеке И его еще нет в результате
                if (other.Contains(cur.Data) && !result.Contains(cur.Data))
                {
                    result.Push(cur.Data);
                }
                cur = cur.Next;
            }

            // Чтобы порядок в итоге совпадал с оригиналом, переворачиваем результат
            var reversed = new MyStack<T>();
            while (!result.IsEmpty) reversed.Push(result.Pop());
            return reversed;
        }

        // ОБЪЕДИНЕНИЕ (A + B): добавляем B наверх A.
        // Для стека "дисциплина обслуживания" - класть новые элементы наверх.
        public override MyCollection<T> Union(MyCollection<T> other)
        {
            var result = new MyStack<T>();

            // 1. Сначала копируем элементы A (снизу вверх, чтобы сохранить порядок)
            var itemsA = ToArray();
            for (int i = itemsA.Length - 1; i >= 0; i--)
                result.Push(itemsA[i]);

            // 2. Затем кладем элементы B сверху (тоже снизу вверх)
            var itemsB = other.ToArray();
            for (int i = itemsB.Length - 1; i >= 0; i--)
                result.Push(itemsB[i]);

            return result;
        }

        // РАЗНОСТЬ (A - B): элементы, которые есть в A, но НЕТ в B.
        public override MyCollection<T> Difference(MyCollection<T> other)
        {
            var temp = new MyStack<T>();
            var cur = head;

            while (cur != null)
            {
                // Если элемента нет в B И его еще нет в результате
                if (!other.Contains(cur.Data) && !temp.Contains(cur.Data))
                {
                    temp.Push(cur.Data);
                }
                cur = cur.Next;
            }

            // Переворачиваем, чтобы сохранить изначальный порядок
            var result = new MyStack<T>();
            while (!temp.IsEmpty) result.Push(temp.Pop());
            return result;
        }
    }
}