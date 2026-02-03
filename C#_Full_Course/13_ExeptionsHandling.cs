using System;

namespace ExceptionHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            bool Looping = true;
            while(Looping)
            {
            //try block contains code that might crash
            try
            {
                Console.Write("Enter a number: ");
                int num = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"Your value is {num}");

                Looping = false;
            }
            //specific catch: when user enters text instead of number
            catch (FormatException)
            {
                Console.WriteLine("Please enter only numbers!");
            }
            //specific catch: when number is too large for int
            catch (OverflowException)
            {
                Console.WriteLine("Value is too large!");
            }
            //general catch & printing error message; 'e' to get sys details as .py
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            //finally block always executes
            }

            Console.WriteLine("End of program");

            Console.ReadLine();
        }
    }
}