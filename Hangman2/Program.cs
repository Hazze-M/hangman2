/* 

Todo
- Ska kunna vinna
- Gränsnittet:Refresha sidan + färger
- Skriv ut de bokstäver som är gissade
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

           
            ArrayList guessedLetters = new ArrayList();
            int tries = 10;
            string[] wordArray = new string[] { "GRUNDSKOLA", "GARAGE", "PROGRAMMERING", "GAVEL", "TELEFON", "FLYGPLAN", "TAVLA" };

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
            Console.WriteLine("guessedletter.length: " + guessedLetters.Count);
            

            //Loopa igenom tills
            while (!rättord)
            {



                Console.Write("\nGissa bokstav: ");

                string input = Console.ReadLine().ToUpper();

                if (input.Length > 1)
                {
                    Console.WriteLine("Wrong input, max 1 letter!");

                }
                else


                if (Regex.IsMatch(input.ToString(), @"^[a-zA-Z]+$"))
                {
                    
                        
                    
                    guessCount++;
                    for (int i = 0; i < hemligtord.Length; i++)
                    {

                        if (hemligtord[i].ToString().Contains(input))
                        {

                            gissatord[i] = hemligtord[i];
                            //winGuess++;
                            //if (winGuess == hemligtord.Length)
                            //{
                            //    Console.WriteLine("YOU WIN!");
                            //        break;
                            //}
                            
                            foreach (var ltr in gissatord)
                            {

                                if (ltr.ToString() != null)
                                {
                                    break;
                                }


                            }


                        }
                    }

                    Console.WriteLine(gissatord);
                    

                    Console.WriteLine("Guesscount: " + guessCount);
                    for (int i = 0; i < guessCount; i++)
                    {
                        
                        if (!guessedLetters.Contains(input))
                        {
                            guessedLetters.Add(input);

                            Console.WriteLine("input:" + input);
                        }
                        Console.WriteLine("GUESSEDLETTER:" + guessedLetters[guessCount] + " ");
                        
                    }
                }
                else
                    Console.WriteLine("Wrong input, only letters accepted!");

                if (guessCount >= tries)
                {
                    Console.WriteLine("You guessed too many times!");
                    break;
                }

                

            }
            //Console.Clear();




        }
    }
}
