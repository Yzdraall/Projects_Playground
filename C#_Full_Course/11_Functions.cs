using System;

namespace Functions
{
    class Program
    {
        static void Main(string[] args)
        {
            //call
            WelcomeMessage();

            CreateAndPrintArray();
            Console.WriteLine();

            //return type examples
            PrintIntroduction();
            
            //capturing return values
            string name = ReturnName();
            int age = ReturnAge();

            //using return values directly
            Console.WriteLine(ReturnNameAgePair());

            //using return value to set console property
            Console.Title = ReturnNameAgePair();

           
            //filling an array using the function
            int[] numbers = new int[3];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = ReadNumberFromConsole();
            }
            foreach (var item in numbers)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();

            //getting an entire array back from a function
            int[] newNumbers = CreateRandomArray();
            foreach (var item in newNumbers)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();

            //simple calculation return
            int result = Add();
            Console.WriteLine(result);

            Console.ReadLine();
        }


        //void = returns nothing
        static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to my program!");
        }

        static void CreateAndPrintArray()
        {
            int[] numbers = new int[] { 0, 1, 2 };

            foreach (var item in numbers)
            {
                Console.Write($"{item} ");
            }
        }

        static void PrintIntroduction()
        {
            Console.WriteLine($"Hello my name is {ReturnName()}, and i'm {ReturnAge()}");
        }

        //string return
        static string ReturnName()
        {
            return "Aba";
        }

        //int return
        static int ReturnAge()
        {
            return 23;
        }

        //calling other functions inside a return
        static string ReturnNameAgePair()
        {
            return ReturnName() + "-" + ReturnAge();
        }

        //helper to read and convert input
        static int ReadNumberFromConsole()
        {
            Console.Write("Enter a number: ");
            return Convert.ToInt32(Console.ReadLine());
        }

        //returning an array
        static int[] CreateRandomArray()
        {
            return new int[] { 0, 1, 2 };
        }

        //calculation return
        static int Add()
        {
            return 5 + 5;
        }
    }
}