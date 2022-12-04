using System.Collections.Immutable;

public static class Parser {
    public static IEnumerable<ImmutableList<HandShape>> ParseAsHandShapes(string path) {
        return File.ReadLines(path)
            .Select(x => x.Split(" "))
            .Select(x => x.Select(y => HandShapeParser.Parse(y)).ToImmutableList());
    }

    public static IEnumerable<(HandShape Opponent, Result Result)> ParseAsOpponentAndResult(string path) {
        return File.ReadLines(path)
            .Select(x => x.Split(" "))
            .Select(x => (HandShapeParser.Parse(x[0]), ResultParser.Parse(x[1])));
    }
}