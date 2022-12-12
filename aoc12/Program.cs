public class Program {
    public static void Main(string[] args) {
        var (start, end, grid) = GridParser.Parse(args[0]);

        var path = PathFinder.FindPath(start, end, grid);
        Console.WriteLine(path.Count() -1 ); // -1 to exclude start point
    }
}
