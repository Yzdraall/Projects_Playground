using System;

namespace Structures
{
    class Program
    {
        //struct def
        struct Person
        {
            public string name;
            public int age;
            public int birthMonth;
            public int number;

            //constructor to initialize struct
            public Person(string name, int age, int birthMonth, int number)
            {
                this.name = name;
                this.age = age;
                this.birthMonth = birthMonth;
                this.number = number;
            }
        }

        static void Main(string[] args)
        {
            /* the 'usual' no struct method
            string newName = "";
            int newAge = 0;
            int newBirthMonth = 0;
            
            ReturnPerson(ref newName, ref newAge, ref newBirthMonth);
            Console.WriteLine($"{newName} - {newAge} - {newBirthMonth}");
            */

            //new way
            //we get 'person' that has every info
            Person person = ReturnPerson();

            Console.WriteLine($"{person.name} - {person.age} - {person.birthMonth} - {person.number}");

            Console.ReadLine();
        }

        //funtion to updatre methods
        static Person ReturnPerson()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            Console.Write("Enter your age: ");
            int age = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter your birthmonth: ");
            int birthMonth = Convert.ToInt32(Console.ReadLine());
            
            Console.Write("Enter your number: ");
            int number = Convert.ToInt32(Console.ReadLine());

            //returning the new person instance
            return new Person(name, age, birthMonth, number);
        }

        /*
        static void ReturnPerson(ref string name, ref int age, ref int birthMonth)
        {
            Console.Write("Enter your name: ");
            name = Console.ReadLine();

            Console.Write("Enter your age: ");
            age = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter your birthmonth: ");
            birthMonth = Convert.ToInt32(Console.ReadLine());
        }
        */
    }
}