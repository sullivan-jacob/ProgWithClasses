using System.Collections.Generic;
using System.IO;

class DataManager {
    private string tagFilePath;
    private string typeTagFilePath;

    public DataManager(string tagFilePath, string typeTagFilePath) {
        this.tagFilePath = tagFilePath;
        this.typeTagFilePath = typeTagFilePath;
    }
    public (List<TypeTag>,List<Tag>) ReadFiles() {
        Dictionary<string, Tag> tagsDict = [];
        Dictionary<string, TypeTag> typeTagsDict = [];
        string[] typeTagFileLines = File.ReadAllLines(typeTagFilePath);
        for (int index = 0; index < typeTagFileLines.Length - 3; index += 3) {
            string name = typeTagFileLines[index];
            TypeTag typeTag = new(name);
            typeTagsDict.Add(name, typeTag);
            typeTag.SetFilePath(typeTagFileLines[index + 1]);
        }
        string[] tagFileLines = File.ReadAllLines(tagFilePath);
        for (int index = 0; index < tagFileLines.Length - 4; index += 5) {
            string name = tagFileLines[index];
            Tag tag = new(name);
            tagsDict.Add(name, tag);
            tag.SetFilePath(tagFileLines[index + 1]);
            string typeTagLine = tagFileLines[index + 2];
            string parentLine = tagFileLines[index + 3];
            string childLine = tagFileLines[index + 4];
            string[] typeTags = typeTagLine.Split("\\|/")[1..];
            foreach (string typeTag in typeTags) {
                if (typeTagsDict.TryGetValue(typeTag, out TypeTag value)) {
                    tag.AddTypeTag(value);
                }
            }
            string[] parentTags = parentLine.Split("/|\\")[1..];
            foreach (string parentTag in parentTags) {
                if (tagsDict.TryGetValue(parentTag, out Tag value)) {
                    tag.AddParent(value);
                }
            }
            string[] childTags = childLine.Split("\\|/")[1..];
            foreach (string childTag in childTags) {
                if (tagsDict.TryGetValue(childTag, out Tag value)) {
                    tag.AddChild(value);
                }
            }
        }
        List<TypeTag> typeTagsList = [];
        foreach (TypeTag typeTag in typeTagsDict.Values) {
            typeTagsList.Add(typeTag);
        }
        List<Tag> tagsList = [];
        foreach (Tag tag in tagsDict.Values) {
            tagsList.Add(tag);
        }
        return (typeTagsList, tagsList);
    }
    public void WriteFiles(List<TypeTag> typeTags,List<Tag> tags) {
        List<string> typeTagLines = [];
        foreach (TypeTag tag in typeTags) {
            typeTagLines.Add(tag.GetName());
            typeTagLines.Add(tag.GetFilePath());
            string childLine = "";
            foreach (Tag child in tag.GetChildren().Values) {
                childLine += $"\\|/{child.GetName}";
            }
            typeTagLines.Add(childLine);
        }
        File.WriteAllLines(typeTagFilePath, typeTagLines);
        List<string> tagLines = [];
        foreach (Tag tag in tags) {
            tagLines.Add(tag.GetName());
            tagLines.Add(tag.GetFilePath());
            string typeTagLine = "";
            foreach (TypeTag typeTag in tag.GetTypeTags().Values) {
                typeTagLine += $"\\|/{typeTag.GetName}";
            }
            tagLines.Add(typeTagLine);
            string parentLine = "";
            foreach (Tag parent in tag.GetParents().Values){
                parentLine += $"/|\\{parent.GetName()}";
            }
            tagLines.Add(parentLine);
            string childLine = "";
            foreach (Tag child in tag.GetChildren().Values) {
                childLine += $"\\|/{child.GetName}";
            }
            tagLines.Add(childLine);
        }
        File.WriteAllLines(tagFilePath, tagLines);
    }
}