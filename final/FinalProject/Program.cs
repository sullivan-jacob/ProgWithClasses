using System;
using System.Collections.Generic;
class Program {
    private static Dictionary<string, Action> mainMenuActions = [];
    private static Dictionary<string, Action> fileCreatorMenuActions = [];
    private static List<Tag> tags = [];
    private static List<TypeTag> typeTags = [];
    private static DataManager dataManager = new("/home/jacob/Documents/programming/ProgWithClasses/ProgWithClasses/final/FinalProject/ProgramData/TypeTags.tfm", "/home/jacob/Documents/programming/ProgWithClasses/ProgWithClasses/final/FinalProject/ProgramData/Tags.tfm");
    static void Main(string[] args) {

        (typeTags, tags) = dataManager.ReadFiles();
        MenuInitializer();
        Menu(mainMenuActions);
    }
    private static void MenuInitializer() {
        /*mainMenuActions*/
        mainMenuActions.Add("Navigate Tag System",TagSystemNavigator);
        mainMenuActions.Add("Create a New File",FileCreator);
        mainMenuActions.Add("Create a New Tag", TagCreator);
        mainMenuActions.Add("Save and Exit",Exit);
        /*fileCreatorMenuActions*/
        fileCreatorMenuActions.Add("Create Empty File",CreateEmptyFile);
        fileCreatorMenuActions.Add("Create File and Edit Contents",CreateFile);

    }
    private static void Menu(Dictionary<string, Action> menuActions) {
        int cursorPosition = 0;
        List<string> Actions = [];
        foreach (string key in menuActions.Keys) {
            Actions.Add(key);
        }
        bool moved = true;
        int menuPosition = 0;
        Console.CursorVisible = false;
        ConsoleColor normalBackground = Console.BackgroundColor;
        ConsoleColor normalForeground = Console.ForegroundColor;
        ConsoleColor selectedBackground = ConsoleColor.White;
        ConsoleColor selectedForeground = ConsoleColor.Black;
        while (true) {
            int consoleHeight = Console.WindowHeight;
            if (menuPosition > 0 && cursorPosition == 0) {
                menuPosition--;
                cursorPosition++;
                moved = true;
            }
            else if (menuPosition < Actions.Count - consoleHeight && cursorPosition == consoleHeight) {
                menuPosition++;
                cursorPosition--;
                moved = true;
            }
            if (moved) {
                Console.Clear();
                for (int line = 0; line < cursorPosition; line++) {
                    Console.WriteLine(Actions[menuPosition + line]);
                }
                Console.BackgroundColor = selectedBackground;
                Console.ForegroundColor = selectedForeground;
                Console.WriteLine(Actions[menuPosition + cursorPosition]);
                Console.BackgroundColor = normalBackground;
                Console.ForegroundColor = normalForeground;
                for (int line = cursorPosition + 1; line < consoleHeight - 2 && line < Actions.Count; line++) {
                    Console.WriteLine(Actions[menuPosition + line]);
                }
                Console.Write("Enter : Select");
                if (menuActions != mainMenuActions) {
                    Console.WriteLine(" | BKSP : Back");
                }
                else {
                    Console.WriteLine();
                }
            }
            moved = false;
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.UpArrow && cursorPosition > 0){
                    cursorPosition--;
                    moved = true;
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow && cursorPosition < Actions.Count - 1){
                    cursorPosition++;
                    moved = true;
            }
            else if (keyInfo.Key == ConsoleKey.Enter) {
                Console.CursorVisible = true;
                try {
                    menuActions[Actions[menuPosition + cursorPosition]]();
                }
                catch (Exception ex) {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                }
                finally {
                    Console.CursorVisible = false;
                    moved = true; // This will force the menu to redraw
                }
            }
            else if (keyInfo.Key == ConsoleKey.Backspace && menuActions != mainMenuActions) {
        /*=>BREAK STATEMENT<=*/
                break;
            }
        }
        Console.Clear();
        Console.CursorVisible = true;
    }
    private static void TagSystemNavigator() {

    }
    private static void FileCreator() {
        Menu(fileCreatorMenuActions);
    }
    private static void TagCreator() {

    }
    private static void Save() {
        dataManager.WriteFiles(typeTags, tags);
    }
    private static void Exit() {
        Save();
        Console.Clear();
        Environment.Exit(0);
    }
    private static void CreateEmptyFile() {
    
    }
    private static void CreateFile() {

    }
}