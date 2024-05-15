using System;
public class Entry {

    private string prompt;
    private string response;
    private DateOnly date;

    public Entry(DateOnly date, string prompt, string response) {
        this.prompt = prompt;
        this.response = response;
        this.date = date;
    }
    public void DisplayEntry() {
        Console.WriteLine($"{date}: {prompt}\n{response}");
    }
    public string CSVOutput() {
        string CSVOutput = $"{date},,,{prompt},,,{response}";
        return CSVOutput;
    }
}