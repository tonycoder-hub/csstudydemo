using System;
using System.Collections.Generic;

namespace HomeworkMar12th
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Input the Problem Number you want(1,2,3,4,5)");
            int a = Convert.ToInt16(Console.ReadLine());
            switch (a)
            {
                case 1:
                    {
                        Problem1.Solution();
                        break;
                    }
                case 2:
                    {
                        Problem2.Solution();
                        break;
                    }
                case 3:
                    {
                        Problem3.Solution();
                        break;
                    }
                case 4:
                    {
                        Problem1.Solution();
                        break;
                    }
                case 5:
                    {
                        Problem1.Solution();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Terrible input!");
                        break;
                    }
            }

        }
    }

    class Problem4
    {
        public static void Solution()
        {
            // Class Shape is out of Class Problem4
        }


    }

   abstract class Shape
    {
        public virtual double GetArea()
        {

        }
    }
    class Problem3
    {
        public static void Solution()
        {
            long t = Convert.ToInt64(Console.ReadLine());
            int[] a = new int[t];
            for(int b = 1; b <= t; b++)
            {
                a[b - 1] = b;
            }
            a[0] = -1;
            for(int d = 2; d < t; d++)
            {
                for(int e = 2; e * d <= t; e++)
                {
                    a[e * d - 1] = -1;
                }
            }
            Console.WriteLine("The result here:");
            for (int c = 0; c < t; c++)
            {
                if (a[c] != -1)
                {
                    Console.Write(a[c] + " ");
                }
            }
            Console.Write("\n");

        }
    }
    class Problem2
    {
        public static void Solution()
        {
            Console.WriteLine("Tell me how much number you want to add");
            int a = Convert.ToInt16(Console.ReadLine());
            long[] longArray = new long[a];
            Console.WriteLine("Input number here");
            for(int b = 0; b < a; b++)
            {
                long tmp = Convert.ToInt64(Console.ReadLine());
                longArray[b] = tmp;
            }

            Console.WriteLine("The array input here:");
            for (int c = 0; c < a; c++)
            {
                Console.Write(longArray[c] + " ");
            }
            Console.Write("\n");
            Console.WriteLine("Maximum");
            Console.WriteLine(getMax(longArray));
            Console.WriteLine("Minimum");
            Console.WriteLine(getMin(longArray));
            Console.WriteLine("Total");
            Console.WriteLine(getTotal(longArray));
            Console.WriteLine("Average");
            Console.WriteLine(getAverage(longArray));
            

        }
        public static long getMax(long[] a)
        {
            long tmp = a[0];
            foreach (long b in a)
            {
                if (b >= tmp)
                {
                    tmp = b;
                }
            }
            return tmp;
        }

        public static long getMin(long[] a)
        {
            long tmp = a[0];
            foreach (long b in a)
            {
                if (b <= tmp)
                {
                    tmp = b;
                }
            }
            return tmp;
        }
        public static long getTotal(long[] a)
        {
            long tmp = 0;
            foreach (long b in a)
            {
                tmp += b;
            }
            return tmp;
        }
        public static double getAverage(long[] a)
        {
            double tmp = getTotal(a) / a.Length;
            return tmp;
        }

    }

    class Problem1
    {
        public static void Solution()
        {
            Console.WriteLine("Input the number here");
            long a = Convert.ToInt64(Console.ReadLine());
            List<long> resultList = new List<long>();
            for(long b = 1; b < System.Math.Sqrt(a); b++)
            {
                if (a % b == 0)
                {
                    resultList.Add(b);
                    resultList.Add(a / b);
                }
            }
            resultList.Sort();
            foreach (long i in resultList){
                Console.Write(i+" ");
            }

        }
    }
}
