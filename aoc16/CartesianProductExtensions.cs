using System.Collections.Immutable;

public static class CartesianProductExtensions {
    public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this ImmutableArray<ImmutableArray<T>> source) {
        return source.Aggregate(
            (IEnumerable<IEnumerable<T>>) new [] { Enumerable.Empty<T>() },
            (acc, src) => src.SelectMany(x => acc.Select(a => a.Concat(new [] {x})))
        );
    }
}