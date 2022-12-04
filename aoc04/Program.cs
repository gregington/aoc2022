public class Program {
    public static void Main(string[] args) {
        var assignments = Parser.Parse(args[0]).ToList();
        var fullyContains = assignments.Select(x => Ranges.Contains(x.First, x.Second));
        var fullyContainsCount = fullyContains
            .Where(x => x)
            .Count();

        Console.WriteLine($"Number of fully contained pairs: {fullyContainsCount}");

        var overlaps = assignments.Select(x => Ranges.Overlaps(x.First, x.Second));
        var overlapsCount = overlaps
            .Where(x => x)
            .Count();
        
        Console.WriteLine($"Number of overlapping pairs: {overlapsCount}");
    }
}
