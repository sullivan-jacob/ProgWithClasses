class TagNavigationMenu : Menu {
    private Tag[] tags;
    protected override void InitializeMenuItemActions() {
        tags = [.. Program.GetTags()];
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
            {ConsoleKey.Backspace, Back}/*,
            {ConsoleKey.Insert, EditSort}*/
        };
        actionsDisplayString = "Up Arrow : Move Up | Down Arrow : Move Down | Enter : Select | Backspace : Back"/* | Insert : Edit Sort"*/;
    }
    private void SelectTag() {
        Tag selectedTag = tags[cursorPosition + menuPosition];
        while (true) {
            TagMenu tagMenu = new(selectedTag);
            selectedTag = tagMenu.OpenTagMenu();
            if (selectedTag == null) {
                break;
            }
        }
    }
}