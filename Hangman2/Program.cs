// OO: Remove usings that aren't used - FIXAT
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Hangman2
{
    class Program
    {
        static void Main(string[] args)
        {
        
            HashSet<char> guessedLetters = new HashSet<char>();
           
            // Print start screen
            Console.ForegroundColor = ConsoleColor.White;
            PrintStartScreen();

            // OO: Remove the comment and create a method instead - FIXAT
           //Select a randomized secretword from wordArray.
          string secretWord =  createRandomSecretWord();

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

            // OO: I suggest you remove all comments below since you have quite good names for your method - FIXAT

          
            while (!correctWord)
            {
                
                Console.Write("\nGuess letter: ");

                string input = Console.ReadLine().ToUpper();
                Console.Clear();
              
                if (ValidateInput(input, guessedLetters))
                {                    
                    guessedLetters.Add(Convert.ToChar(input[0]));
                    guessCount++;
                    MatchInputAndSecretWord(secretWord, input, allCorrectLetters);                    
                 
                    PrintNumberOftries(hangman, guessCount);

                    if (CheckIfWon(allCorrectLetters, secretWord))
                    {
                        break;
                    }
                  
                    if (MaxGuessCalc(guessCount, hangman.numberOfTries))
                    {
                        Console.WriteLine("The correct word is: " + secretWord);
                        break;
                    }

                }
                else
                {                  
                    
                }
             
                Console.Clear();
                Console.WriteLine();
     
                PrintUnmaskedAndGuessedLetters(allCorrectLetters, guessedLetters);
                PrintNumberOftries(hangman, guessCount);
            }

        }

        private static string createRandomSecretWord()
        {
            string[] wordArray = new string[] { "GRUNDSKOLA", "GARAGE", "PROGRAMMERING", "GAVEL", "TELEFON", "FLYGPLAN", "TAVLA" };

            Random randGen = new Random();
            var idx = randGen.Next(0, 6);
            return wordArray[idx];
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
                PrintGameOverScreen();             
                return true;
            }
            return false;
        }

        private static void PrintGameOverScreen()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You loose, better luck next time!");
            Console.ResetColor();
        }

        private static bool CheckIfWon(char[] allCorrectLetters, string secretWord)
        {

            foreach (var ch in allCorrectLetters)
            {
                string guessedLetters = new string(allCorrectLetters);//no need for this code? see below
                if (guessedLetters.Equals(secretWord))             //#if guessedLetter.ToString().Equals(Hemligtord)
                {
                    PrintYouWinScreen();
                    return true;
                }
            }

            return false;

        }

        private static void PrintYouWinScreen()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nYOU WON!");
            Console.ResetColor();
        }

      
        private static void PrintUnmaskedAndGuessedLetters(char[] correctletters, HashSet<char> allGuessedLetters) //#why not use a one-liner to print this in the code above?
        {
            // Console.WriteLine(correctletters);


            foreach (char ltr in correctletters)
            {

                Console.Write(ltr + " ");

            }

          Console.WriteLine();
            Console.WriteLine();

            foreach (char ltr in allGuessedLetters)
            {

                Console.Write(ltr + " ");

            }
        }

        private static void MatchInputAndSecretWord(string hemligtord, string input, char[] gissatord)
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
