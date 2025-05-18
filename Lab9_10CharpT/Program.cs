using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Lab#9 or Lab#10");

        while (true)
        {
            Console.WriteLine("\nВибери завдання для запуску:");
            Console.WriteLine("1 - Завдання 1 (префікс → постфікс)");
            Console.WriteLine("2 - Завдання 2 (фільтрація співробітників)");
            Console.WriteLine("3 - Завдання 3 (ArrayList, IEnumerable, IComparer, ICloneable)");
            Console.WriteLine("4 - Музикальний диск");
            Console.WriteLine("0 - Вийти");

            Console.Write("Твій вибір: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Lab9T1 lab9task1 = new Lab9T1();
                lab9task1.Run();
            }
            else if (choice == "2")
            {
                Lab9T2 lab9task2 = new Lab9T2();
                lab9task2.Run();
            }
            else if (choice == "3")
            {
                Lab9T3 lab9task3 = new Lab9T3();
                lab9task3.Run();
            }
            else if (choice == "4")
            {
                Lab9T4 lab9task4 = new Lab9T4();
                lab9task4.Run();
            }
            else if (choice == "0")
            {
                Console.WriteLine("Вихід з програми.");
                break;
            }
            else
            {
                Console.WriteLine("Невірний вибір, спробуй ще раз.");
            }
        }

        Console.WriteLine("Усі задачі виконано. Натисни Enter для виходу.");
        Console.ReadLine();
    }
}
