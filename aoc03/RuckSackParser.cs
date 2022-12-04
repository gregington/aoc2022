using System.Collections.Immutable;

public static class RucksackParser {
    public static ImmutableArray<ImmutableHashSet<char>> CompartmentContents(string rucksackContents) {
        var trimmedContents = rucksackContents.Trim();
        var midpoint = trimmedContents.Length / 2;
        return new string[] { trimmedContents.Substring(0, midpoint), trimmedContents.Substring(midpoint) }
            .Select(x => x.ToCharArray().ToImmutableHashSet())
            .ToImmutableArray();
    }

    public static IEnumerable<ImmutableArray<ImmutableHashSet<char>>> GroupAndUniqueContents(this IEnumerable<string> source, int groupSize) {
        using (var enumerator = source.GetEnumerator()) {
            var array = ImmutableArray<ImmutableHashSet<char>>.Empty;
            while (enumerator.MoveNext()) {
                var current = enumerator.Current.Trim();
                var unique = current.ToCharArray().ToImmutableHashSet();
                array = array.Add(unique);
                if (array.Length == groupSize) {
                    yield return array;
                    array = ImmutableArray<ImmutableHashSet<char>>.Empty;
                }
            }
            if (array.Length != 0) {
                throw new ArgumentException($"Input length is not a multiple of {groupSize}");
            }
        }
    }
}