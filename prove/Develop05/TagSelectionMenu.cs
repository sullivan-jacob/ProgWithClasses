class TagSelectionMenu : Menu{
    private Tag[] tags;
    Tag selectedTag = null;
    public TagSelectionMenu() {
        tags = [.. Program.GetTags()];
    }
    public Tag OpenTagSelectionMenu() {
        InitializeMenuItemActions();
        InitializeMenuKeyActions();
        if (menuItemActions.Count == 0) {
            Console.Clear();
            Console.WriteLine("No Tags to select.\nPress any key to continue");
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
    protected override void InitializeMenuItemActions() {
        menuItemActions = [];
        foreach (Tag tag in tags) {
            try {
                menuItemActions.Add(tag.GetName(), SelectTag);
            }
            catch (Exception ex) {
                Console.WriteLine($"Error: {ex}");
                Console.WriteLine($"Tag at Error: {tag}");
                Console.WriteLine($"Press key to continue");
                Console.ReadKey(true);
            }
        }
        menuItemsArray = [.. menuItemActions.Keys];
    }

    protected override void InitializeMenuKeyActions() {
        keyActions = new Dictionary<ConsoleKey, Action> {
            {ConsoleKey.UpArrow, MoveSelectionUp},
            {ConsoleKey.DownArrow, MoveSelectionDown},
            {ConsoleKey.Enter, Select},
            {ConsoleKey.Backspace, Back}
        };
        actionsDisplayString = "Up Arrow : Move Up | Down Arrow : Move Down | Enter : Select | Backspace : Back";
    }
    private void SelectTag() {
        selectedTag = tags[cursorPosition + menuPosition];
        if (selectedTag == null) {
            Console.WriteLine("Null Tag Selected");
            foreach (Tag tag in tags) {
                Console.WriteLine(tag.GetName());
            }
            Console.ReadKey(true);
        }
        exitMenu = true;
    }
}