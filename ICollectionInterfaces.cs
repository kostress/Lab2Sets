using System;
using System.Collections.Generic; // нужно для IEnumerable (чтобы работал foreach)

namespace Lab2Sets
{
    // Общий интерфейс для всех структур
    public interface IMyCollection<T> : IEnumerable<T>
    {
        int Count { get; }        // Сколько элементов
        bool IsEmpty { get; }     // Пустая ли
        bool IsBounded { get; }   // Конечная ли (true - есть лимит)
        int Capacity { get; }     // Максимальный размер (если конечная)

        void Clear();             // Очистить
        bool Contains(T item);    // Есть ли такой элемент
        T[] ToArray();            // Получить все элементы (нужно для вывода на экран)
    }

    // Интерфейс для Стека (LIFO - последний зашел, первый вышел)
    public interface IMyStack<T> : IMyCollection<T>
    {
        void Push(T item);        // Положить сверху
        T Pop();                  // Снять сверху
        T Peek();                 // Посмотреть сверху
    }

    // Интерфейс для Очереди (FIFO - первый зашел, первый вышел)
    public interface IMyQueue<T> : IMyCollection<T>
    {
        void Enqueue(T item);     // Встать в конец
        T Dequeue();              // Выйти из начала
        T Peek();                 // Посмотреть первого
    }

    // Интерфейс для Дека (двусторонняя очередь)
    public interface IMyDeque<T> : IMyCollection<T>
    {
        void PushFront(T item);   // Добавить в начало
        void PushBack(T item);    // Добавить в конец
        T PopFront();             // Забрать из начала
        T PopBack();              // Забрать из конца
        T PeekFront();            // Посмотреть в начале
        T PeekBack();             // Посмотреть в конце
    }
}