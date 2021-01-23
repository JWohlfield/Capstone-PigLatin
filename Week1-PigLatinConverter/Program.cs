using System;

namespace Week1_PigLatinConverter
{
    /*
     Author:   Jeffrey Wohlfield
     Date  01-22-2021
     This program will prompt user for a word or sentence and convert their entry to a modified Pig Latin.

     Build Specifications:
        Convert each word to a lowercase before translating.
        If a word starts with a vowel, just add “way” onto the ending.
        if a word starts with a consonant, move all of the consonants that appear
        before the first vowel to the end of the word, then add “ay” to the end of the word.
    */
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Pig Latin Translator!");

            string input; string proceed = "y";

            while (proceed == "y")
            {
                Console.Write("Please enter a word or sentence: "); // Prompt for user input
                input = Console.ReadLine();
                
                //  Check if user entered data
                if (String.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Sorry, that entry was invalid\n");
                    continue;
                }
                
                Console.WriteLine();

                //  Create array of words from the input
                String[] words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (string word in words)
                {
                    Console.Write($"{ConvertWord(word)} ");
                }
                
                // Ask if user wants to continue
                Console.Write("\nContinue? (y/n) ");
                proceed = Console.ReadLine();
            }
        }

        public static bool ValidWord(string input)
        {
            //  setup special character string
            string special = "0123456789@";

            //determine if passed in string is a word
            for (int i = 0; i < input.Length; i++)
            {
                if(special.IndexOf(input.Substring(i, 1)) > -1)
                {
                    return false;
                }
            }
            return true;        
        }

        public static int GetVowelIndex(string input)
        {
            string vowels = "aeiou";

            //  Find where the position of the first vowel
            if (ValidWord(input))
            {            
               for (int i = 0; i < input.Length; i++)
               {
                    int vowelIndex = vowels.IndexOf(input.Substring(i, 1));
                    if (vowelIndex > -1)
                    {
                        return input.IndexOf(vowels.Substring(vowelIndex, 1));
                    }
                  if (i == 0)
                    {
                        // 'y' becomes a valid vowel anytime after the first letter
                        vowels += "y"; 
                    }
               }
            }
            return -1;
        }

        public static string ConvertWord(string input)
        {
            string inputLower = input.ToLower(); 
            string converted;

            int vowelIndex = GetVowelIndex(inputLower); // get vowel index to setup conversion
            string finalChar = input.Substring(input.Length - 1);

            switch (vowelIndex)
            {
                //word to be unchanged
                case -1:
                    converted = input;
                    break;
                
                //word that starts with a vowel
                case 0:
                    if (finalChar == "?" || finalChar == "!" || finalChar == ".") 
                    {
                        converted = input.Substring(vowelIndex,input.Length - 2) + "way" + finalChar;
                    }
                    else
                    {
                        converted = input + "way";
                    }
                    break;
                
                //word that starts with a consonant
                default:
                    if (finalChar == "?" || finalChar == "!" || finalChar == ".")
                    {
                        converted = input.Substring(vowelIndex, input.Length - 2) +
                            input.Substring(0, vowelIndex) + "ay" + finalChar;
                    }
                    else
                    {
                        converted = input.Substring(vowelIndex, (input.Length - vowelIndex)) +
                            input.Substring(0, vowelIndex) + "ay";
                    }
                    break;
            }

            return converted;
        }
    }
}
