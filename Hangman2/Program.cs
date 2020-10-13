using System;
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

            string[] guessedLetters = new string[10];
            string[] wordArray = new string[] { "grundskola", "garage", "programmering", "gavel", "telefon", "flygplan", "tavla" };

            Console.WriteLine("Hangman game\n");

            Random randGen = new Random();
            var idx = randGen.Next(0, 6);
            string hemligtord = wordArray[idx];


            char[] gissatord = new char[hemligtord.Length];
            char gissa;
            int winGuess = 0;
            bool rättord = false;
            int guessCount = 0;

            Program kontroll = new Program();

            //Maskera hemligt ord och printa _
            for (int i = 0; i < gissatord.Length; i++)
            {
                gissatord[i] = '_';
                Console.Write(gissatord[i]);
            }
            //Test printout
            Console.WriteLine("Hemligtord: " + hemligtord);
            Console.WriteLine("guessedletter.length: " + guessedLetters.Length);


            //Loopa igenom tills
            while (!rättord)
            {
                Console.Write("\nGissa bokstav: ");
                char input = char.Parse(Console.ReadLine());

                if (Regex.IsMatch(input.ToString(), @"^[a-zA-Z]+$"))
                {
                    guessCount++;
                    for (int i = 0; i < hemligtord.Length; i++)
                    {
                        if (hemligtord[i].ToString().Contains(input)){
                            gissatord[i] = hemligtord[i];
                            //winGuess++;
                            //if (winGuess == hemligtord.Length)
                            //{
                            //    Console.WriteLine("YOU WIN!");
                            //        break;
                            //}
                            Console.WriteLine(gissatord);
                            foreach (var ltr in gissatord)
                            {
                                if (ltr.ToString() != null)
                                {
                                    Console.WriteLine("YOU WIN");
                                    break;
                                }
                            }
                            
                            
                        }
                    }
                        Console.WriteLine("Guesscount: " + guessCount);
                }
                else
                    Console.WriteLine("Wrong input, only letters accepted!");

                if (guessCount >= guessedLetters.Length)
                {
                    Console.WriteLine("You guessed too many times!");
                    break;
                }



            }




        }
    }
}
