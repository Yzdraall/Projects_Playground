using System;

namespace ControlFlow
{
    class Program
    {
        static void Main(string[] args)
        {
            //User input
            //input()
            Console.Write("Enter your name: "); 
            string nameInput = Console.ReadLine(); // ReadLine() returns a string

            Console.Write("Enter your age: ");
            string ageInput = Console.ReadLine();
            int age = Convert.ToInt32(ageInput); // to compare, you have to covert to int



            if (age >= 18 && age <= 25) //parenthesis are mandatory if (cdt) {then}
            // && = 'and' ; || = 'or' 
            {
                Console.WriteLine("You are between 18 and 25.");
            }
            else if (age >= 26)
            {
                Console.WriteLine("You are 26 or older.");
            }
            else
            {
                Console.WriteLine("You are too young.");
            }
            

            int day = 1;

            switch (day)
            {
                case 1:
                    Console.WriteLine("Monday");
                    break; // mandatory, stops code from falling into the next case
                case 2:
                    Console.WriteLine("Tuesday");
                    break;
                default: // Like the 'else' catch-all
                    Console.WriteLine("Another day");
                    break;
            }
        }
    }
}