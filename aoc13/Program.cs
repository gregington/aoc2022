using System.Collections.Immutable;

public class Program {
    public static void Main(string[] args) {
        var pairs = Parser.Parse(args[0]).ToImmutableArray();

        var sumOfRightOrderIndices = pairs
            .Select((x, i) => (Index: i + 1, Result: OrderChecker.InOrder(x.Item1, x.Item2)))
            .Where(x => x.Result == OrderResult.RIGHT_ORDER)
            .Select(x => x.Index)
            .Sum();

        Console.WriteLine($"Sum or right order indices: {sumOfRightOrderIndices}");
    }
}