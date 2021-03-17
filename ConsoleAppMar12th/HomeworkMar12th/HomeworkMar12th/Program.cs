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
                        Problem4.Solution();
                        break;
                    }
                case 5:
                    {
                        Problem5.Solution();
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
    class Problem5
    {
        public static void Solution()
        {
            ShapeFactory sf = new ShapeFactory();
            List<Shape> useless = new List<Shape>();
            for (int i = 0; i < 10;)
            {
                Shape tmp = sf.produceShape();
                if (tmp.isLegal())
                {
                    Console.WriteLine(tmp.toString());
                    Console.WriteLine("Area: " + tmp.GetArea());
                    i++;
                }
                else
                {
                    useless.Add(tmp);
                }
                
            }
            Console.WriteLine(useless.Count);

        }
        public static int getRandomInt(int a, int b)
        {
            Random r = new Random();
            return r.Next(a, b);
        }
        public static double getRandomDouble(int a, int b)
        {
            Random r = new Random();
            return Problem5.getRandomInt(a, b)*r.NextDouble();
        }

    }
    class ShapeFactory
    {
        public Shape produceShape()
        {
            int whichOne = Problem5.getRandomInt(1, 4);
            switch (whichOne)
            {
                case 1:
                    {
                        double b = Problem5.getRandomDouble(-10000, 10000);
                        double c = Problem5.getRandomDouble(-10000, 10000);
                        return new Rectangle(b,c);
                    }
                case 2:
                    {
                        double b = Problem5.getRandomDouble(-10000, 10000);
                        return new Square(b);
                    }
                case 3:
                    {
                        double b = Problem5.getRandomDouble(-10000, 10000);
                        double c = Problem5.getRandomDouble(-10000, 10000);
                        double d = Problem5.getRandomDouble(-10000, 10000);
                        return new Triangle(b, c, d);
                    }
                default:
                    {
                        throw new Exception("Unexpected input");
                    }
            }
        }
    }
    class Problem4
    {
        public static void Solution()
        {
            // Class Shape is out of Class Problem4
            Shape a = new Rectangle(4, 5);
            Console.WriteLine(a.isLegal());
            Console.WriteLine(a.GetArea());
            Shape b = new Triangle(4, 5, 3);
            Console.WriteLine(b.isLegal());
            Console.WriteLine(b.GetArea());
            Shape c = new Square(4);
            Console.WriteLine(c.isLegal());
            Console.WriteLine(c.GetArea());
            Shape d = new Rectangle(4, 0);
            Console.WriteLine(d.isLegal());
            Console.WriteLine(d.GetArea());
            Shape e = new Triangle(1, 1,2);
            Console.WriteLine(e.isLegal());
            Console.WriteLine(e.GetArea());
            Shape f = new Square(-1);
            Console.WriteLine(f.isLegal());
            Console.WriteLine(f.GetArea());
        }


    }

   abstract class Shape
    {
        public abstract double GetArea();
        public abstract bool isLegal();
        public abstract string toString();

    }
    class Rectangle : Shape
    {
        public Rectangle(double width,double length)
        {
            this.width = width;
            this.length = length;
        }
        private double width { get; set; }
        private double length{ get; set; }
        public override double GetArea()
        {
            if (isLegal())
            {
                return width * length;
            }
            else
            {
                return -1;
            }
        }

        public override bool isLegal()
        {
            if(width>0 && length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override string toString()
        {
            return this.GetType() + " " + "width: " + width + " " + "length: " + length;
        }
    }
    class Triangle : Shape
    {
        public Triangle(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
        private double a { get; set; } 
        private double b { get; set; } 
        private double c { get; set; }
        public override double GetArea()
        {
            if (isLegal())
            {
                double tmp = (a + b + c) / 2;
                return Math.Sqrt(tmp * (tmp - a) * (tmp - b) * (tmp - c));
            }
            else
            {
                return -1;
            }
        }

        public override bool isLegal()
        {
            if (a + b > c && b + c > a && a + c > b && a>0 && b>0 && c>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override string toString()
        {
            return this.GetType() + " " + "a: " + a + " " + "b: " + b +" c: "+c;
        }
    }

    class Square : Shape
    {
        public Square(double len)
        {
            this.len = len;
        }
        private double len { get; set; }
        public override double GetArea()
        {
            if (isLegal())
            {
                return len * len;
            }
            else
            {
                return -1;
            }
        }

        public override bool isLegal()
        {
            if (len > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override string toString()
        {
            return this.GetType() + " " + "len: " + len;
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
