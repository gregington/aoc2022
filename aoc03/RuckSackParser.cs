using System.Collections.Immutable;

public static class RucksackParser {
    public static ImmutableArray<ImmutableHashSet<char>> CompartmentContents(string rucksackContents) {
        var trimmedContents = rucksackContents.Trim();
        var midpoint = trimmedContents.Length / 2;
        return new string[] { trimmedContents.Substring(0, midpoint), trimmedContents.Substring(midpoint) }
            .Select(x => x.ToCharArray().ToImmutableHashSet())
            .ToImmutableArray();
    }
}