/* 

Todo
- Gränsnittet:Refresha sidan + färger

- Refactoring: metod, 1-7 rader lång cirka + förklarar sig själv

 */
using Hangman2.Methods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Hangman2
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            // Declare variables
            // * array with words
            // * array to save input
            // Generate a word
            // Check input (valid char) + already guessed char 
            // Check if word is complete or no more guesses
            // Print game screen:
            // Guessed letter 
            // Guesses left
            // Correct/Wrong guess
            // Number of tries left
            // You won / you lost screen

           
            
            HashSet<char> guessedLetters = new HashSet<char>();
            string[] wordArray = new string[] { "GRUNDSKOLA", "GARAGE", "PROGRAMMERING", "GAVEL", "TELEFON", "FLYGPLAN", "TAVLA" };

            Console.WriteLine("Hangman game\n");

            Random randGen = new Random();
            var idx = randGen.Next(0, 6);
            string hemligtord = wordArray[idx];


            char[] allCorrectLetters = new char[hemligtord.Length];
            bool rättord = false;  //#Dont mix english and swedish...
            int guessCount = 0;

            //METODER
            //Maskera hemligt ord och printa _
            PrintAndMaskWord(allCorrectLetters);

            
            //Test printout
            Console.WriteLine("Hemligtord: " + hemligtord);
            Console.WriteLine("guessedletter.length: " + guessedLetters.Count);

            

            //Loopa igenom tills
            while (!rättord)
            {

                Console.Write("\nGissa bokstav: ");
                
                string input = Console.ReadLine().ToUpper();

                    Console.Clear();
                if (ValidateInput(input)) {

                    guessedLetters.Add(Convert.ToChar(input[0]));
                    guessCount++;

                    // Match if input exists in "hemligtord"
                    MatchInputAndHemligtord(hemligtord, input, allCorrectLetters); //#rename "hemligtord..."to english

                    //Printar gissade bokstäver //#please dont mix english and swedish.
                    PrintAllCorrectLetters(allCorrectLetters); //#currently prints all guesses and not the correct ones?

                    // Print out guessed letters
                    PrintOutAllGuessedLetters(guessedLetters); 

                    // Check if guessed letters matches secret word
                    if (CheckIfWon(allCorrectLetters, hemligtord))
                    {
                        break;
                    }

                    //Declare and init max number of guesses and check if reached
                    if (MaxGuessCalc(guessCount)) {
                        break;
                    }


                }

            }

        }

        private static bool MaxGuessCalc(int guessCount)
        {
            int tries = 10;

            if (guessCount >= tries)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You guessed too many times!");
                Console.ResetColor();

                return true;
            }
            return false;
        }

        private static bool CheckIfWon(char[] allCorrectLetters, string hemligtord)
        {

            foreach (var ch in allCorrectLetters)
            {
                string guessedLetters = new string(allCorrectLetters);//no need for this code? see below
                if (guessedLetters.Equals(hemligtord))              //#if guessedLetter.ToString().Equals(Hemligtord)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nYOU WON!");
                    Console.ResetColor();
                    return true;
                }
            }
            
            return false;
            
        }

        private static void PrintOutAllGuessedLetters(HashSet<char> allGuessedLetters)
        {
            Console.WriteLine();
            foreach (char ltr in allGuessedLetters)
            {
                Console.Write(ltr + " ");

            }
        }

        private static void PrintAllCorrectLetters(char[] correctletters) //#why not use a one-liner to print this in the code above?
        {
            Console.WriteLine(correctletters);
        }

        private static void MatchInputAndHemligtord(string hemligtord, string input, char[] gissatord)
        {
            for (int i = 0; i < hemligtord.Length; i++)
            {
                if (hemligtord[i].ToString().Contains(input))
                {
                    gissatord[i] = hemligtord[i];
                }
            }
        }

        private static bool ValidateInput(string input)
        {
            if (input.Length > 1 || !Regex.IsMatch(input.ToString(), @"^[a-zA-Z]+$"))
            {
                    Console.ForegroundColor = ConsoleColor.Red;
                if (input.Length > 1)
                {
                    Console.WriteLine("Wrong input, max 1 letter!");
                }
                if (!Regex.IsMatch(input.ToString(), @"^[a-zA-Z]+$"))
                {
                    Console.WriteLine("Wrong input, only letters accepted!");
                }
                Console.ResetColor();
                    return false;
            }
            else
                return true;
        }

        private static void PrintAndMaskWord(char[] gissatord) //#gissatord betyder?kanske ändra namn? 
        {
            for (int i = 0; i < gissatord.Length; i++)
            {
                gissatord[i] = '_'; //# doesnt this line of code overwrite the gissatord sent in to this method?
                Console.Write(gissatord[i] + " ");
            }
        }
    }
}
