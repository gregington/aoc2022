using System.Collections.Immutable;

public class Program {
    public static void Main(string[] args) {
        var grid = Parser.Parse(args[0]);

        var numVisible = VisiblityCalculator.NumberVisible(grid);
        Console.WriteLine($"Number of trees visible: {numVisible}");

        var maxScenicScore = ScenicScoreCalculator.CalculateMax(grid);
        Console.WriteLine($"Max scenic score: {maxScenicScore}");
    }
}