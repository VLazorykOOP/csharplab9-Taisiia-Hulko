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

					// ������: ������� ��'� �� ������� ϳ����� ³� ��������
					// parts[3] - ������ (� ��� �)
					if (parts.Length < 6)
					{
						Console.WriteLine("������������ ������ �����: " + line);
						continue;
					}

					if (parts[3] == "�")
						menQueue.Enqueue(line);
					else if (parts[3] == "�")
						womenQueue.Enqueue(line);
					else
						Console.WriteLine("�������� ������ � �����: " + line);
				}
			}

			Console.WriteLine("��� ��� �������:");
			while (menQueue.Count > 0)
			{
				Console.WriteLine(menQueue.Dequeue());
			}

			Console.WriteLine("\n��� ��� ����:");
			while (womenQueue.Count > 0)
			{
				Console.WriteLine(womenQueue.Dequeue());
			}
		}
		catch (FileNotFoundException)
		{
			Console.WriteLine("���� �� ��������: " + fileName);
		}
		catch (Exception ex)
		{
			Console.WriteLine("�������: " + ex.Message);
		}
	}
}
