class TagCreationMenu : Menu{
    private string name = "UnNamed";
    private string filePath = "";
    private List<Tag> parentTags = [];
    private List<Tag> childTags = [];
    protected override void InitializeMenuItemActions()
    {
        menuItemActions = new Dictionary<string, Action> {
            {$"Edit Name: {name}", EditName},
            {"Add File Link", AddFileLink},
            {"Add Parent Tag", AddParentTag},
            {"Add Child Tag", AddChildTag},
            {"Save New Tag", SaveTag}
        };
        menuItemsArray = [.. menuItemActions.Keys];
    }
    protected override void InitializeMenuKeyActions()
    {
        keyActions = new Dictionary<ConsoleKey, Action> {
            {ConsoleKey.UpArrow, MoveSelectionUp},
            {ConsoleKey.DownArrow, MoveSelectionDown},
            {ConsoleKey.Enter, Select},
            {ConsoleKey.Backspace, Back}
        };
        actionsDisplayString = "Up Arrow : Move Up | Down Arrow : Move Down | Enter : Select | Backspace : Back";
    }
    private void EditName() {
        name = StringEditor.RunStringEditor(name, "Name");
        InitializeMenuItemActions();
    }
    private void AddFileLink() {
        while (true) {
            string possibleFilePath = StringEditor.RunStringEditor(filePath, "File Path");
            if (File.Exists(possibleFilePath)) {
                filePath = possibleFilePath;
                break;
            }
            else if (possibleFilePath != "" && possibleFilePath != null) {
                Console.Clear();
                Console.WriteLine("No file was found at that location");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
            }
            else {
                break;
            }
        }
        InitializeMenuItemActions();
    }
    private void AddParentTag() {
        TagSelectionMenu tagSelectionMenu = new();
        Tag newParent = tagSelectionMenu.OpenTagSelectionMenu();
        if (newParent != null) {
            parentTags.Add(newParent);
            InitializeMenuItemActions();
        }
    }
    private void AddChildTag() {
        TagSelectionMenu tagSelectionMenu = new();
        Tag newChild = tagSelectionMenu.OpenTagSelectionMenu();
        if (newChild != null) {
            childTags.Add(newChild);
            InitializeMenuItemActions();
        }
    }
    private void SaveTag() {
        Tag newTag = new(name);
        if (filePath != "") {
            newTag.SetFilePath(filePath);
        }
        foreach (Tag parentTag in parentTags) {
            newTag.AddParent(parentTag);
        }
        foreach (Tag childTag in childTags) {
            childTag.AddParent(newTag);
        }
        Program.AddTag(newTag);
        exitMenu = true;
    }
}