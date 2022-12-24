using System.Collections.Immutable;

public static class Parser {
    public static Valley Parse(string path) {
        var lines = File.ReadAllLines(path);
        var yLength = lines.Length;
        var xLength = lines[0].Length;
        var start = new Point(lines[0].IndexOf('.'), 0);
        var goal = new Point(lines[yLength - 1].IndexOf('.'), yLength - 1);

        var walls = new HashSet<Point>();
        var blizzards = new HashSet<Blizzard>();
        for (int y = 0; y < yLength; y++) {
            for (int x = 0; x < xLength; x++) {
                var c = lines[y][x];
                if (c == '.') {
                    continue;
                }
                var point = new Point(x, y);
                if (c == '#') {
                    walls.Add(point);
                    continue;
                }
                blizzards.Add(new Blizzard(point, (Direction) c));
            }
        }

        var blizzardsByLocation = blizzards.GroupBy(b => b.Location)
            .ToImmutableDictionary(x => x.Key, x => x.ToImmutableArray());

        return new Valley(xLength, yLength, start, goal, blizzardsByLocation, walls.ToImmutableHashSet());
    }
}