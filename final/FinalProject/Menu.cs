abstract class Menu {
    protected Dictionary<string/*Item name*/, Action/*Action upon selection*/> menuItemActions;
    protected string[] menuItemsArray; //Set during runtime
    protected Dictionary<ConsoleKey/*Key that triggers actions*/, Action/*Action pointer*/> keyActions;
    protected string actionsDisplayString;
    protected bool changed = true; 
    protected int cursorPosition = 0;
    protected int menuPosition = 0;
    protected bool exitMenu = false;
    protected int consoleHeight; //Set during runtime
    protected int consoleWidth; //Set during runtime
    protected ConsoleKeyInfo keyInfo; //Set during runtime

    /*Menu Constructor Example:
    public MainMenu() {
        menuItemActions = new Dictionary<string, Action> {
            {"Open Tag Navigator", NavigateTags},
            {"Create New Tag", CreateTag},
            {"Save", Program.Save},
            {"Save and Exit", Program.Exit}
        };
        keyActions = new Dictionary<ConsoleKey, Action> {
            {ConsoleKey.UpArrow, MoveSelectionUp},
            {ConsoleKey.DownArrow, MoveSelectionDown},
            {ConsoleKey.RightArrow, Select}
        };
        actionsDisplayString = "Up Arrow : Move Up | Down Arrow : Move Down | Enter : Select";
    }
    */

    public virtual void OpenMenu(){
        InitializeMenuItemActions();
        InitializeMenuKeyActions();
        if (menuItemActions.Count == 0) {
            Console.Clear();
            Console.WriteLine("No Options Provided By Menu.\nPress any key to continue");
            Console.ReadKey(true);
            return;
        }
        InitializeMenu();
        while (!exitMenu) {
            DisplayMenu();
            RunKeyActions();
        }
    }
    protected virtual void InitializeMenu() {
        consoleHeight = Console.WindowHeight;
        consoleWidth = Console.WindowWidth;
        cursorPosition = 0;
        menuPosition = 0;
        Console.CursorVisible = false;
    }
    protected abstract void InitializeMenuItemActions();
    protected abstract void InitializeMenuKeyActions();
//Menu display methods
    protected virtual void DisplayMenu() {
        UpdateMenuDimensions();
        UpdateMenuPosition();
        if (changed) {
            UpdateMenuDisplay();
            changed = false;
        }
    }
    protected virtual void UpdateMenuDimensions() {
        if (consoleHeight != Console.WindowHeight) {
            consoleHeight = Console.WindowHeight;
            changed = true;
        }
        if (consoleWidth != Console.WindowWidth) {
            consoleWidth = Console.WindowWidth;
            changed = true;
        }
    }
    protected virtual void UpdateMenuPosition() {
        if (menuPosition > 0 && cursorPosition == 0) {
            menuPosition--;
            cursorPosition++;
            changed = true;
        }
        else if (cursorPosition + 3 == consoleHeight && menuPosition + consoleHeight - 2 < menuItemsArray.Length) {
            menuPosition++;
            cursorPosition--;
            changed = true;
        }
    }
    protected virtual void UpdateMenuDisplay() {
        Console.Clear();
        DisplayMenuItems();
        DisplayMenuActions();
    }
    protected virtual void DisplayMenuItems() {
        for (int line = 0; line < cursorPosition; line++) {
            Console.WriteLine(menuItemsArray[menuPosition + line]);
        }
        DisplaySelectedItem();
        for (int line = cursorPosition + 1; line < consoleHeight - 2 && line < menuItemsArray.Length; line++) {
            Console.WriteLine(menuItemsArray[menuPosition + line]);
        }
    }
    protected virtual void DisplaySelectedItem() {
        ConsoleColor normalBackground = Console.BackgroundColor;
        ConsoleColor normalForeground = Console.ForegroundColor;
        try {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(menuItemsArray[menuPosition + cursorPosition]);
            Console.BackgroundColor = normalBackground;
            Console.ForegroundColor = normalForeground;
        }
        catch {
            Console.BackgroundColor = normalBackground;
            Console.ForegroundColor = normalForeground;
            //This is here because if the code in the try block fails, it would end up messing up the console colors. This fixes that problem.
        }
    }
    protected virtual void DisplayMenuActions() {
        for (int spacerLine = menuItemsArray.Length; spacerLine < consoleHeight - 2; spacerLine++) {
            Console.WriteLine();
        }
        Console.WriteLine(actionsDisplayString);
    }
//Menu interaction methods
    protected virtual void RunKeyActions() {
        keyInfo = Console.ReadKey(true);
        if (keyActions.TryGetValue(keyInfo.Key, out Action action)) {
            action();
        }
    }


    //MenuItemActions
    protected virtual void Pass() {
        //Does nothing
    }
    

    //KeyActions
    protected virtual void MoveSelectionUp() {
        if (cursorPosition > 0) {
            cursorPosition--;
            changed = true;
        }
    }
    protected virtual void MoveSelectionDown() {
        if (cursorPosition < consoleHeight - 3 && cursorPosition < menuItemsArray.Length - 1){
            cursorPosition++;
            changed = true;
        }
    }
    protected virtual void Select() {
        Console.CursorVisible = true;
        try {
            menuItemActions[menuItemsArray[menuPosition + cursorPosition]]();
        }
        catch (Exception ex) {
            Console.WriteLine($"An error occurred: {ex}");
            Console.WriteLine("Press a key to continue");
            Console.ReadKey(true);
        }
        finally {
            Console.CursorVisible = false;
            changed = true;
        }
    }
    protected virtual void Back() {
        exitMenu = true;
    }
}