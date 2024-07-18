using System.Runtime.InteropServices;

class Tag{
    protected Dictionary<string, Tag> childTags = [];
    protected string filePath;
    protected string name;
    private Dictionary<string, Tag> parentTags = [];
    public Tag(string tagName){
        name = tagName;
    }
    public Dictionary<string, Tag> GetParents() {
        return parentTags;
    }
    public void AddParent(Tag parent) {
        if (parent != null) {
            parent.AddChild(this);
            parentTags.Add(parent.GetName(), parent);
        }
    }
    public void RemoveParent(Tag parent) {
        if (parent != null) {
            parent.RemoveChild(this);
            parentTags.Remove(parent.GetName());
        }
    }
    public Dictionary<string, Tag> GetChildren() {
        return childTags;
    }
    protected void AddChild(Tag child) {
        childTags.Add(child.GetName(), child);
    }
    protected void RemoveChild(Tag child) {
        childTags.Remove(child.GetName());
    }
    public void DeleteTag() {
        foreach (Tag child in childTags.Values) {
            child.RemoveParent(this);
        }
        foreach (Tag parent in parentTags.Values) {
            parent.RemoveChild(this);
        }
    }
    public string GetName() {
        if (name == null || name == ""){
            Console.WriteLine("Tag name is unnamed");
            Console.WriteLine("Press key to continue");
            Console.ReadKey(true);
            name = "unnamed";
        }
        return name;
    }
    public void SetName(string name) {
        name = name.Trim();
        string[] bannedNames = [
            "Open Tag Navigator",
            "Create New Tag",
            "Save",
            "Save and Exit",
            "Add File Link",
            "Add Parent Tag",
            "Add Child Tag",
            "Save New Tag",
            "Edit Name",
            "View Linked File",
            "Edit File Path",
            "Parent Tags",
            "   Add Parent Tag",
            "Child Tags",
            "   Add Child Tag",
            "Delete Tag",
        ];
        foreach (string bannedName in bannedNames) {
            if (name == bannedName) {
                Console.Clear();
                Console.WriteLine($"The name '{name}' is not allowed to be used");
                Thread.Sleep(2000);
                return;
            }
        }
        foreach (Tag tag in Program.GetTags()) {
            if (name == tag.GetName()) {
                Console.Clear();
                Console.WriteLine($"The name '{name}' is already being used by another tag");
                Thread.Sleep(2000);
                return;
            }
        }
        this.name = name;
    }
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
        if (filePath != "") {
            Console.Clear();
            try {
                foreach (string line in File.ReadLines(filePath)) {
                Console.WriteLine(line);
            }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("\n\n\nPress any key to continue");
            Console.ReadKey(true);  
        }
        
    }
}