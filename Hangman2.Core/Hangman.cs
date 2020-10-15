using System;

namespace Hangman2.Core
{
    public class Hangman
    {
        private int _numberOfTries;


        public  Hangman(string secretWord)
        {
            _numberOfTries = secretWord.Length + 5;
        }

        public int numberOfTries
        {
            get
            {
                return _numberOfTries;
            }

           
        }
    }
}
