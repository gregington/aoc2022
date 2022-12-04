using System.Collections.Immutable;

public static class CommonItemFinder {
    public static ImmutableHashSet<char> Find(ImmutableHashSet<char> compartment1, ImmutableHashSet<char> compartment2) {
        return compartment1.Intersect(compartment2);
    }
}