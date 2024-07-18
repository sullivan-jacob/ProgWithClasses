class DataManager {
    private string tagFilePath;

    public DataManager(string tagFilePath) {
        this.tagFilePath = tagFilePath;
    }
    public List<Tag> ReadFiles() {
        Dictionary<string, Tag> tagsDict = [];
        string[] tagFileLines = [""];
        try {
            tagFileLines = File.ReadAllLines(tagFilePath);
        }
        catch (Exception ex) {
            Console.WriteLine($"Error Reading Tag File: {ex.Message}");
        }
        if (tagFileLines.Length % 4 == 0) {
            tagFileLines = [.. tagFileLines, ""];
        }
        for (int index = 0; index < tagFileLines.Length - 3; index += 4) {
            string name = tagFileLines[index];
            Tag tag = new(name);
            tagsDict.Add(name, tag);
            tag.SetFilePath(tagFileLines[index + 1]);
            string parentLine = tagFileLines[index + 2];
            string[] parentTagNames = parentLine.Split("|||");
            foreach (string parentTagName in parentTagNames) {
                if (tagsDict.TryGetValue(parentTagName, out Tag value)) {
                    tag.AddParent(value);
                }
            }
            string childLine = tagFileLines[index + 3];
            string[] childTagNames = childLine.Split("|||");
            foreach (string childTagName in childTagNames) {
                if (tagsDict.TryGetValue(childTagName, out Tag value)) {
                    value.AddParent(tag);
                }
            }
        }
        List<Tag> tagsList = [];
        foreach (Tag tag in tagsDict.Values) {
            if (tag != null && tag.GetName() != null) {
                tagsList.Add(tag);
            }
            else{
                Console.WriteLine("Null Tag");
                Console.ReadKey(true);
            }
        }
        return tagsList;
    }
    public void WriteFiles(List<Tag> tags) {
        List<string> tagLines = [];
        foreach (Tag tag in tags) {
            tagLines.Add(tag.GetName());
            tagLines.Add(tag.GetFilePath());
            string parentLine = "";
            foreach (Tag parent in tag.GetParents().Values){
                if (parentLine == "") {
                    parentLine += parent.GetName();
                } else {
                    parentLine += $"|||{parent.GetName()}";
                }
            }
            tagLines.Add(parentLine);
            string childLine = "";
            foreach (Tag child in tag.GetChildren().Values) {
                if (childLine == "") {
                    childLine += child.GetName();
                } else {
                    childLine += $"|||{child.GetName()}";
                }
            }
            tagLines.Add(childLine);
        }
        File.WriteAllLines(tagFilePath, tagLines);
    }
}