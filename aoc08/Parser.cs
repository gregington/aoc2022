using System.Collections.Immutable;

public static class Parser {
    public static ImmutableArray<ImmutableArray<int>> Parse(string path) => 
        File.ReadLines(path)
            .Select(x => x.Trim())
            .Select(x => x.ToCharArray())
            .Select(x => x.Select(y => Convert.ToInt32(y.ToString())).ToImmutableArray())
            .ToImmutableArray();
}