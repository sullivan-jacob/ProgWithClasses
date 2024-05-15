using System;
using System.Reflection;

public class Resume(string name)
{

    public string name = name;
    public List<Job> jobs = [];

    public void NewJob() {
        Console.Write("What is the job's title? ");
        string jobTitle = Console.ReadLine();
        Console.Write("What company did you do this job for? ");
        string jobCompany = Console.ReadLine();
        Console.Write("What year did you start this job? ");
        string jobStartYear = Console.ReadLine();
        Console.Write("What year did you leave this job? ");
        string jobEndYear = Console.ReadLine();
        jobs.Add(new Job(jobTitle, jobCompany, jobStartYear, jobEndYear));
    }

    public void Display() {
        Console.WriteLine($"Name: {name}");
        Console.WriteLine("Jobs:");
        int numJobs = jobs.Count;
        for(int i = 0; i < numJobs; i++) {
            jobs[i].Display();
        }
    }
}