using System;

namespace FunctionsExercise
{
    class Program
    {
        static void Main(string[] args)
        {
        
            //ask user for width
            Console.Write("Enter width: ");
            int width = Convert.ToInt32(Console.ReadLine());

            //ask user for height
            Console.Write("Enter height: ");
            int height = Convert.ToInt32(Console.ReadLine());

            //call function and store result
            float result = CalculateArea(width, height);

            //print out the area
            Console.WriteLine("The area is: " + result);



            //test array with numbers
            int[] numbers = new int[] { 10, 20, 30 };
            
            int sum = SumOfNumbers(numbers);

            //check return in main
            if(sum != -1)
            {
                Console.WriteLine($"Total: {sum}");
            }
            else
            {
                Console.WriteLine("Array cannot be empty!");
            }


            //test empty array
            int[] emptyNumbers = new int[] { };
            
            result = SumOfNumbers(emptyNumbers);

            if(result != -1)
            {
                Console.WriteLine($"Total: {sum}");
            }
            else
            {
                Console.WriteLine("Array cannot be empty!");
            }

            Console.ReadLine();
        }

        

   
        static float CalculateArea(int width, int height)
        {
            return (width * height) / 2f;
        }

        static int SumOfNumbers(int[] numbers)
        {
            //extra check array length
            if(numbers.Length == 0)
            {
                return -1;
            }

            int total = 0;
            
            foreach (var item in numbers)
            {
                total += item;
            }

            return total;
        }
    }
}