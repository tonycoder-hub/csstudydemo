using System;
using System.Collections;

namespace ConsoleAppMar19th
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // Demo from class
            GenericList<int> intList = new GenericList<int>();
            for(int i = 0; i < 100; i++)
            {
                intList.add(i);
            }
            //for (Node<int> node = intList.head;
            //     node != null; node = node.next)
            //{
            //    Console.WriteLine(node.data);
            //}
            GenericList<string> strList = new GenericList<string>();
            for (int i = 0; i < 10; i++)
            {
                strList.add("str "+i);
            }
            //for (Node<string> node = strList.head;
            //     node != null; node = node.next)
            //{
            //    Console.WriteLine(node.data);
            //}
            // I designed to use a new array to temporarily fit these data
            // But this means that it would use double a space to do the same thing
            //foreach(int i in intList)
            //{
            //    Console.WriteLine(i);
            //}
            Calculator calculator = new Calculator();
            Console.WriteLine("Content:");
            foreach(int i in intList)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            Console.WriteLine("max: "+calculator.max(intList));
            Console.WriteLine("min: " + calculator.min(intList));
            Console.WriteLine("avg: " + calculator.avg(intList));
            Console.WriteLine("total: " + calculator.total(intList));





        }
       
    }
    class Node<T>
    {
        public Node<T> next { get; set; }
        public T data { get; set; }
        public Node(T t)
        {
            next = null;
            data = t;
        }
    }
    class GenericList<T> : IEnumerable
    {
        public Node<T> head { get; set; }
        private Node<T> tail;
        public int length = 0;
        public GenericList()
        {
            tail = head = null;
        }
        public void add(T t)
        {
            Node<T> node = new Node<T>(t);
            if (tail == null)
            {
                head = tail = node;
            }
            else
            {
                tail.next = node;
                tail = node;
            }
            length++;
        }
        private T[] getArray()
        {
            if(tail == null)
            {
                return null;
            }
            else
            {
                T[] array = new T[length];
                Node<T> node = this.head;
                for(int i = 0; i < length;i++)
                {
                    array[i] = node.data;
                    node = node.next;
                }
                return array;
            }
        }
        public IEnumerator GetEnumerator()
        {
            return new MyEnumerator<T>(getArray());
        }
        
  
    }
    class Calculator
    {
        public delegate double calculators(GenericList<int> genericList);
        static double GetAvg(GenericList<int> genericList)
        {
            return Calculator.GetTotal(genericList) / genericList.length;
        }
        static double GetTotal(GenericList<int> genericList)
        {
            int total = 0;
            foreach (int t in genericList)
            {
                total += t;
            }
            return total;
        }
        static double GetMax(GenericList<int> genericList)
        {
            int temp = genericList.head.data;
            foreach (int t in genericList)
            {
               if (t > temp)
                {
                    temp = t;
                }
            }
            return temp;
        }
        static double GetMin(GenericList<int> genericList)
        {
            int temp = genericList.head.data;
            foreach (int t in genericList)
            {
                if (t < temp)
                {
                    temp = t;
                }
            }
            return temp;
        }
        public calculators avg
        {
            get { return new calculators(GetAvg); }
        }
        public calculators total
        {
            get { return new calculators(GetTotal); }
        }
        public calculators max
        {
            get { return new calculators(GetMax); }
        }
        public calculators min
        {
            get { return new calculators(GetMin); }
        }
    }
    class MyEnumerator<T> : IEnumerator
    {
        private int i = 0;
        private T[] ts;
        public MyEnumerator(T[] ts){
            this.ts = ts;   
        }
        object IEnumerator.Current {
            get { return ts[i]; }
        }

        bool IEnumerator.MoveNext()
        {
            i++;
            return i < ts.Length ? true:  false;
        }

        void IEnumerator.Reset()
        {
        i = -1;
        }
    }

}
