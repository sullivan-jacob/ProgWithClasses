using System;
using System.Collections.Generic;
using System.IO;

class FileEditor {
    string filePath;

    public FileEditor(string filePath) {
        this.filePath = filePath;
    }
    public void Editor() {
        string[] names = filePath.Split('/');
        string[] fileNameParts = names[^1].Split('.');
        string fileExtension = fileNameParts[^1];
        if (fileExtension == "txt") {
            TextEditor();
        } else {
            while(true){
                Console.Write($".{fileExtension} File Type not supported. Do you want to open the file in the plain text editor? (Y/n) ");
                string openInTextEditor = Console.ReadLine();
                if(openInTextEditor == "y" || openInTextEditor == "Y" ){
                    try{
                        TextEditor();
                    } 
                    catch {
                        Console.WriteLine("Text Editor Failure: Unable to open file.");
                    }

            /*=>BREAK STATEMENT<=*/
                    break;
                } else if (openInTextEditor == "n" || openInTextEditor == "N" ) {
            /*=>BREAK STATEMENT<=*/
                    break;
                } else {
                    Console.WriteLine("Invalid Response: Expected 'Y', 'y', 'N', or 'n'.");   
                }
            }
        }
    }
    private void TextEditor() {
        int[] cursorPosition = [0,0];
        List<string> text = [];
        foreach (string line in File.ReadLines(filePath)) {
            text.Add(line);
        }
        bool edited = true;
        int editorPosition = 0;
        while (true) {
            int consoleHeight = Console.WindowHeight;
            if (editorPosition > 0 && cursorPosition[1] == 0) {
                editorPosition--;
                cursorPosition[1]++;
                edited = true;
            }
            else if (editorPosition < text.Count - consoleHeight && cursorPosition[1] == consoleHeight) {
                editorPosition++;
                cursorPosition[1]--;
                edited = true;
            }
            if (edited) {
                Console.Clear();
                for (int line = 0; line < consoleHeight - 2; line++) {
                    Console.WriteLine(text[editorPosition + line]);
                }
                Console.WriteLine("Shift + Enter : Save and Exit");
            }
            edited = false;
            Console.SetCursorPosition(cursorPosition[0], cursorPosition[1] - editorPosition);
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.LeftArrow) {
                if (cursorPosition[0] > 0) {
                    cursorPosition[0]--;
                } else if (cursorPosition[1] > 0) {
                    cursorPosition[0] = text[--cursorPosition[1]].Length;
                }
            }
            else if (keyInfo.Key == ConsoleKey.RightArrow) {
                if (cursorPosition[0] < text[cursorPosition[1]].Length) {
                    cursorPosition[0]++;
                } else if (cursorPosition[1] < text.Count) {
                    cursorPosition[0] = 0;
                    cursorPosition[1]++;
                }
            }
            else if (keyInfo.Key == ConsoleKey.UpArrow && cursorPosition[1] > 0){
                cursorPosition[1]--;
                if (cursorPosition[0] > text[cursorPosition[1]].Length) {
                    cursorPosition[0] = text[cursorPosition[1]].Length;
                }
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow && cursorPosition[1] < text.Count){
                cursorPosition[1]++;
                if (cursorPosition[0] > text[cursorPosition[1]].Length) {
                    cursorPosition[0] = text[cursorPosition[1]].Length;
                }
            }
            else if (keyInfo.Key == ConsoleKey.Enter) {
                if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift)) { 
        /*=>BREAK STATEMENT<=*/;
                    break;
                }
                else if (cursorPosition[0] < text[cursorPosition[1]].Length){
                    string beginningSection = text[cursorPosition[1]][..cursorPosition[0]];
                    string endSection = text[cursorPosition[1]][cursorPosition[0]..];
                    text[cursorPosition[1]] = beginningSection;
                    text.Insert(++cursorPosition[1], endSection);
                    cursorPosition[0] = 0;
                    edited = true;
                } else {
                    text.Insert(++cursorPosition[1], "");
                    cursorPosition[0] = 0;
                    edited = true;
                }

            }
            else if (keyInfo.Key == ConsoleKey.Backspace) {
                if (cursorPosition[0] != 0) {
                    text[cursorPosition[1]] = text[cursorPosition[1]].Remove(--cursorPosition[0] , 1);
                }
                else if (cursorPosition[1] > 0) {
                    text[cursorPosition[1]-1]= text[cursorPosition[1]-1] + text[cursorPosition[1]];
                    text.RemoveAt(cursorPosition[1]--);
                }
                edited = true;
            }
            else if (keyInfo.Key == ConsoleKey.Delete) {
                if (cursorPosition[0] < text[cursorPosition[1]].Length - 1) {
                    text[cursorPosition[1]] = text[cursorPosition[1]].Remove(cursorPosition[0] , 1);
                }
                else if (cursorPosition[1] < text.Count - 1) {
                    text[cursorPosition[1]]= text[cursorPosition[1]] + text[cursorPosition[1] + 1];
                    text.RemoveAt(cursorPosition[1] + 1);
                }
                edited = true;
            }
            else if (!char.IsControl(keyInfo.KeyChar)) {
                text[cursorPosition[1]] = text[cursorPosition[1]].Insert(cursorPosition[0], keyInfo.KeyChar.ToString());
                cursorPosition[0]++;
                edited = true;
            }


        }
    }
}