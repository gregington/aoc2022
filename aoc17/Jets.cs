using System.Collections.Immutable;

public static class Jets {
    public static ImmutableArray<char> GetJets(string path) {
        return File.ReadAllLines(path).First().ToImmutableArray();
    }
}