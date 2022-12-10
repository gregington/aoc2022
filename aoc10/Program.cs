public class Program {
    public static void Main(string[] args) {

        var xValues = ValueCalculator.CalculateValues(args[0]);
        var signalStrengths = SignalStrengthCalculator.CalculateSignalStrength(xValues);

        var sampledSingalStrengths = signalStrengths
            .Where(x => x.Cycle == 20 || (x.Cycle > 20 && (x.Cycle - 20) % 40 == 0));

        var sumOfSignalStrengths = sampledSingalStrengths
            .Select(x => x.SignalStrengthDuring)
            .Sum();

        Console.WriteLine($"Sum of signal strengths: {sumOfSignalStrengths}");
        Console.WriteLine();
        Console.WriteLine(ScreenCalculator.GetScreen(xValues));
    }
}
