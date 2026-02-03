using System;

namespace ClassScope
{
    class Program
    {
        static void Main(string[] args)
        {
            //instantiate
            Person person = new Person("Aba", 25);
            Console.WriteLine($"Created: {person}");

            //validation check
            person.Name = ""; 
            Console.WriteLine($"Name check: {person.Name}");

            person.Age = 200; 
            Console.WriteLine($"Age check: {person.Age}");

            //equality check
            Person twin = new Person("Aba", 25); //reset values

            if (person.Equals(twin))
            {
                Console.WriteLine("Same person.");
            }
            else
            {
                Console.WriteLine("Different people.");
            }

            Console.ReadLine();
        }
    }

    class Person
    {
        //fields
        private string name;
        private int age;

        //constructor
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        //properties with lambda logic
        public string Name
        {
            get => name;
            set => name = !string.IsNullOrEmpty(value) ? value : "Invalid Name";
        }

        public int Age
        {
            get => age;
            set => age = value >= 0 && value <= 150 ? value : -1;
        }

        //methods
        public string ReturnDetails()
        {
            return $"Name: {Name} - Age: {Age}";
        }

        //overrides
        public override string ToString()
        {
            return $"[Person] Name: {Name}\tAge: {Age}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Person)
            {
                Person other = (Person)obj;
                return Name == other.Name && Age == other.Age;
            }
            return false;
        }
    }
}