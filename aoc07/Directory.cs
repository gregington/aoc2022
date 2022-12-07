
using System.Text;

public class Directory : IEntry {    

    private Directory(string name, Directory? parent, IEnumerable<IEntry> contents) {
        Name = name;
        Parent = parent;
        Contents = contents.ToList();
    }

    public static Directory Create(string name) {
        return new Directory(name, null, Enumerable.Empty<IEntry>());
    }


    public void Add(Directory directory) {
        directory.Parent = this;
        Contents.Add(directory);
    }

    public void Add(File file) {
        Contents.Add(file);
    }

    public string Name { get; private set; }

    public Directory? Parent { get; private set; }

    public List<IEntry> Contents { get; private set; }

    public int Size => Contents.Select(x => x.Size).Sum();

    public override string ToString() {
        return $"{Name} (dir, size={Size})";
    }

    public String ToStringRecursive() => ToStringRecursive(0, new StringBuilder());

    private String ToStringRecursive(int level, StringBuilder sb) {
        sb.AppendLine($"{Indent(level)}- {ToString()}");
        foreach (var entry in Contents) {
            if (entry is Directory) {
                (entry as Directory)!.ToStringRecursive(level + 1, sb);
            } else if (entry is File) {
                sb.AppendLine($"{Indent(level + 1)}- {entry.ToString()}");
            }
        }
        return sb.ToString();
    }

    private String Indent(int level) => new String(' ', level * 2);
}