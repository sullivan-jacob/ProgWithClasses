using System;

class TypeTag : Property{
    public TypeTag(string tagName) : base(tagName) {}
    public override void SetName(string name) {
        foreach (Tag child in childTags.Values) {
            child.RemoveTypeTag(this);
        }
        this.name = name;
        foreach (Tag child in childTags.Values) {
            child.AddTypeTag(this);
        }
    }
    public override void AddChild(Tag child) {
        child.AddTypeTag(this);
        childTags.Add(child.GetName(), child);
    }
    public override void RemoveChild(Tag child) {
        child.RemoveTypeTag(this);
        childTags.Remove(child.GetName());
    }
    public override void Display() {
        Console.Write($"{name} : Type Tag");
        Console.WriteLine();
        Console.WriteLine("Child Tags:");
        foreach (Tag child in childTags.Values) {
            child.DisplayIndented();
        }
        Console.WriteLine();
    }

    public override void DisplayIndented() {
        Console.WriteLine($"    {name} : Type Tag");
    }
}