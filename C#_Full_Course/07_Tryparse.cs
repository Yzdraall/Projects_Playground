using System;

namespace TryParseExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //tryparse prevents crashing if user types text instead of numbers
            Console.Write("Enter a number: ");
            string input = Console.ReadLine();

            int num;

            //int.TryParse(input, out result_variable)
            bool success = int.TryParse(input, out num);

            if (success)
            {
                Console.WriteLine($"Success! Number is {num}"); //$ in C# = f in py
            }
            else
            {
                Console.WriteLine("Failed to convert");
            }
        bool success = true;

            //runs until user enters a valid number
            while (success)
            {
                Console.Write("Enter a number: ");
                string numInput = Console.ReadLine();

                //variable 'num' is declared inside the tryparse line
                if (int.TryParse(numInput, out int num))
                {
                    success = false; //stops the loop
                    Console.WriteLine(num);
                }
                else
                {
                    Console.WriteLine("Failed to convert!");
                }
            }

            Console.ReadLine();
        }
    }
}