using System.Collections.Immutable;

public static class Parser {
    public static IEnumerable<string> Parse(string path) {
        return File.ReadLines(path);
    }
}