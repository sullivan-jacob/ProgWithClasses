using System;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new();
        bool run = true;
        while(run) {
            Console.WriteLine("1. New Entry\n2. Display Entries\n3. Save Journal\n4. Load Journal\n5. Quit");
            string menuResponse = Console.ReadLine();
            switch(menuResponse) {
                case "1":
                    journal.NewEntry();
                    break;
                case "2":
                    journal.DisplayJournal();
                    break;
                case "3":
                    journal.SaveJournal();
                    break;
                case "4":
                    journal.LoadJournal();
                    break;
                case "5":
                    run = false;
                    break;
            }
        }
    }
}