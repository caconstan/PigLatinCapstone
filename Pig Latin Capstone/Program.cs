using System;

namespace Pig_Latin_Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("\nWelcome to the Pig Latin Translator!\n");
                Console.Write("Enter a line to be translated: ");
                string inputString = Console.ReadLine();

                while (String.IsNullOrEmpty(inputString))
                {
                    Console.WriteLine("I'm sorry, I didn't understand.  Please try again.");
                    Console.Write("Enter a line to be translated: ");
                    inputString = Console.ReadLine();
                }

                string[] delimiters = {" ", ",", "!", "?"};
                string pigLatin = PigLatinSentence(inputString, 0, delimiters );
                Console.WriteLine(pigLatin);
            } while (RunAgain("\nTranslate another line? (y/n)","y"));
        }

        static string PigLatinSentence(string sentence, int index, string[] delimiters)
        {
            string plSentence;
            bool lastDelimiter = (index == delimiters.Length-1) ? true : false;
            string curDelimiter = delimiters[index];

            //Console.WriteLine("s=" + sentence + ",index=" + index + ", delimiter" + curDelimiter );

            string[] words = sentence.Split( curDelimiter );
            
            if (lastDelimiter)
            {
                plSentence = PigLatin(words[0]);
            }
            else
            {
                plSentence = PigLatinSentence(words[0],index+1,delimiters);
            }

            for (int i=1; i < words.Length;i++)
            {
                plSentence += curDelimiter;
                if (lastDelimiter)
                {
                    plSentence += PigLatin(words[i]);
                }
                else
                {
                    plSentence += PigLatinSentence(words[i], index+1, delimiters);
                }
            }
            return plSentence;
        }

        static bool CheckWord(string word)
        {
            if (String.IsNullOrEmpty(word))
                return false;

            string alphabet = "abcdefghijklmnopqrstuvwxyz'";
            word = word.ToLower();
            char[] chars = word.ToCharArray();

            for (int i=0; i < chars.Length; i++)
            {
                if (!alphabet.Contains(chars[i]) )
                {
                    return false;// word has an disallowed char
                }
            }
            return true;// word is fine
        }

        static string PigLatin(string word)
        {
            if (word.EndsWith("."))
            {
                return PigLatin(word.Substring(0, word.Length - 1)) + ".";
            }

            if (!CheckWord(word))
            {   // don't translate words w numbers or symbols
                return word;
            }

            bool upperCase = word.ToUpper().Equals(word);
            bool lowerCase = word.ToLower().Equals(word);
            bool titleCase = !upperCase && !lowerCase;// my assumption

            // per requirement to lowercase the word
            word = word.ToLower();

            String plWord = word;
            char firstChar = word[0];
            string vowels = "aeiou";
            
            if (vowels.Contains(firstChar) )
            {
                plWord = word + "way";
            }
            else
            {
                int firstVowel = word.IndexOfAny(vowels.ToCharArray() );
                plWord = word.Substring(firstVowel) + word.Substring(0, firstVowel) + "ay";
            }

            if (upperCase)
                return plWord.ToUpper();
            else if (titleCase)
            {
                return char.ToUpper(plWord[0]) + plWord.Substring(1);
            }
            else //lowercase to begin with
            {
                return plWord;
            }
        }

        static bool RunAgain(string sentence, string cont)
        {
            Console.Write(sentence);
            string response = Console.ReadLine().Trim();
            if (response == cont)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
