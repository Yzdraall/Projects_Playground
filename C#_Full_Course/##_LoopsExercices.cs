using System;

namespace LoopExercises
{
    class Program
    {
        static void Main(string[] args)
        {
            //TimeTables
            Console.Write("Enter a number for the table: ");
            string input = Console.ReadLine();
            int number;

            //using tryparse for safety
            if (int.TryParse(input, out number))
            {
                //loop 1 to 10
                for (int i = 1; i <= 10; i++)
                {
                    //math logic: input * counter
                    Console.WriteLine("{0} x {1} = {2}", number, i, number * i);
                }
            }
            else
            {
                Console.WriteLine("Invalid number");
            }

            Console.WriteLine(); //spacer

            //FizzBuzz
            //1 to 15. div by 3=Fizz, div by 5=Buzz, both=FizzBuzz
            for (int i = 1; i <= 15; i++)
            {
                //check strictly for both first (most specific condition)
                if (i % 3 == 0 && i % 5 == 0)
                {
                    Console.WriteLine("FizzBuzz");
                }
                else if (i % 3 == 0)
                {
                    Console.WriteLine("Fizz");
                }
                else if (i % 5 == 0)
                {
                    Console.WriteLine("Buzz");
                }
                else
                {
                    Console.WriteLine(i);
                }
            }
            
            bool threeDiv = false;
            bool fiveDiv = false;

            //memory friendly version, does less calculs for the same results
            for (int i = 1; i <= 15; i++)
            {
                //calculate divisibility once per iteration
                threeDiv = i % 3 == 0;
                fiveDiv = i % 5 == 0;

                //check booleans instead of doing math again
                if (threeDiv && fiveDiv)
                {
                    Console.WriteLine("FizzBuzz");
                }
                else if (threeDiv)
                {
                    Console.WriteLine("Fizz");
                }
                else if (fiveDiv)
                {
                    Console.WriteLine("Buzz");
                }
                else
                {
                    Console.WriteLine(i);
                }
            }


        }
    }
}