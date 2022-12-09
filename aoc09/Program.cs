public class Program {
    public static void Main(string[] args) {
        var moves = Parser.Parse(args[0]);
        var tailPositions = TailPositionCalculator.Calculate(moves);
        Console.WriteLine($"Unique number of positions: {tailPositions.Count()}");
    }
}