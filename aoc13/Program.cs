using System.Collections.Immutable;

public class Program {
    public static void Main(string[] args) {
        var pairs = Parser.ParsePairs(args[0]).ToImmutableArray();

        var sumOfRightOrderIndices = pairs
            .Select((x, i) => (Index: i + 1, Result: OrderChecker.InOrder(x.Item1, x.Item2)))
            .Where(x => x.Result == OrderResult.RIGHT_ORDER)
            .Select(x => x.Index)
            .Sum();

        Console.WriteLine($"Sum or right order indices: {sumOfRightOrderIndices}");

        var packets = Parser.ParseLines(args[0]).ToList();
        var dividers = new List<string>{ "[[2]]", "[[6]]" }.Select(Parser.ParseLine);

        var orderedPackets = PackerOrderer.ReorderPackets(packets, dividers);
        var decoderKey = DecoderKeyCalculator.Calculate(orderedPackets, dividers);
        Console.WriteLine($"Decoder key: {decoderKey}");
    }
}