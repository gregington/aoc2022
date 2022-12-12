public class Program {
    public static void Main(string[] args) {
        var (start, end, grid) = GridParser.Parse(args[0]);

        var path = PathFinder.FindPath(start, end, int.MaxValue, grid);
        var numSteps = path.Count() - 1; // -1 to exclude start point
        Console.WriteLine($"Min steps: {numSteps}"); 

        var shortestPathFromAnyLowPoint = PathFinder.FindShortestPathFromLowestPoints(end, grid);
        var numStepsFromAnyLowPoint = shortestPathFromAnyLowPoint.Count() - 1;
        Console.WriteLine($"Min steps from any low point: {numStepsFromAnyLowPoint}");
    }
}
