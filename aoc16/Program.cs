public class Program {
    public static void Main(string[] args) {
        var (network, flowRates) = Parser.Parse(args[0]);
        var maxPressure = Solver.Solve(network, flowRates);
        Console.WriteLine($"Max pressure: {maxPressure}");
    }
}