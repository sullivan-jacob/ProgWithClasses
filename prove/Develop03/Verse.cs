using System;

class Verse {

    private List<Word> words = new();
    private List<int> visibleIndexes = new();
    private int verse;

    public Verse(int verse, string scripture) {
        this.verse = verse;
        string[] scriptureWords = scripture.Split(" ");
        for(int i = 0; i < scriptureWords.Length;i++) {
            words.Add(new Word(scriptureWords[i]));
        }
        for(int i = 0; i < words.Count; i++){
            visibleIndexes.Add(i);
        }
    }

    public void Display() {
        Console.Write($"\n{verse} ");
        for(int i = 0; i < words.Count; i++) {
            words[i].Display();
        }
    }

    public bool Hide(int count) {
        for(int i = 0; i < count; i++) {
            Random random = new();
            int randomIndex = random.Next(0, visibleIndexes.Count - 1);
            words[visibleIndexes[randomIndex]].Hide();
            if (!visibleIndexes.Remove(visibleIndexes[randomIndex])) {
                Console.WriteLine("Failed Remove");
            }
            if (visibleIndexes.Count == 0) {
                Console.WriteLine("All Hidden");
                return true;
            }
        }
    return false;
    }
}