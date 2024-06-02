using System;

class Scripture {

    private string book;
    private int chapter;
    private int startVerse;
    private int endVerse;
    private List<Verse> verses = new();

    public Scripture(string book, int chapter, int verse, string scripture) {
        this.book = book;
        this.chapter = chapter;
        startVerse = verse;
        endVerse = verse;
        verses.Add(new(verse, scripture));
    }

    public Scripture(string book, int chapter, int startVerse, int endVerse, List<string> scriptures) {
        this.book = book;
        this.chapter = chapter;
        this.startVerse = startVerse;
        this.endVerse = endVerse;
        for(int i = 0; i <= endVerse - startVerse; i++) {
            verses.Add(new(i+startVerse, scriptures[i]));
        }
    }

    public void Display() {
        if (startVerse == endVerse) {
            Console.Write($"\n{book} {chapter}:{startVerse}");
        } else {
            Console.Write($"\n{book} {chapter}:{startVerse}-{endVerse}");
        }
        for(int i = 0; i < verses.Count; i++) {
            verses[i].Display();
        }
    }

    public void Memorize() {
        bool run = true;
        while (run) {
            Display();
            Console.WriteLine("\nPress enter to hide words or type 'Quit' to end the memorization program.");
            string input = Console.ReadLine();
            if (input == "") {
                bool hidden = true;
                for(int i = 0; i < verses.Count; i++) {
                    if (!verses[i].Hide(3)) {
                        hidden = false;
                    }
                }
                if (hidden) {
                    return;
                }
            } else {
                return;
            }
        }
        

    }
    
}