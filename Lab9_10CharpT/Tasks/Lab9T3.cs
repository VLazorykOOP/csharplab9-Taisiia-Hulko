using System;
using System.Collections;
using System.Collections.Generic;

public class Lab9T3
{
    public void Run()
    {
        Console.WriteLine("1 - Префікс → Постфікс з ArrayList");
        Console.WriteLine("2 - Обробка співробітників з ArrayList");
        Console.Write("Обери підзавдання: ");
        string choice = Console.ReadLine();

        if (choice == "1")
            PrefixToPostfixArrayList();
        else if (choice == "2")
            EmployeeProcessingArrayList();
        else
            Console.WriteLine("Невірний вибір.");
    }

    private void PrefixToPostfixArrayList()
    {
        Console.WriteLine("Введи префіксний вираз:");
        string prefix = Console.ReadLine();

        ArrayList stack = new ArrayList();

        for (int i = prefix.Length - 1; i >= 0; i--)
        {
            char ch = prefix[i];
            if (ch == ' ') continue;

            if (IsOperator(ch))
            {
                string op1 = (string)stack[stack.Count - 1];
                stack.RemoveAt(stack.Count - 1);
                string op2 = (string)stack[stack.Count - 1];
                stack.RemoveAt(stack.Count - 1);
                string newExpr = op1 + " " + op2 + " " + ch;
                stack.Add(newExpr);
            }
            else
            {
                stack.Add(ch.ToString());
            }
        }

        Console.WriteLine("Постфіксний вираз:");
        Console.WriteLine(stack[stack.Count - 1]);
    }

    private bool IsOperator(char ch)
    {
        return ch == '+' || ch == '-' || ch == '*' || ch == '/' || ch == '^';
    }

    private void EmployeeProcessingArrayList()
    {
        ArrayList employees = new ArrayList();
        employees.Add(new Employee("Іванов", "Іван", "Іванович", "Ч", 30, 8000));
        employees.Add(new Employee("Петренко", "Марія", "Олександрівна", "Ж", 28, 9500));
        employees.Add(new Employee("Сидоренко", "Олексій", "Петрович", "Ч", 45, 12000));
        employees.Add(new Employee("Коваль", "Олена", "Миколаївна", "Ж", 35, 11000));

        ArrayList men = new ArrayList();
        ArrayList women = new ArrayList();

        foreach (Employee emp in employees)
        {
            if (emp.Gender == "Ч")
                men.Add(emp.Clone());
            else if (emp.Gender == "Ж")
                women.Add(emp.Clone());
        }

        Console.WriteLine("Чоловіки:");
        foreach (Employee emp in men)
            Console.WriteLine(emp);

        Console.WriteLine("\nЖінки:");
        foreach (Employee emp in women)
            Console.WriteLine(emp);
    }
}

public class Employee : ICloneable, IComparable<Employee>
{
    public string LastName;
    public string FirstName;
    public string MiddleName;
    public string Gender;
    public int Age;
    public int Salary;

    public Employee(string ln, string fn, string mn, string gender, int age, int salary)
    {
        LastName = ln;
        FirstName = fn;
        MiddleName = mn;
        Gender = gender;
        Age = age;
        Salary = salary;
    }

    public object Clone()
    {
        return new Employee(LastName, FirstName, MiddleName, Gender, Age, Salary);
    }

    public int CompareTo(Employee other)
    {
        return this.Salary.CompareTo(other.Salary);
    }

    public override string ToString()
    {
        return $"{LastName} {FirstName} {MiddleName} {Gender} {Age} {Salary}";
    }
}
