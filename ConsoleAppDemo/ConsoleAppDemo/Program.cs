using System;

namespace ConsoleAppDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Give me 2 numbers and a signal");
            double a = Convert.ToDouble(Console.ReadLine());
            double b = Convert.ToDouble(Console.ReadLine());
            char c = Convert.ToChar(Console.ReadLine());
            switch (c)
            {
                case '+':
                    {
                        Console.WriteLine(a + b);
                        break;
                    }
                case '-':
                    {
                        Console.WriteLine(a - b);
                        break;
                    }
                case '*':
                    {
                        Console.WriteLine(a * b);
                        break;
                    }
                case '/':
                    {
                        Console.WriteLine(a / b);
                        break;
                    }
                case '%':
                    {
                        Console.WriteLine(a % b);
                        break;
                    }
            }
            
        }
    }
}
