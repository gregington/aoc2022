using System.Collections.Immutable;

public static class Parser {
    public static IEnumerable<ImmutableList<HandShape>> Parse(string path) {
        return File.ReadLines(path)
            .Select(x => x.Split(" "))
            .Select(x => x.Select(y => HandShapeParser.Parse(y)).ToImmutableList());
    }
}