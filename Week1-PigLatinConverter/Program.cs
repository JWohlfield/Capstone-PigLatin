using System;
using System.Linq;

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

        public static int GetEndPunctuationIndex(string input)
        {
            int endIndex = -1;

            if(input.EndsWith('!') || input.EndsWith('?') || input.EndsWith('.'))
            {
                string EndPunctuation = "!?.";

                for (int i = input.Length - 1; i >= 0; i--)
                {
                    if(EndPunctuation.Contains(input.Substring(i, 1)))
                    {
                        endIndex = i;
                    }
                }
            }

            return endIndex;
        }

        public static string ConvertWord(string input)
        {
            string inputLower = input.ToLower(); 
            string converted;

            // get vowel index to setup conversion
            int vowelIndex = GetVowelIndex(inputLower); 
            // get punctuation index
            int startPunctuationIndex = GetEndPunctuationIndex(inputLower);
            int endOfWordIndex = input.Length - startPunctuationIndex;

            switch (vowelIndex)
            {
                //word to be unchanged
                case -1:
                    converted = input;
                    break;
                
                //word that starts with a vowel
                case 0:
                    if(startPunctuationIndex > -1)    
                    {
                        converted = input.Substring(vowelIndex, startPunctuationIndex) + "way" +
                            input.Substring(startPunctuationIndex, endOfWordIndex);
                    }
                    else
                    {
                        converted = input + "way";
                    }
                    break;
                
                //word that starts with a consonant
                default:
                    if (startPunctuationIndex > -1)
                    {
                        converted = input.Substring(vowelIndex, startPunctuationIndex - vowelIndex) +
                            input.Substring(0, vowelIndex) + "ay" + input.Substring(startPunctuationIndex, endOfWordIndex);
                    }
                    else
                    {
                        converted = input.Substring(vowelIndex, (input.Length - vowelIndex)) +
                            input.Substring(0, vowelIndex) + "ay";
                    }
                    break;
            }
            // Adjust case positionally of converted word
            return CaseAdjust(input,converted);
        }

        public static string CaseAdjust(string input, string converted)
        {
            input = input.Trim();
            converted = converted.Trim();
            string adjusted = "";

            for (int i = 0; i < input.Length; i++)
            {
                   if(char.IsUpper(input[i]))
                    {
                        adjusted += char.ToUpper(converted[i]);
                    }
                   else
                    {
                        adjusted += char.ToLower(converted[i]);
                    }
            }

            if(converted.Length > adjusted.Length)
            {
                adjusted += converted.Substring(input.Length, converted.Length - input.Length);
            }

            return adjusted;
        }
    }
}
