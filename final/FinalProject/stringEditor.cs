class StringEditor{
    private static string oldString;
    private static string newString;
    private static int cursorPosition;
    private static bool runStringEditor;
    public static string RunStringEditor(string name, string promptString) {
        cursorPosition = 0;
        oldString = name;
        newString = $"{name} ";
        runStringEditor = true;
        while (runStringEditor) {
            DisplayStringEditor(promptString);
            RunKeyActions();
        }
        return newString.Trim();
    }
    private static void DisplayStringEditor(string promptString) {
        Console.Clear();
        Console.Write($"{promptString}: {newString[..cursorPosition]}");
        ConsoleColor normalBackground = Console.BackgroundColor;
        ConsoleColor normalForeground = Console.ForegroundColor;
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(newString.Substring(cursorPosition, 1));
        Console.BackgroundColor = normalBackground;
        Console.ForegroundColor = normalForeground;
        Console.WriteLine(newString[(cursorPosition + 1)..]);
        Console.WriteLine();
        Console.WriteLine("Enter : Save | End : Cancel");
    }
    private static void RunKeyActions() {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        if (keyInfo.Key == ConsoleKey.Enter) {
            runStringEditor = false;
        }
        else if (keyInfo.Key == ConsoleKey.End) {
            runStringEditor = false;
            newString = oldString;
        }
        else if (keyInfo.Key == ConsoleKey.Backspace && cursorPosition != 0) {
            newString = newString.Remove(--cursorPosition , 1);
        }
        else if (keyInfo.Key == ConsoleKey.Delete && cursorPosition < newString.Length - 1) {
            newString = newString.Remove(cursorPosition , 1);
            if (cursorPosition > 0) {
                cursorPosition--;
            }
        }
        else if (keyInfo.Key == ConsoleKey.LeftArrow && cursorPosition > 0) {
            cursorPosition--;
        }
        else if (keyInfo.Key == ConsoleKey.RightArrow && cursorPosition < newString.Length - 1) {
            cursorPosition++;
        }
        else if (!char.IsControl(keyInfo.KeyChar)) {
            newString = newString.Insert(cursorPosition, keyInfo.KeyChar.ToString());
            cursorPosition++;
        }
    }

}