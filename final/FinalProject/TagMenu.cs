class TagMenu : Menu {
    Tag tag;
    Tag selectedTag = null;

    public TagMenu(Tag tag) {
        this.tag = tag;
    }
    protected override void InitializeMenuItemActions() {  
        menuItemActions = new Dictionary<string, Action> {
            {$"Current Tag: {tag.GetName()}", Pass},
            {"Edit Name", EditName},
            {"View Linked File", ViewFile},
            {"Edit File Path", EditFilePath},
            {"Parent Tags", Pass}
        };
        foreach (Tag parentTag in tag.GetParents().Values) {
            menuItemActions.Add($"   {parentTag.GetName()}", SelectTag);
        }
        menuItemActions.Add("   Add Parent Tag", AddParentTag);
        menuItemActions.Add("Child Tags", Pass);
        foreach (Tag childTag in tag.GetChildren().Values) {
            menuItemActions.Add($"    {childTag.GetName()}", SelectTag);
        }
        menuItemActions.Add("   Add Child Tag", AddChildTag);
        menuItemActions.Add("Delete Tag", DeleteTag);
        menuItemsArray = [.. menuItemActions.Keys];
    }
    protected override void InitializeMenuKeyActions()
    {
        keyActions = new Dictionary<ConsoleKey, Action> {
            {ConsoleKey.UpArrow, MoveSelectionUp},
            {ConsoleKey.DownArrow, MoveSelectionDown},
            {ConsoleKey.Enter, Select},
            {ConsoleKey.Backspace, Back},
            {ConsoleKey.Delete, RemoveTag}/*,
            {ConsoleKey.Insert, EditSort}*/
        };
        actionsDisplayString = "Up Arrow : Move Up | Down Arrow : Move Down | Enter : Select | Delete : Remove Tag | Backspace : Back";
    }
    public Tag OpenTagMenu() {
        InitializeMenuItemActions();
        InitializeMenuKeyActions();
        if (menuItemActions.Count == 0) {
            Console.Clear();
            Console.WriteLine("No Options Provided By Menu.\nPress any key to continue");
            Console.ReadKey(true);
            return null;
        }
        InitializeMenu();
        while (!exitMenu) {
            DisplayMenu();
            RunKeyActions();
        }
        return selectedTag;
    }
    private void SelectTag() {
        string[] menuItemActionKeys = menuItemActions.Keys.ToArray();
        Dictionary<string, Tag> childTags = tag.GetChildren();
        Dictionary<string, Tag> parentTags = tag.GetParents();
        string selectedMenuItem = menuItemActionKeys[cursorPosition + menuPosition].TrimStart();
        if (parentTags.TryGetValue(selectedMenuItem, out Tag parentTag)) {
            selectedTag = parentTag;
            exitMenu = true;
        }
        else if (childTags.TryGetValue(selectedMenuItem, out Tag childTag)) {
            selectedTag = childTag;
            exitMenu = true;
        }
    }
    private void EditName() {
        tag.SetName(StringEditor.RunStringEditor(tag.GetName(),"Name"));
        InitializeMenuItemActions();
    }
    private void RemoveTag() {
        string[] menuItemActionKeys = menuItemActions.Keys.ToArray();
        Dictionary<string, Tag> childTags = tag.GetChildren();
        Dictionary<string, Tag> parentTags = tag.GetParents();
        int selectedMenuIndex = cursorPosition + menuPosition;
        string selectedMenuItem = menuItemActionKeys[selectedMenuIndex].TrimStart();
        int childTagsIndex = 0;
        for (int index = 0; index < menuItemsArray.Length; index++) {
            if (menuItemsArray[index] == "Child Tags") {
                childTagsIndex = index;
                break;
            }
        }
        if (selectedMenuIndex < childTagsIndex) {
            if (parentTags.TryGetValue(selectedMenuItem, out Tag selectedTag)) {
                tag.RemoveParent(selectedTag);
            }
        }
        else {
            if (childTags.TryGetValue(selectedMenuItem, out Tag selectedTag)) {
                selectedTag.RemoveParent(tag);
            }
        }
        InitializeMenuItemActions();
    }
    private void EditFilePath() {
        while (true) {
            string possibleFilePath = StringEditor.RunStringEditor(tag.GetFilePath(), "File Path");
            if (File.Exists(possibleFilePath)) {
                tag.SetFilePath(possibleFilePath);
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
            tag.AddParent(newParent);
            InitializeMenuItemActions();
        }
    }
    private void AddChildTag() {
        TagSelectionMenu tagSelectionMenu = new();
        Tag newChild = tagSelectionMenu.OpenTagSelectionMenu();
        if (newChild != null) {
            newChild.AddParent(tag);
            InitializeMenuItemActions();
        }
    }
    private void DeleteTag() {
        Console.Clear();
        Console.Write($"Do you want to delete {tag.GetName()}? (Y/n)");
        ConsoleKeyInfo key = Console.ReadKey(false);
        if (key.Key == ConsoleKey.Y) {
            Console.Clear();
            Console.WriteLine($"Deleting {tag.GetName()}...");
            Thread.Sleep(2000);
            tag.DeleteTag();
        }
        else {
            Console.Clear();
            Console.WriteLine("Canceling Tag Deletion...");
            Thread.Sleep(2000);
        }
    }
    private void ViewFile() {
        tag.ReadFile();
    }
}