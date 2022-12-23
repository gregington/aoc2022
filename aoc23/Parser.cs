using System.Collections.Immutable;

public static class Parser {
    public static ImmutableHashSet<Point> Parse(String path) =>
        File.ReadLines(path)
            .SelectMany((line, y) => line.ToCharArray().Select((c, x) => (X: x, Y: y, C: c)))
            .Where(p => p.C == '#')
            .Select(p => new Point(p.X, p.Y))
            .ToImmutableHashSet();
        
}