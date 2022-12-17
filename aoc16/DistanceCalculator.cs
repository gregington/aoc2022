using System.Collections.Immutable;

public static class DistanceCalculator {
    public static ImmutableDictionary<string, ImmutableDictionary<string, int>> Calculate(ImmutableDictionary<string, ImmutableArray<string>> network) {
        var valves = network.Keys.Order().ToImmutableArray();

        var distances = new Dictionary<string, Dictionary<string, int>>();
        foreach (var x in valves) {
            distances[x] = new Dictionary<string, int>();
            foreach (var y in valves) {
                distances[x][y] = int.MaxValue;
            }
        }

        foreach (var start in valves) {
            foreach (var destination in network[start]) {
                distances[start][destination] = 1;
            }
        }

        var done = false;
        while (!done) {
            done = true;
            foreach (var start in valves) {
                foreach (var end in valves) {
                    if (start != end) {
                        foreach (var through in valves) {
                            if (distances[start][through] == int.MaxValue || distances[through][end] == int.MaxValue) {
                                continue;
                            }
                            var cost = distances[start][through] + distances[through][end];
                            if (cost < distances[start][end]) {
                                done = false;
                                distances[start][end] = cost;
                            }
                        }
                    }
                }
            }
        }

        return distances.ToImmutableDictionary(kvp1 => kvp1.Key, kvp1 => kvp1.Value.ToImmutableDictionary(kvp2 => kvp2.Key, kvp2 => kvp2.Value));
    }
}