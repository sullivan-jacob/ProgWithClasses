using System;

Console.Write("What is your name? ");
string name = Console.ReadLine();
Resume resume = new(name);
bool run = true;
while (run)
{
    Console.Write("Do you want to add a job to your resume[Y/n]? ");
    string jobResponse = Console.ReadLine();
    if (jobResponse == "y" || jobResponse == "Y") {
        resume.NewJob();
    } else {
        run = false;
    }
    Console.Write("Do you want to display your resume[Y/n]? ");
    string displayResponse = Console.ReadLine();
    if (displayResponse == "y" || displayResponse == "Y") {
        resume.Display();
    }
}
