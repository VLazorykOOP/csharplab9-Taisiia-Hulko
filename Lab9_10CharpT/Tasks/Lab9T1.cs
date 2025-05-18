using System;
using System.Collections.Generic;

public class Lab9T1
{
	public void Run()
	{
		Console.WriteLine("Введи префіксний вираз:");
		string prefix = Console.ReadLine();

		string postfix = ConvertPrefixToPostfix(prefix);

		Console.WriteLine("Постфіксний вираз:");
		Console.WriteLine(postfix);
	}

	private bool IsOperator(char ch)
	{
		if (ch == '+' || ch == '-' || ch == '*' || ch == '/' || ch == '^')
			return true;
		else
			return false;
	}

	private string ConvertPrefixToPostfix(string prefix)
	{
		Stack<string> stack = new Stack<string>();

		// Ідемо по рядку справа наліво
		for (int i = prefix.Length - 1; i >= 0; i--)
		{
			char ch = prefix[i];

			if (ch == ' ')
			{
				continue; // пропускаємо пробіли
			}

			if (IsOperator(ch))
			{
				// Оператор: беремо два останні елементи зі стеку
				string op1 = stack.Pop();
				string op2 = stack.Pop();

				string newExpr = op1 + " " + op2 + " " + ch;
				stack.Push(newExpr);
			}
			else
			{
				// Операнд: кладемо в стек
				stack.Push(ch.ToString());
			}
		}

		// В кінці у стеку має бути один вираз - постфіксний
		return stack.Pop();
	}
}
