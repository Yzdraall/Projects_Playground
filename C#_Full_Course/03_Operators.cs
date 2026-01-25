using System;

namespace Operators
{
    class Program
    {
        static void Main(string[] args)
        {
            int age = 23;

            //increment n decrement
            age++;  //+ 1 to variable
            age--;  //- 1 to variable

            age = age + 10; 

            age += 10; //shortcut syntax as python

            //divisions
            int num1 = 10;
            int num2 = 3;
            int remainder = num1 % num2; //= 1 (10 div by 3 is 3, remainder of 1)
            
            Console.WriteLine(remainder);

            //even or odd logic
            int number = 10;
            if (number % 2 == 0) 
            {
                Console.WriteLine("Even");
            }
            else 
            {
                Console.WriteLine("Odd");
            }
        }
    }
}