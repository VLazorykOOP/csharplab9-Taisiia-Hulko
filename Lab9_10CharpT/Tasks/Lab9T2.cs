using System;
using System.Collections.Generic;
using System.IO;

public class Lab9T2
{
	public void Run()
	{
		string fileName = "employees.txt";

		Queue<string> menQueue = new Queue<string>();
		Queue<string> womenQueue = new Queue<string>();

		try
		{
			using (StreamReader sr = new StreamReader(fileName))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					string[] parts = line.Split(' ');

					// Формат: Прізвище Ім'я По батькові Підлога Вік Зарплата
					// parts[3] - підлога (Ч або Ж)
					if (parts.Length < 6)
					{
						Console.WriteLine("Неправильний формат рядка: " + line);
						continue;
					}

					if (parts[3] == "Ч")
						menQueue.Enqueue(line);
					else if (parts[3] == "Ж")
						womenQueue.Enqueue(line);
					else
						Console.WriteLine("Невідомий підлога у рядку: " + line);
				}
			}

			Console.WriteLine("Дані про чоловіків:");
			while (menQueue.Count > 0)
			{
				Console.WriteLine(menQueue.Dequeue());
			}

			Console.WriteLine("\nДані про жінок:");
			while (womenQueue.Count > 0)
			{
				Console.WriteLine(womenQueue.Dequeue());
			}
		}
		catch (FileNotFoundException)
		{
			Console.WriteLine("Файл не знайдено: " + fileName);
		}
		catch (Exception ex)
		{
			Console.WriteLine("Помилка: " + ex.Message);
		}
	}
}
