class Program {
    private static List<Tag> tags = [];
    private static DataManager dataManager = new("/home/jacob/Documents/programming/ProgWithClasses/ProgWithClasses/prove/Develop05/ProgramData/Tags.tfm");
    static void Main(string[] args) {
        tags = dataManager.ReadFiles();
        MainMenu mainMenu = new();
        mainMenu.OpenMenu();
    }

    public static void Save() {
        dataManager.WriteFiles(tags);
    }
    public static void Exit() {
        Save();
        Console.Clear();
        Environment.Exit(0);
    }
    public static List<Tag> GetTags() {
        if (tags == null) {
            Console.WriteLine("Warning: tags list is null");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
            return new List<Tag>();
        }
        return tags;
    }
    public static void AddTag(Tag newTag) {
        tags.Add(newTag);
    }
}