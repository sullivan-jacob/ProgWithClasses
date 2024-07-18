class MainMenu : Menu {
    protected override void InitializeMenuItemActions()
    {
        menuItemActions = new Dictionary<string, Action> {
            {"Open Tag Navigator", OpenTagNavigationMenu},
            {"Create New Tag", OpenTagCreationMenu},
            {"Save", Program.Save},
            {"Save and Exit", Program.Exit}
        };
        menuItemsArray = [.. menuItemActions.Keys];
    }
    protected override void InitializeMenuKeyActions()
    {
        keyActions = new Dictionary<ConsoleKey, Action> {
            {ConsoleKey.UpArrow, MoveSelectionUp},
            {ConsoleKey.DownArrow, MoveSelectionDown},
            {ConsoleKey.Enter, Select}
        };
        actionsDisplayString = "Up Arrow : Move Up | Down Arrow : Move Down | Enter : Select";
    }

    private void OpenTagNavigationMenu() {
        TagNavigationMenu tagNavigationMenu = new();
        tagNavigationMenu.OpenMenu();
    }
    private void OpenTagCreationMenu() {
        TagCreationMenu tagCreationMenu = new();
        tagCreationMenu.OpenMenu();
    }
}