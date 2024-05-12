using System;
using System.ComponentModel;

public class Job(string title, string company, string startYear, string endYear)
{
    
    public string title = title;
    public string company = company;
    public string startYear = startYear;
    public string endYear = endYear;

    public void Display() {
        Console.WriteLine($"{title} ({company}) {startYear}-{endYear}");
    }
}