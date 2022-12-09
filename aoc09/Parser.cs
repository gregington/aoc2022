public static class Parser {
    public static IEnumerable<(int XIncrement, int YIncrement)> Parse(string path) {
        return File.ReadLines(path)
            .Select(line => ParseLine(line))
            .Expand();
    }

    private static (string Direction, int Count) ParseLine(string line) {
        var parts = line.Split(" ").Select(x => x.Trim()).ToArray();
        return (parts[0], Convert.ToInt32(parts[1]));
    }

    private static IEnumerable<(int XIncrement, int YIncrement)> Expand(this IEnumerable<(string Direction, int Count)> source) {
        using (var enumerator = source.GetEnumerator()) {
            while (enumerator.MoveNext()) {
                var current = enumerator.Current;
                var move = CreateMove(current.Direction);
                foreach (var _ in Enumerable.Range(0, current.Count)) {
                    yield return move;
                }
            }
        }
    }

    private static (int XIncrement, int YIncrement) CreateMove(string direction) {
        return direction switch {
            "U" => (0, 1),
            "R" => (1, 0),
            "D" => (0, -1),
            "L" => (-1, 0),
            _ => throw new ArgumentException($"Unknown direction {direction}")
        };
    }
}