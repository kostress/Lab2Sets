using System;

namespace Lab2Sets
{
    // 1. Ошибка переполнения (для конечных структур)
    public class CollectionOverflowException : Exception
    {
        public CollectionOverflowException(string message) : base(message) { }
    }

    // 2. Ошибка пустой структуры (пытаемся достать, когда нечего)
    public class CollectionEmptyException : Exception
    {
        public CollectionEmptyException(string message) : base(message) { }
    }
}