public class File : IEntry {

    private File(string name, int size) {
        Name = name;
        Size = size;
    }

    public static File Create(string name, int size) {
        return new File(name, size);
    }

    public string Name { get; private set; }
    public int Size { get; private set; }

    public override string ToString() {
        return $"{Name} (file, size={Size})";
    }
}