using System;

namespace Variables
{
    class Program
    {
        static void Main(string[] args)
        {
            //integers 
            int age = 23; // 0s at the begining are not taken into account, for storing phone numbers, strings
            
           //for bigger numbers they have a very higher limit compared to ints
            long bigNumber = 900000000L; 

            //Decimals
            //like a standart float
            double negative = -55.2D; //D for 'default' so it's optionnal
            
            //like python, less precise but less memory taken
            float precision = 5.000001f; 
            
           // super precise, used for loads of decimals (like Money)
            decimal money = 14.99M; 

            //Txt
            string name = "Aba"; //chain of characters
            char letter = 'a';   // Just one letter

            // Bools
            bool isMale = true; // remember a just not '"T"rue'

            //prints
            Console.WriteLine(age);
            Console.WriteLine(money);
            Console.WriteLine(isMale);

            //Conversions
            string textAge = "-23";
            int convertedAge = Convert.ToInt32(textAge); //convert from the type you have ToTargetType
            string textMoney = "14.99";
            decimal convertedMoney = Convert.ToDecimal(textMoney);
        }
    }
}