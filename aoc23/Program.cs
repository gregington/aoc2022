public class Program {
    public static void Main(string[] args) {
        var elfPositions = Parser.Parse(args[0]);

        var finalPositions = Simulator.Simulate(elfPositions, 10);
        var emptyTiles = EmptyTileCalculator.Calculate(finalPositions);
        Console.WriteLine($"Empty tiles: {emptyTiles}");
    }
}
