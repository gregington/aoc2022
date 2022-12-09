public class Program {
    public static void Main(string[] args) {
        var moves = Parser.Parse(args[0]);

        var tailPositions2 = TailPositionCalculator.Calculate(moves, 2);
        Console.WriteLine($"Unique number of positions for 2 knots: {tailPositions2.Count()}");

        var tailPositions10 = TailPositionCalculator.Calculate(moves, 10);
        Console.WriteLine($"Unique number of positions for 10 knots: {tailPositions10.Count()}");

    }
}