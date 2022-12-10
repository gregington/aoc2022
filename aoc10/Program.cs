public class Program {
    public static void Main(string[] args) {
        var signalStrengths = SignalStrengthCalculator.CalculateSignalStrength(args[0]);

        var sampledSingalStrengths = signalStrengths
            .Where(x => x.Cycle == 20 || (x.Cycle > 20 && (x.Cycle - 20) % 40 == 0));

        var sumOfSignalStrengths = sampledSingalStrengths
            .Select(x => x.SignalStrengthDuring)
            .Sum();

        Console.WriteLine($"Sum of signal strengths: {sumOfSignalStrengths}");
    }
}
