using System.Collections.Immutable;
using System.Text.RegularExpressions;

public static class Parser {
    private static Regex PointRegex = new Regex(@"^(?<x>\d+),(?<y>\d+)$");

    public static ImmutableHashSet<Point> ParseRocks(string path) {
        return ParseRocks(File.ReadLines(path));
    }

    public static ImmutableHashSet<Point> ParseRocks(IEnumerable<string> lines) {
        return lines.SelectMany(ParseWalls)
            .ToImmutableHashSet();
    }

    private static IEnumerable<Point> ParseWalls(string line) {
        var vertices = line.Split(" -> ")
            .Select(x => PointRegex.Match(x))
            .Where(m => m.Success)
            .Select(m => m.Groups)
            .Select(g => new Point(Convert.ToInt32(g["x"].Value), Convert.ToInt32(g["y"].Value)));

        return vertices.Zip(vertices.Skip(1))
            .SelectMany(x => MakeWall(x.First, x.Second));
    }

    private static IEnumerable<Point> MakeWall(Point a, Point b) {
        if (a.X == b.X) {
            var minY = Math.Min(a.Y, b.Y);
            var distance = Math.Abs(a.Y - b.Y) + 1;
            return Enumerable.Range(minY, distance)
                .Select(y => new Point(a.X, y));
        } else if (a.Y == b.Y) {
            var minX = Math.Min(a.X, b.X);
            var distance = Math.Abs(a.X - b.X) + 1;
            return Enumerable.Range(minX, distance)
                .Select(x => new Point(x, a.Y));
        } else {
            throw new ArgumentException($"{a} -> {b}");
        }
    }
}