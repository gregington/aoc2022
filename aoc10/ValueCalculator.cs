using System.Text.RegularExpressions;

public static class ValueCalculator {
    private static Regex NoOpRegex = new Regex(@"^noop$");
    private static Regex AddXRegex = new Regex(@"^addx (?<value>-?\d+)$");

    public static IEnumerable<(int Cycle, int XDuring, int XAfter)> CalculateValues(string path) {
        return CalculateValue(File.ReadLines(path));
    }

    private static IEnumerable<(int Cycle, int XDuring, int XAfter)> CalculateValue(IEnumerable<string> lines) {
        using (var enumerator = lines.GetEnumerator()) {
            var cycle = 0;
            var x = 1;

            while (enumerator.MoveNext()) {
                cycle = cycle + 1;
                var line = enumerator.Current.Trim();
                
                var match = NoOpRegex.Match(line);
                if (match.Success) {
                    yield return (Cycle: cycle, XDuring: x, XAfter: x);
                    continue;
                }

                match = AddXRegex.Match(line);
                if (match.Success) {
                    yield return (Cycle: cycle, XDuring: x,  XAfter: x);

                    cycle = cycle + 1;
                    var xCycle2During = x;

                    var toAdd = Convert.ToInt32(match.Groups["value"].Value);
                    x = x + toAdd;
                    var xCycle2After = x;

                    yield return (Cycle: cycle, XDuring: xCycle2During, XAfter: xCycle2After);

                    continue;
                }

                throw new ArgumentException(line);
            }
        }
    }
}