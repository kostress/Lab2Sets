using System;
using System.Collections.Generic;

namespace Lab2Sets
{
    internal class Program
    {
        // Вспомогательный метод для красивого вывода структуры в консоль
        static void PrintCollection(string name, IMyCollection<int> collection)
        {
            Console.Write($"{name}: [ ");
            // Проходимся foreach'ем, который мы написали в базовом классе
            foreach (var item in collection)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("]");
        }

        static void Main(string[] args)
        {
            // 1. СОЗДАЕМ СТРУКТУРЫ
            // Стек делаем КОНЕЧНЫМ (максимум 5 элементов), чтобы проверить ошибку переполнения
            var stack = new MyStack<int>(isBounded: true, capacity: 5);

            // Очередь и Дек делаем БЕСКОНЕЧНЫМИ (по умолчанию)
            var queue = new MyQueue<int>();
            var deque = new MyDeque<int>();

            var rand = new Random();

            // 2. ЦИКЛ РАНДОМНОГО ЧТЕНИЯ-ЗАПИСИ (п.10)
   
            for (int i = 0; i < 12; i++)
            {
                int action = rand.Next(2); // 0 - положить, 1 - забрать
                int val = rand.Next(10, 100); // Случайное число от 10 до 99

                try
                {
                    if (action == 0)
                    {
                        Console.WriteLine($"Попытка положить {val}:");
                        stack.Push(val);
                        queue.Enqueue(val);
                        deque.PushBack(val);
                    }
                    else
                    {
                        Console.WriteLine("Попытка забрать элемент:");
                        int s = stack.Pop();
                        int q = queue.Dequeue();
                        int d = deque.PopFront();
                        Console.WriteLine($"  Успешно забрали -> Стек: {s}, Очередь: {q}, Дек: {d}");
                    }
                }
                catch (CollectionOverflowException ex)
                {
                    Console.WriteLine($"   {ex.Message}");
                }
                catch (CollectionEmptyException ex)
                {
                    Console.WriteLine($"   {ex.Message}");
                }

                // Выводим состояние структур после каждого шага
                PrintCollection("Стек ", stack);
                PrintCollection("Очередь", queue);
                PrintCollection("Дек  ", deque);
                Console.WriteLine(new string('-', 30));
            }

            // 3. ДЕМОНСТРАЦИЯ ОПЕРАЦИЙ НАД МНОЖЕСТВАМИ (п.5)
            Console.WriteLine("\n--- Демонстрация операций (*, +, -) ---");

            // Создадим два новых стека с конкретными числами
            var stackA = new MyStack<int>();
            stackA.Push(1); stackA.Push(2); stackA.Push(3);

            var stackB = new MyStack<int>();
            stackB.Push(3); stackB.Push(4); stackB.Push(5);

            Console.WriteLine("Есть два множества (Стека):");
            PrintCollection("A", stackA); // Будет 3 2 1 (потому что стек)
            PrintCollection("B", stackB); // Будет 5 4 3

            // Пересечение (A * B)
            var intersect = stackA * stackB;
            Console.WriteLine("\nПересечение (A * B) - есть и в A, и в B:");
            PrintCollection("Результат", intersect); // Должно быть 3

            // Объединение (A + B)
            var union = stackA + stackB;
            Console.WriteLine("\nОбъединение (A + B) - элементы B кладутся наверх A:");
            PrintCollection("Результат", union); // Должно быть 5 4 3 3 2 1

            // Разность (A - B)
            var difference = stackA - stackB;
            Console.WriteLine("\nРазность (A - B) - что есть в A, но нет в B:");
            PrintCollection("Результат", difference); // Должно быть 2 1

            // 4. ПРОВЕРКА ОШИБКИ ПЕРЕПОЛНЕНИЯ НА КОНЕЧНОЙ СТРУКТУРЕ (п.10, п.11)
            Console.WriteLine("\n--- Проверка переполнения конечной структуры (лимит 5) ---");
            var smallStack = new MyStack<int>(true, 5);
            try
            {
                // Пытаемся впихнуть 6 элементов в стек на 5 мест
                for (int i = 1; i <= 6; i++)
                {
                    smallStack.Push(i);
                    Console.WriteLine($"Успешно добавлен элемент {i}");
                }
            }
            catch (CollectionOverflowException ex)
            {
                Console.WriteLine($"\n[ИСКЛЮЧЕНИЕ] {ex.Message}"); // Сработает на 6-м элементе
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}