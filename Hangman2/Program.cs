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


            // Print start screen
            Console.ForegroundColor = ConsoleColor.White;
            PrintStartScreen();
            
            //Select a randomized secretword from wordArray.
            Random randGen = new Random();
            var idx = randGen.Next(0, 6);
            string secretWord = wordArray[idx];

            //Instanciate variables and create new "Hangman" core that handles "numberoftries" to guess, depending on length of word
            char[] allCorrectLetters = new char[secretWord.Length];
            bool correctWord = false; 
            int guessCount = 0;
            var hangman = new Core.Hangman(secretWord);


            //Test printout
            //Console.WriteLine("Secret word: " + secretWord);
            //Console.WriteLine("guessedletter.length: " + guessedLetters.Count);

            //Print first screen that masks words, then print number of tries left.
            PrintAndMaskWord(allCorrectLetters);
            PrintNumberOftries(hangman, guessCount);

            //Loopa igenom tills
            while (!correctWord)
            {
                //First input to check if input is valid.
                Console.Write("\nGissa bokstav: ");
                string input = Console.ReadLine().ToUpper();
                Console.Clear();
                Console.WriteLine();
                if (ValidateInput(input, guessedLetters))
                {

                    //Add valid input to guessedletters hashset, increment guesscount.
                    guessedLetters.Add(Convert.ToChar(input[0]));
                    guessCount++;


                    // Match if input exists in "secretWord"
                    MatchInputAndHemligtord(secretWord, input, allCorrectLetters);

                    //Printar gissade bokstäver //#please dont mix english and swedish.
                    PrintUnmaskedAndGuessedLetters(allCorrectLetters, guessedLetters); //#currently prints all guesses and not the correct ones?
                    PrintNumberOftries(hangman, guessCount);


                    // Print out guessed letters
                    //  PrintOutAllGuessedLetters(guessedLetters); 

                    // Check if guessed letters matches secret word
                    if (CheckIfWon(allCorrectLetters, secretWord))
                    {
                        break;
                    }

                    //Declare and init max number of guesses and check if reached
                    if (MaxGuessCalc(guessCount, hangman.numberOfTries))
                    {
                        break;
                    }


                }
                else
                {
                    Console.Clear();
                    Console.WriteLine();
                    //Printar gissade bokstäver //#please dont mix english and swedish.
                    PrintUnmaskedAndGuessedLetters(allCorrectLetters, guessedLetters); //#currently prints all guesses and not the correct ones?
                    PrintNumberOftries(hangman, guessCount);
                }

            }

        }

        private static void PrintStartScreen()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(@"╒══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╕");
            Console.WriteLine(@"│                                                                                                                      │");
            Console.WriteLine(@"│                                                                                                                      │");
            Console.WriteLine(@"│                                                                                                                      │");
            Console.WriteLine(@"│                                             Welcome to Hangman game                                                  │");
            Console.WriteLine(@"│                                                                                                                      │");
            Console.WriteLine(@"│                                                                                                                      │");
            Console.WriteLine(@"│                                                                                                                      │");
            Console.WriteLine(@"│                                                                                                                      │");
            Console.WriteLine(@"│                                                    +---+                                                             │");
            Console.WriteLine(@"│                                                    |   |                                                             │");
            Console.WriteLine(@"│                                                    O   |                                                             │");
            Console.WriteLine(@"│                                                   /|\  |                                                             │");
            Console.WriteLine(@"│                                                   / \  |                                                             │");
            Console.WriteLine(@"│                                                        |                                                             │");
            Console.WriteLine(@"│                                                 =========''']                                                        │");
            Console.WriteLine(@"│                                                                                                                      │");
            Console.WriteLine(@"│                                                                                                                      │");
            Console.WriteLine(@"│                                                                                                                      │");
            Console.WriteLine(@"│                                                                                                                      │");
            Console.WriteLine(@"│                                                                                                                      │");
            Console.WriteLine(@"│                                                                                                                      │");
            Console.WriteLine(@"╘══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╛");
            Console.WriteLine(@"                                              Press enter to continue...");
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine();
        }

        private static void PrintNumberOftries(Core.Hangman hangman, int guessCount)
        {
            Console.WriteLine($"\n\nNumber of tries left: {hangman.numberOfTries - guessCount}");
        }

        private static bool MaxGuessCalc(int guessCount, int tries)
        {

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

        private static void PrintUnmaskedAndGuessedLetters(char[] correctletters, HashSet<char> allGuessedLetters) //#why not use a one-liner to print this in the code above?
        {
            // Console.WriteLine(correctletters);


            foreach (char ltr in correctletters)
            {

                Console.Write(ltr + " ");

            }

            Console.WriteLine();
            foreach (char ltr in allGuessedLetters)
            {

                Console.Write(ltr + " ");

            }
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

        private static bool ValidateInput(string input, HashSet<char> guessedLetters)
        {
            if (input.Length > 1 || !Regex.IsMatch(input.ToString(), @"^[a-zA-Z]+$"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                if (input.Length > 1)
                {
                    Console.WriteLine("Wrong input, max 1 letter!\nPress enter to continue.");
                    Console.ReadLine();

                }
                if (!Regex.IsMatch(input.ToString(), @"^[a-zA-Z]+$"))
                {
                    Console.WriteLine("Wrong input, only letters accepted!\nPress enter to continue.");
                    Console.ReadLine();

                }
                Console.ForegroundColor = ConsoleColor.White;

                return false;
            }
            else if (guessedLetters.Contains(input[0]))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Letter is already guessed!\nPress enter to continue.");
                Console.ReadLine();
                Console.ResetColor();
                return false;
            }
            else


                return true;
        }

        private static void PrintAndMaskWord(char[] guessedLetters) //#gissatord betyder?kanske ändra namn? 
        {
            for (int i = 0; i < guessedLetters.Length; i++)
            {
                guessedLetters[i] = '_'; //# doesnt this line of code overwrite the gissatord sent in to this method?
                Console.Write(guessedLetters[i] + " ");
            }
        }
    }
}
