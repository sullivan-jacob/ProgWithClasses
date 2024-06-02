using System;

class Word {

    private string word;
    private bool hidden;

    public Word(string word) {
        this.word = word;
    }

    public void Hide() {
        hidden = true;
    }

    public void Display() {
        if (hidden) {
            Console.Write("--- ");
        } else {
            Console.Write($"{word} ");
        }
        
    }
}