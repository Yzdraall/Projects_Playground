using System;
using System.Collections.Generic;

namespace ListBasics
{
    class Program 
    {
        static void Main(string[] args)   
        {
            //creating a list of integers
            List<int> listNumbers = new List<int>();

            for (int i = 0; i < 3; i++)
            {
                Console.Write("Enter a number: ");
                //.Add() = .append()
                listNumbers.Add(Convert.ToInt32(Console.ReadLine()));
            }

            // !! .Count, not .Length
            for (int i = 0; i < listNumbers.Count; i++)
            {
                Console.WriteLine(listNumbers[i]);
            }

            //removing specific item by index
            listNumbers.RemoveAt(0);

            foreach (var item in listNumbers)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();




            //// Dictionaries ////
            
            
            //dictionary<key type, value type>
            Dictionary<int, string> names = new Dictionary<int, string>
            {
                //keyvaluepair
                { 1, "Aba" },
                { 2, "Test" },
                { 3, "Test" } //keys must be unique, values can duplicate
            };

            //iterating using for loop (rare, needs linq)
            for (int i = 0; i < names.Count; i++)
            {
                KeyValuePair<int, string> pair = names.ElementAt(i);
                Console.WriteLine($"{pair.Key} - {pair.Value}");
            }

            Console.WriteLine();


            foreach (KeyValuePair<int, string> item in names)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }



            Dictionary<string, string> teachers = new Dictionary<string, string>
            {
                { "Math", "Aba" },
                { "Science", "Test" }
            };

            //trygetvalue prevents crash if key doesn't exist
            //if found, puts value in variable 'teacher'
            if (teachers.TryGetValue("Math", out string teacher))
            {
                Console.WriteLine(teacher);
                
                //update value using the key
                teachers["Math"] = "Joe";
            }
            else
            {
                Console.WriteLine("Math teacher not found");
            }

            //remove logic using containskey
            if (teachers.ContainsKey("Math"))
            {
                teachers.Remove("Math");
            }
            else
            {
                Console.WriteLine("Math not found");
            }

            //foreach using 'var'
            foreach (var item in teachers)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }


            Console.ReadLine();
        }
    }
}