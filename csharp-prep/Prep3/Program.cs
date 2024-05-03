using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("You will be guessing a whole number(integer) between 0 and 100");
        Random seed = new();
        int magicNum = seed.Next(100);
        bool wrong = true;
        string guess;
        int guessInt;
        while (wrong) {
            Console.Write("What is your guess? ");
            guess = Console.ReadLine();
            try {
                guessInt = int.Parse(guess);
                if (guessInt == magicNum) {
                    Console.WriteLine("Correct!");
                    wrong = false;
                } else if (guessInt > magicNum) {
                    Console.WriteLine("Lower");
                } else {
                    Console.WriteLine("Higher");
                }
            } catch {
                Console.WriteLine("That is not an integer.");
            }
        }

    }

    
}