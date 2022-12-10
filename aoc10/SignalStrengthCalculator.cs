using System.Text.RegularExpressions;

public static class SignalStrengthCalculator {

    private static Regex NoOpRegex = new Regex(@"^noop$");
    private static Regex AddXRegex = new Regex(@"^addx (?<value>-?\d+)$");


    public static IEnumerable<(int Cycle, int XDuring, int SignalStrengthDuring, int XAfter, int SignalStrengthAfter)> CalculateSignalStrength(IEnumerable<(int Cycle, int XDuring, int XAfter)> xValues) {
        return xValues
            .Select(a => (Cycle: a.Cycle, XDuring: a.XDuring, SignalStrengthDuring: CalculateSignalStrength(a.Cycle, a.XDuring), XAfter: a.XAfter, SignalStrengthAfter: CalculateSignalStrength(a.Cycle, a.XAfter)));
    }

    private static int CalculateSignalStrength(int cycle, int x) =>
        cycle * x;
}