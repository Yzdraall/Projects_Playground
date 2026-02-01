using System;
using System.Collections.Generic;

namespace ListsExercices
{
    class Program
    {
        static void Main(string[] args)
        {
            //create two lists to hold the data
            List<int> even = new List<int>();
            List<int> odd = new List<int>();

            
            for (int i = 0; i <= 20; i++)
            {
                //check if even
                if (i % 2 == 0)
                {
                    even.Add(i);
                }
                else
                {
                    odd.Add(i);
                }
            }

            //print even list
            Console.WriteLine("Even numbers:");
            foreach (var item in even)
            {
                Console.Write($"{item} ");
            }

            Console.WriteLine("\n");

            //print odd list
            Console.WriteLine("Odd numbers:");
            foreach (var item in odd)
            {
                Console.Write($"{item} ");
            }



            ////////////arrayMultiples////////////

            Console.WriteLine("\n");

            int num = 7;
            int length = 5;

            //declare array with fixed size 'length'
            int[] result = new int[length];
            //counter for indexing
            int counter = 0;

            //loop from 1 up to length and multiply num by i
            for (int i = 1; i <= length; i++, counter++)
            {
                result[counter] = num * i;
            }

            //print result
            foreach (int item in result)
            {
                Console.Write($"{item} ");
            }


            Console.ReadLine();
        }
    }
}