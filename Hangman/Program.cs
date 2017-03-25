using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Hangman
{
    public class Program
    {
        static string word = string.Empty;
        private static string input = string.Empty;
        static char[] incorrectGuesses = new char[26];
        static char[] correctGuesses = new char[26];
        static int incorrectGuessCount = 0; 
        static int correctGuessCount = 0;
        static int guessesLeft = 0;
        static bool hasWon = false;
        static int lettersRevealed = 0;
        static string wordToUpper = string.Empty;
        //static List<char> correctGuesses = new List<char>();
        //static StringBuilder puzzleDisplay = null;
        static string puzzleDisplay = string.Empty;

        public static string Input
        {
            get
            {
                return input;
            }
        }

        private static char? guess;
        public static char? Guess
        {
            get
            {
                return ((guess.HasValue && !char.IsNumber(guess.Value) && !char.IsSymbol(guess.Value)) ? guess : null);
            }
            set
            {
                guess = value;
            }
        }

        static bool initialize()
        {
            guessesLeft = 10; //int.Parse(ConfigurationManager.AppSettings["NumberOfGuesses"]);
            Console.WriteLine("Enter word to guess: ");
            word = Console.ReadLine();
            if (word != null && word != string.Empty)
            {
                wordToUpper = word.ToUpper();
                //puzzleDisplay = new StringBuilder();
                for (int i = 0; i < word.Length; i++)
                {
                    //puzzleDisplay.Append("*");
                    puzzleDisplay += "*";
                }
                return true;
            }
            else
            {
                Console.WriteLine("Word is not valid.");
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        static void ProcessCorrectGuess()
        {
            Console.Clear();
            Console.WriteLine("Correct!");
            correctGuesses[correctGuessCount] = Guess.Value;
            //correctGuesses.Add(Guess.Value);
            correctGuessCount++;
            char[] temp = puzzleDisplay.ToCharArray();
            for (int i = 0; i < wordToUpper.Length; i++)
            {
                if (wordToUpper[i] == Guess.Value)
                {
                    //puzzleDisplay[i] = wordToUpper[i];       
                    temp[i] = Guess.Value;
                    lettersRevealed++;
                }
            }
            puzzleDisplay = new string(temp);
            if (lettersRevealed == word.Length)
            {
                hasWon = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        static void ProcessIncorrectGuess()
        {
            Console.Clear();
            Console.WriteLine("Incorrect!");
            incorrectGuesses[incorrectGuessCount] = Guess.Value;
            incorrectGuessCount++;
            guessesLeft--;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            if (initialize())
            {
                Console.WriteLine("Welcome to hangman!");
                Console.WriteLine(puzzleDisplay.ToString());
                while (!hasWon && guessesLeft > 0)
                {
                    Console.WriteLine("You have " + guessesLeft + " guesses left");
                    Console.WriteLine("Please enter your guess as a single letter:");
                    Console.WriteLine("Letters chosen:");
                    for (int i = 0; i < incorrectGuesses.Length; i++)
                    {
                        Console.Write(incorrectGuesses[i]);
                    }
                    Console.WriteLine();
                    input = Console.ReadLine().ToUpper();
                    Guess = input[0];
                    if (Guess.HasValue)
                    {
                        Console.WriteLine("Your guess is " + Guess.Value);
                        if (correctGuesses.Contains(Guess.Value))
                        {
                            Console.Clear();
                            Console.WriteLine("You've already correctly guessed this letter. Guess again.");
                            Console.WriteLine(puzzleDisplay.ToString());
                            continue;
                        }
                        else if (incorrectGuesses.Contains(Guess.Value))
                        {
                            Console.Clear();
                            Console.WriteLine("You've already incorrectly guessed this letter. Guess again.");
                            Console.WriteLine(puzzleDisplay.ToString());
                            continue;
                        }
                        if (wordToUpper.Contains(Guess.Value))
                        {
                            ProcessCorrectGuess();
                        }
                        else
                        {
                            ProcessIncorrectGuess();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid entry. Guess again.");
                        Console.WriteLine(puzzleDisplay.ToString());
                        continue;
                    }
                    Console.WriteLine(puzzleDisplay.ToString());
                }
                Console.WriteLine((hasWon) ? "Winner!" : "Looooser");
            }
            Console.ReadLine();
        }
    }
}