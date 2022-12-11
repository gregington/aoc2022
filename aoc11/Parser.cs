using System.Collections.Immutable;

public static class Parser {
    public static ImmutableArray<Monkey> ParseMonkeys(string path) {
        return ParseMonkeys(File.ReadLines(path));
    }

    public static ImmutableArray<Monkey> ParseMonkeys(IEnumerable<string> lines) {
        return lines.ChunkByNewLines()
            .Select(x => MonkeyParser.Parse(x))
            .ToImmutableArray();
    }

}