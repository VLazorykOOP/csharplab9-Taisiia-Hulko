using System;
using System.Collections.Generic;

public class Lab9T1
{
	public void Run()
	{
		Console.WriteLine("����� ���������� �����:");
		string prefix = Console.ReadLine();

		string postfix = ConvertPrefixToPostfix(prefix);

		Console.WriteLine("����������� �����:");
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

		// ����� �� ����� ������ �����
		for (int i = prefix.Length - 1; i >= 0; i--)
		{
			char ch = prefix[i];

			if (ch == ' ')
			{
				continue; // ���������� ������
			}

			if (IsOperator(ch))
			{
				// ��������: ������ ��� ������ �������� � �����
				string op1 = stack.Pop();
				string op2 = stack.Pop();

				string newExpr = op1 + " " + op2 + " " + ch;
				stack.Push(newExpr);
			}
			else
			{
				// �������: ������� � ����
				stack.Push(ch.ToString());
			}
		}

		// � ���� � ����� �� ���� ���� ����� - �����������
		return stack.Pop();
	}
}
