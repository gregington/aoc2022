using System.Collections.Immutable;

public static class NonBeaconPositionCalculator {
    public static ImmutableArray<Range> ForRow(ImmutableDictionary<Point, int> sensorDistances, ImmutableDictionary<Point, int> beaconDistances, int row) {
        var sensorExtents = sensorDistances
            .Select(s => ExtentCalculator.Calculate(s.Key, s.Value));

        return sensorExtents
            .Select(e => e.IntersectRow(row))
            .Where(r => r.HasValue)
            .Select(r => r!.Value)
            .Combine()
            .ToImmutableArray();
    }

    public static IEnumerable<Range> Combine(this IEnumerable<Range> source) {
        var ordered = source.OrderBy(x => x.Min);
        Range? merged = null;
        using (var enumerator = ordered.GetEnumerator()) {
            while (enumerator.MoveNext()) {
                var current = enumerator.Current;
                if (merged == null) {
                    merged = current;
                    continue;
                }

                if (merged.Value.Intersects(current)) {
                    merged = merged.Value.Union(current);
                    continue;
                }
                
                yield return merged.Value;
                merged = current;
            }
        }
        if (merged.HasValue) {
            yield return merged.Value;
        }
    }
}