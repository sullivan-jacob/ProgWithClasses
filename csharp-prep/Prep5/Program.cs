using System;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

class Program
{
    static void Main(string[] args)
    {
        DisplayResult(PromptUserName(), SquaredNumber(PromptUserNumber()));
        /*It works. Heres the code that uses more variables:
        string name = PromptUserName();
        int number = PromptUserNumber();
        int squaredNumber = SquaredNumber(number);
        DisplayResult(name, squaredNumber);
        */
    }
    static void DisplayWelcome() {
        Console.WriteLine("Welcome to the program.");
    }
    static string PromptUserName() {
        Console.Write("Please enter your name: ");
        return Console.ReadLine();
    }
    static int PromptUserNumber() {
        Console.Write("Please enter your favorite number: ");
        return int.Parse(Console.ReadLine());
    }
    static int SquaredNumber(int number) {
        return number * number;
    }
    static void DisplayResult(string name, int squaredNumber) {
        Console.WriteLine($"{name}, the sqaure of your number is {squaredNumber}.");
    }
}