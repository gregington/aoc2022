using System.Collections.Immutable;

public static class Parser {
    public static ImmutableHashSet<(int X, int Y, int Z)> Parse(string path) {
        return File.ReadLines(path)
            .Select(line => line.Split(","))
            .Select(parts => (Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2])))
            .ToImmutableHashSet();
    }
}