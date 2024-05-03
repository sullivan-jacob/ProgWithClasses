using System;
using System.Diagnostics;
using System.Globalization;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a list of numbers, enter 0 when finished.");
        bool execute = true;
        int number;
        List<int> numbers = [];
        while (execute) {
            Console.Write("Enter Number: ");
            number = int.Parse(Console.ReadLine());
            if (number != 0){
                numbers.Add(number);
            } else {
                execute = false;
            }
        }
        numbers.Sort(CompareGreatestToSmallest);
        int sum = 0;
        foreach (int num in numbers) {
            sum += num;
        }
        Console.WriteLine($"The sum is: {sum}");
        float average = sum/numbers.Count;
        Console.WriteLine($"The average is: {average}");
        int largestNum = numbers[0];
        Console.WriteLine($"The largest number is: {largestNum}");
        int smallestNum = numbers[^1];
        Console.WriteLine($"The smallest number is: {smallestNum}");
        Console.WriteLine("The sorted list is:");
        foreach (int num in numbers) {
            Console.WriteLine(num);
        }

    }

    private static int CompareGreatestToSmallest(int x, int y) {
        int result;
        if (x == y) {
            result = 0;
        } else if (x > y) {
            result = -1;
        } else {
            result = 1;
        }
        return result;
    }
}