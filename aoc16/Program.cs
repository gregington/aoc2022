public class Program {
    public static void Main(string[] args) {
        var (network, flowRates) = Parser.Parse(args[0]);
        var maxPressureAlone = Solver.Solve(network, flowRates, 1, 30);
        Console.WriteLine($"Max pressure alone: {maxPressureAlone}");

        var maxPressureWithElephant = Solver.Solve(network, flowRates, 2, 26);
        Console.WriteLine($"Max pressure with elephant: {maxPressureWithElephant}");
    }
}