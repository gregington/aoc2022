public class Program {
    public static void Main(string[] args) {
        Directory root = Parser.Parse(args[0])!;
        Console.WriteLine($"Sum of directory sizes of at most 100000: {root.DirectoriesWithSizeAtMost(100000).Select(x => x.Size).Sum()}");

        Console.WriteLine($"Directory to delete: {root.FindDirectoryToDelete(70000000, 30000000)}");
    }
}