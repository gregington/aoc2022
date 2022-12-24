public class Program {
    public static void Main(string[] args) {
        var valley = Parser.Parse(args[0]);

        var numMoves = Simulation.Run(valley);
        Console.WriteLine($"Got to goal in {numMoves} minutes.");
    }
}