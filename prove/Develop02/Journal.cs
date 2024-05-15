using System.Net;
using System.IO;

public class Journal {
    
    private List<Entry> entries = [];
    private List<string> prompts = [
        "What was the highlight of your day today? Describe the moment or experience in detail, including how it made you feel.",
        "Is there something you're struggling with or a problem you need to work through? Write about it in your journal, getting all your thoughts and feelings out on paper.",
        "Describe a random act of kindness you witnessed or experienced today. How did it impact you?",
        "If you could relive one moment from your past, what would it be and why? Reflect on the significance of that memory.",
        "What are you most grateful for today? List out three to five things, no matter how big or small, and explain why you appreciate them."
    ];

    public void NewEntry(){
        Random random = new();
        int randomIndex = random.Next(5);
        string prompt = prompts[randomIndex];
        Console.WriteLine($"Prompt: {prompt}");
        string response = Console.ReadLine();
        DateOnly date = new(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
        entries.Add(new Entry(date, prompt, response));
    }
    public void DisplayJournal() {
        foreach(Entry entry in entries) {
            entry.DisplayEntry();
        }
    }
    public void SaveJournal() {
        using StreamWriter writer = new("journal.csv");
        writer.WriteLine("date,prompt,response");
        int numEntries = entries.Count;
        for (int i = 0; i < numEntries; i++)
        {
            writer.WriteLine(entries[i].CSVOutput());
        }
    }
    public void LoadJournal() {
        entries.Clear();
        using StreamReader reader = new("journal.csv");
        reader.ReadLine();
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] fields = line.Split(",,,");
            DateOnly date = DateOnly.Parse(fields[0]);
            string prompt = fields[1];
            string response = fields[2];
            entries.Add(new(date,prompt,response));
        }
    }

}