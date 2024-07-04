using System;
using System.Collections.Generic;
using System.IO;

abstract class Property {
    protected Dictionary<string, Tag> childTags = [];
    protected string filePath;
    protected string name;
    public Property(string name) {
        this.name = name;
    }
    public string GetName() {
        return name;
    }
    abstract public void SetName(string name);
    public Dictionary<string, Tag> GetChildren() {
        return childTags;
    }
    public abstract void AddChild(Tag child);
    public abstract void RemoveChild(Tag child);
    public abstract void Display();
    public abstract void DisplayIndented();
    public bool HasFile() {
        if (filePath == "") {
            return false;
        }
        return true;
    }
    public string GetFilePath() {
        return filePath;
    }
    public void SetFilePath(string filePath) {
        this.filePath = filePath;
    }
    public void ReadFile() {

        foreach (string line in File.ReadLines(filePath)) {
            Console.WriteLine(line);
        }
    }
    public void EditFile() {
        FileEditor fileEditor = new(filePath);
        fileEditor.Editor();
    }
}