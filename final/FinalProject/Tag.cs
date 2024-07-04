using System;
using System.Collections.Generic;

class Tag : Property{
    private Dictionary<string, Tag> parentTags = [];
    private Dictionary<string, TypeTag> typeTags = [];
    public Tag(string tagName) : base(tagName) {}
    public override void SetName(string name) {
        foreach (Tag child in childTags.Values) {
            child.RemoveParent(this);
        }
        foreach (Tag parent in parentTags.Values) {
            parent.RemoveChild(this);
        }
        foreach (TypeTag typeTag in typeTags.Values) {
            typeTag.RemoveChild(this);
        }
        this.name = name;
        foreach (Tag child in childTags.Values) {
            child.AddParent(this);
        }
        foreach (Tag parent in parentTags.Values) {
            parent.AddChild(this);
        }
        foreach (TypeTag typeTag in typeTags.Values) {
            typeTag.AddChild(this);
        }
    }
    public Dictionary<string, Tag> GetParents() {
        return parentTags;
    }
    public void AddParent(Tag parent) {
        parent.AddChild(this);
        parentTags.Add(parent.GetName(), parent);
    }
    public void RemoveParent(Tag parent) {
        parent.RemoveChild(this);
        parentTags.Remove(parent.GetName());
    }
    public override void AddChild(Tag child) {
        child.AddParent(this);
        childTags.Add(child.GetName(), child);
    }
    public override void RemoveChild(Tag child) {
        child.RemoveParent(this);
        childTags.Remove(child.GetName());
    }
    public Dictionary<string, TypeTag> GetTypeTags() {
        return typeTags;
    }
    public void AddTypeTag(TypeTag typeTag) {
        typeTag.AddChild(this);
        typeTags.Add(typeTag.GetName(), typeTag);
    }
    public void RemoveTypeTag(TypeTag typeTag) {
        typeTag.RemoveChild(this);
        typeTags.Remove(typeTag.GetName());
    }
    public override void Display() {
        Console.Write($"{name}");
        foreach (TypeTag typeTag in typeTags.Values) {
            Console.Write($" : {typeTag.GetName()}");
        }
        Console.WriteLine();
        Console.WriteLine("Parent Tags:");
        foreach (Tag parent in parentTags.Values) {
            parent.DisplayIndented();
        }
        Console.WriteLine("Child Tags:");
        foreach (Tag child in childTags.Values) {
            child.DisplayIndented();
        }
    }

    public override void DisplayIndented() {
        Console.Write($"    {name}");
        foreach (TypeTag typeTag in typeTags.Values) {
            Console.Write($" : {typeTag.GetName()}");
        }
        Console.WriteLine();
    }
    public void DeleteTag() {
        foreach (Tag child in childTags.Values) {
            child.RemoveParent(this);
        }
        foreach (Tag parent in parentTags.Values) {
            parent.RemoveChild(this);
        }
        foreach (TypeTag typeTag in typeTags.Values) {
            typeTag.RemoveChild(this);
        }
    }
}