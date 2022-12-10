using System.Text.RegularExpressions;

public static class SignalStrengthCalculator {

    private static Regex NoOpRegex = new Regex(@"^noop$");
    private static Regex AddXRegex = new Regex(@"^addx (?<value>-?\d+)$");


    public static IEnumerable<(int Cycle, int XDuring, int SignalStrengthDuring, int XAfter, int SignalStrengthAfter)> CalculateSignalStrength(string path) {
        return CalculateSignalStrength(File.ReadLines(path));
    }

    private static IEnumerable<(int Cycle, int XDuring, int SignalStrengthDuring, int XAfter, int SignalStrengthAfter)> CalculateSignalStrength(IEnumerable<string> lines) {
        using (var enumerator = lines.GetEnumerator()) {
            var cycle = 0;
            var x = 1;

            while (enumerator.MoveNext()) {
                cycle = cycle + 1;
                var line = enumerator.Current.Trim();
                
                var match = NoOpRegex.Match(line);
                if (match.Success) {
                    var signalStrength = CalculateSignalStrength(cycle, x);
                    yield return (Cycle: cycle, XDuring: x, SignalStrengthDuring: signalStrength, XAfter: x, SignalStrengthAfter: signalStrength);
                    continue;
                }

                match = AddXRegex.Match(line);
                if (match.Success) {
                    var signalStrengthCycle1 = CalculateSignalStrength(cycle, x);
                    yield return (Cycle: cycle, XDuring: x, SignalStrengthDuring: signalStrengthCycle1, XAfter: x, SignalStrengthAfter: signalStrengthCycle1);

                    cycle = cycle + 1;
                    var xCycle2During = x;
                    var signalStrengthCycle2During = CalculateSignalStrength(cycle, xCycle2During);

                    var toAdd = Convert.ToInt32(match.Groups["value"].Value);
                    x = x + toAdd;
                    var xCycle2After = x;
                    var signalStrengthCycle2After = CalculateSignalStrength(cycle, x);

                    yield return (Cycle: cycle, XDuring: xCycle2During, SignalStrengthDuring: signalStrengthCycle2During, XAfter: xCycle2After, SignalStrengthAfter: signalStrengthCycle2After);

                    continue;
                }

                throw new ArgumentException(line);
            }    
        }
    }

    private static int CalculateSignalStrength(int cycle, int x) =>
        cycle * x;
}