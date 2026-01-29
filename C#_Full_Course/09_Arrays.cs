using System;

namespace ArrayBasics
{
    class Program 
    {
        static void Main(string[] args)   
        {
            //trying to decide if the figure is a triangle sum of angles = 180

            //declare array of size 5, default values are 0
            int[] numbers = new int[5];

            //the hard way
            Console.Write("Enter a number: ");
            numbers[0] = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter a number: ");
            numbers[1] = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter a number: ");
            numbers[2] = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter a number: ");
            numbers[3] = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter a number: ");
            numbers[4] = Convert.ToInt32(Console.ReadLine());

            //output using concatenation
            Console.WriteLine($"{numbers[0]} {numbers[1]} {numbers[2]}");


            const int angleCount = 3;
            int[] angles = new int[angleCount];

            for (int i = 0; i < angles.Length; i++)
            {
                Console.Write($"Enter angle {i + 1}: ");
                angles[i] = Convert.ToInt32(Console.ReadLine());
            }



            //array sort
            int angleSum = 0;

            //foreach loop to read data
            //cleaner syntax when index is not needed
            foreach (int angle in angles)
            {
                angleSum += angle; 
            }

            Console.WriteLine(angleSum == 180 ? "Valid" : "Invalid!");

            //initializing array with values directly
            string[] movies = { "Lord of the Rings", "Fight Club", "Interstellar", "Oppeinheimer" };

            //sorts alphabeticaly or numericaly
            Array.Sort(movies);

            Console.WriteLine("--- Sorted (A-Z) ---");
            foreach (string movie in movies)
            {
                Console.WriteLine(movie);
            }

            //reverses the current order of elements
            Array.Reverse(movies);

            Console.WriteLine("\n--- Reversed (Z-A) ---");
            foreach (string movie in movies)
            {
                Console.WriteLine(movie);
            }

            //example with numbers
            int[] numbers = { 10, 5, 200, 1 };
            Array.Sort(numbers);
            

            //array clear

            int[] numbers = { 1, 2, 3, 4, 5, 6 };

            
            //Array.Clear(array, starting, length);
            Array.Clear(numbers, 0, 2);

            foreach (int num in numbers)
            {
                Console.Write($"{num} ");
            }

            Console.WriteLine();

            Array.Clear(numbers, 0, numbers.Length);

            foreach (int num in numbers)
            {
                Console.Write($"{num} ");
            }


        //IndexOf
        int[] numbers = { 10, 20, 30, 40, 50 };

            Console.Write("Enter number to find: ");
            int search = Convert.ToInt32(Console.ReadLine());

            //Array.IndexOf(array, valueToFind, begin, lenght)
            int position = Array.IndexOf(numbers, search);

            //check the result
            if (position > -1)
            {
                Console.WriteLine($"Number {search} found at index {position}.");
            }
            else
            {
                Console.WriteLine($"Number {search} does not exist in the array.");
            }

            Console.ReadLine();
        }
    }
}