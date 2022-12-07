using System.Text.RegularExpressions;

public static class Parser {

    private static Regex ChangeToRootDirectoryRegex = new Regex(@"^\$ cd /$");
    private static Regex ChangeUpDirectoryRegex = new Regex(@"^\$ cd \.\.$");
    private static Regex ChangeDirectoryRegex = new Regex(@"^\$ cd (?<dirname>.*)$");
    private static Regex LsRegex = new Regex(@"^\$ ls$");
    private static Regex DirectoryRegex = new Regex(@"^dir (?<dirname>.*)$");
    private static Regex FileRegex = new Regex(@"^(?<size>\d+) (?<filename>.*)$");


    public static Directory? Parse(string path) {
        return Parse(System.IO.File.ReadLines(path));
    }

    private static Directory? Parse(IEnumerable<string> lines) {
        Directory? root = null;
        Directory? current = null;

        foreach (var line in lines.Select(x => x.Trim())) {
            Match match = ChangeToRootDirectoryRegex.Match(line);
            if (match.Success) {
                (root, current) = CreateRootDirectory();
                continue;
            }

            match = ChangeUpDirectoryRegex.Match(line);
            if (match.Success) {
                current = ChangeUpDirectory(current!);
                continue;
            }

            match = ChangeDirectoryRegex.Match(line);
            if (match.Success) {
                current = ChangeDirectory(current!, match.Groups["dirname"].Value);
                continue;
            }

            match = LsRegex.Match(line);
            if (match.Success) {
                // No-op!
                continue;
            }

            match = DirectoryRegex.Match(line);
            if (match.Success) {
                AddDirectory(current!, match.Groups["dirname"].Value);
            }

            match = FileRegex.Match(line);
            if (match.Success) {
                AddFile(current!, match.Groups["filename"].Value, Convert.ToInt32(match.Groups["size"].Value));
            }
        }

        return root;
    }

    private static (Directory Root, Directory Current) CreateRootDirectory() {
        var root = Directory.Create("/") ;
        return (root, root);
    }

    private static Directory ChangeUpDirectory(Directory current) => current.Parent!;

    private static Directory ChangeDirectory(Directory current, string dirname) => 
        current.Contents.Where(x => x is Directory)
            .Where(x => x.Name == dirname)
            .Select(x => x as Directory)
            .First()!;
            
    private static void AddDirectory(Directory current, string dirname) {
        current.Add(Directory.Create(dirname));
    }

    private static void AddFile(Directory current, string filename, int size) {
        current.Add(File.Create(filename, size));
    }
}