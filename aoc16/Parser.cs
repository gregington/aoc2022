using System.Collections.Immutable;
using System.Text.RegularExpressions;

public static class Parser {
    private static Regex LineRegex = new Regex(@"^Valve (?<valve>.+) has flow rate=(?<flowRate>\d+); tunnels? leads? to valves? ((?<destinations>\w+)(, )?)*$");

    public static (ImmutableDictionary<string, ImmutableArray<string>> Network, ImmutableDictionary<string, int> FlowRates) Parse(string path) {
        return Parse(File.ReadLines(path));
    }

    private static (ImmutableDictionary<string, ImmutableArray<string>> Network, ImmutableDictionary<string, int> FlowRates) Parse(IEnumerable<string> lines) {
        var groups = lines.Select(x => LineRegex.Match(x).Groups);

        var network = groups.ToImmutableDictionary(
            g => g["valve"].Value,
            g => g["destinations"].Captures.Select(c => c.Value).ToImmutableArray()
        );

        var flowRates = groups.ToImmutableDictionary(
            g => g["valve"].Value,
            g => Convert.ToInt32(g["flowRate"].Value)
        );

        return (network, flowRates);
    }    
}