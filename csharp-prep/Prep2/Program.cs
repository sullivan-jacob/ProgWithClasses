using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your grade percentage? .");
        int grade = int.Parse(Console.ReadLine());
        int gradeOnes = grade%10;
        int gradeTens = (grade - gradeOnes)/10;
        string gradeModifier = "";
        if (gradeOnes >= 7) {
            gradeModifier = "+";
        } else if (gradeOnes <= 3) {
            gradeModifier = "-";
        }
        string letterGrade = "";
        bool passed = true;
        if (gradeTens >= 9) {
            if (gradeModifier == "-") {
                letterGrade = "A-";
            } else {
                letterGrade = "A";
            }
        } else if (gradeTens == 8) {
            letterGrade = $"B{gradeModifier}";
        } else if (gradeTens == 7) {
            letterGrade = $"C{gradeModifier}";
        } else if (gradeTens == 6) {
            letterGrade = $"D{gradeModifier}";
            passed = false;
        } else if (gradeTens <= 5) {
            letterGrade = "F";
            passed = false;
        }
        Console.WriteLine($"You currently have a {letterGrade} in this class.");
        if (passed) {
            Console.WriteLine("You are currently passing this class, good job!");
        } else {
            Console.WriteLine("You are currently failing this class. Work harder!");
        }

    }
}