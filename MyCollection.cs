using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab2Sets
{
    // Абстрактный класс - "скелет" для Стека, Очереди и Дека.
    // Он содержит всё общее, что есть у этих трех структур.
    public abstract class MyCollection<T> : IMyCollection<T>
    {
        protected Node<T>? head;  // Начало списка
        protected Node<T>? tail;  // Конец списка
        protected int count;      // Сколько сейчас элементов

        public int Count => count;
        public bool IsEmpty => count == 0;
        public bool IsBounded { get; protected set; } // Конечная?
        public int Capacity { get; protected set; }   // Лимит

        // Конструктор. По умолчанию структура бесконечная.
        protected MyCollection(bool isBounded = false, int capacity = int.MaxValue)
        {
            IsBounded = isBounded;
            Capacity = isBounded ? capacity : int.MaxValue; // Если не конечная - лимит огромный
            head = tail = null;
            count = 0;
        }

        // Метод для проверки: а можно ли еще добавить?
        // Если структура конечная и уже полная - выбрасываем нашу ошибку!
        protected void EnsureCanAdd()
        {
            if (IsBounded && count >= Capacity)
                throw new CollectionOverflowException($"Структура переполнена! Максимум: {Capacity}");
        }

        // Метод для проверки: а есть ли что доставать?
        protected void EnsureNotEmpty()
        {
            if (IsEmpty)
                throw new CollectionEmptyException("Структура пуста!");
        }

        // Есть ли элемент в структуре
        public bool Contains(T item)
        {
            var cur = head;
            while (cur != null)
            {
                // Сравниваем элементы
                if (EqualityComparer<T>.Default.Equals(cur.Data, item))
                    return true;
                cur = cur.Next;
            }
            return false;
        }

        // Очистить всё
        public void Clear()
        {
            head = tail = null;
            count = 0;
        }

        // Получить элементы в виде обычного массива (только для вывода на экран, не для хранения!)
        public T[] ToArray()
        {
            var arr = new T[count];
            var cur = head;
            int i = 0;
            while (cur != null) { arr[i++] = cur.Data; cur = cur.Next; }
            return arr;
        }

        // Чтобы можно было перебирать элементы через foreach
        public IEnumerator<T> GetEnumerator()
        {
            var cur = head;
            while (cur != null) { yield return cur.Data; cur = cur.Next; }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        // ----- Перегрузка операций (п.5) -----
        // Мы объявляем их здесь, но конкретно реализуем в Стеке, Очереди и Деке.
        // Потому что "дисциплина обслуживания" у них разная!
        public abstract MyCollection<T> Intersect(MyCollection<T> other); // Пересечение (*)
        public abstract MyCollection<T> Union(MyCollection<T> other);     // Объединение (+)
        public abstract MyCollection<T> Difference(MyCollection<T> other);// Разность (-)

        // Сами операторы * + -
        public static MyCollection<T> operator *(MyCollection<T> a, MyCollection<T> b) => a.Intersect(b);
        public static MyCollection<T> operator +(MyCollection<T> a, MyCollection<T> b) => a.Union(b);
        public static MyCollection<T> operator -(MyCollection<T> a, MyCollection<T> b) => a.Difference(b);
    }
}