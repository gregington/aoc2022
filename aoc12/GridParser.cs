using System.Collections.Immutable;

public static class GridParser {
    public static (Point Start, Point End, ImmutableArray<ImmutableArray<char>> Grid) Parse(string path) {
        return Parse(File.ReadAllLines(path).ToImmutableArray());
    }

    public static (Point Start, Point End, ImmutableArray<ImmutableArray<char>> Grid) Parse(IList<string> lines) {
        var start = new Point(-1, -1);
        var end = new Point(-1, -1);

        var grid = new List<ImmutableArray<char>>();

        for (var y = 0; y < lines.Count(); y++) {
            var line = lines[y];

            var startIndex = line.IndexOf('S');
            if (startIndex != -1) {
                start = new Point(startIndex, y);
                line = line.Replace('S', 'a');
            }

            var endIndex = line.IndexOf('E');
            if (endIndex != -1) {
                end = new Point(endIndex, y);
                line = line.Replace('E', 'z');
            }

            grid.Add(line.ToCharArray().ToImmutableArray());
        }

        return (start, end, grid.ToImmutableArray());
    }        
}