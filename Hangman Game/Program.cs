using System;
using System.Collections.Generic;
using System.IO;

namespace Hangman_Game
{
    internal class Program
    {
        static void Main()
        {
            // Open and save each line of file to a string.
            string contents = "";
            try
            {
                string path = @"Files\word.txt";
                using (var sr = new StreamReader(path))
                {
                    contents = sr.ReadToEnd().ToLower();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            // List created that would store each letter that has been currently guessed.
            List<string> lettersGuessed = new List<string>();

            // Random object to select a random word from the contents of the word.txt file and store in string.
            Random randWord = new Random();
            string[] words = contents.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            string wordToGuess = words[randWord.Next(0, words.Length)].Trim();

            // Used to intialise a string with hidden characters of underscores until overwritten by user with correct letter of hidden word.
            string guessedWord = new string('_', wordToGuess.Length);

            // Open title.
            Console.WriteLine("Welcome to Hangman! A word has been generated.");

            // Declared number of lives and bool to check for the word being gussed.
            // Used for while loop condition to tell user they failed (out of lives) or gussed the correct word.
            int lives = 7;
            bool wordGuessed = false;
            while (lives > 0 && !wordGuessed)
            {
                // After every letter input condition is checked to see if word was gussed correctly.
                if (wordToGuess.Equals(guessedWord))
                {
                    wordGuessed = true;
                }
                else
                {
                    // User input of letter.
                    Console.Write("\nEnter a letter: ");
                    string letter = Console.ReadLine().ToLower();

                    // Checks for empty input.
                    if (string.IsNullOrWhiteSpace(letter))
                    {
                        Console.WriteLine("Input must not be blank!");
                        continue;
                    }

                    // Displays the current status of characters guessed.
                    Console.WriteLine($"\nCurrent entries: {guessedWord}");

                    // Checks to see if the letter has already been inputted.
                    if (lettersGuessed.Contains(letter))
                    {
                        Console.WriteLine($"\nYou already entered the letter: {letter}.");
                        Console.WriteLine("Try another letter!");
                        continue;
                    }

                    // Adds input to list.
                    lettersGuessed.Add(letter);

                    // If the inputted letter is contained within the randomly generated word, condition is run.
                    if (wordToGuess.Contains(letter))
                    {
                        Console.WriteLine($"Correct letter of {letter}");

                        // Replaces the underscores in guessedWord variable with the input of "letter", at the same index as the hidden word.
                        for (int index = 0; index < wordToGuess.Length; index++)
                        {
                            if (wordToGuess[index] == letter[0])
                            {
                                guessedWord = guessedWord.Substring(0, index) + letter[0] + guessedWord.Substring(index + 1);
                            }
                        }
                    }
                    else
                    {
                        // If none of the conditions are met, meaning the letter wasn't in the hidden word, a life is deducted.
                        Console.WriteLine("\nThat letter is not in my word");
                        lives--;
                        Console.WriteLine($"You have {lives} lives remaining.");
                    }
                }

            }

            // If the word is guessed then the condition will display the hidden word, congratulate the user and exit the program.
            if (wordGuessed)
            {
                Console.WriteLine($"\nThe word is {wordToGuess}!");
                Console.WriteLine("Congratulations!");
                System.Environment.Exit(1);
            }
            else
            {
                // Once lives drop to 0, the game is over and ends.
                Console.WriteLine("\nNo remaining lives. You have failed!");
                Console.WriteLine($"The word was: {wordToGuess}.");
                System.Environment.Exit(1);
            }
            Console.ReadLine();
        }
    }
}
