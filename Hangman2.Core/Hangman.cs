﻿using System;

namespace Hangman2.Core
{
    public class Hangman
    {
        private int _numberOfTries;


        public  Hangman(string secretWord)
        {
            _numberOfTries = secretWord.Length + 2;
        }

        public int numberOfTries
        {
            get
            {
                return _numberOfTries;
            }

           
        }
    }
    /*
      
    OO: You next step is to move code from Program.cs to this class
      
    public class Hangman
    {
        public Hangman(string secretWord, int nrOfGuesses)
        {
            // write code
        }

        public GuessResult Guess(string guess)
        {
            // write code

            throw new NotImplementedException();
        }
    }

    public enum GuessResult
    {
        CorrectGuess, IncorrectGuess, InvalidGuess, AlreadyGuessed
    }
    */
}
