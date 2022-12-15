using System.Collections.Immutable;

public static class NonBeaconPositionCalculator {
    public static ImmutableArray<Point> ForRow(ImmutableDictionary<Point, int> sensorDistances, ImmutableDictionary<Point, int> beaconDistances, int row) {
        var sensorExtents = sensorDistances
            .Select(s => ExtentCalculator.Calculate(s.Key, s.Value));

        return sensorExtents
            .SelectMany(e => e.IntersectRow(row))
            .Distinct()
            .Where(p => !beaconDistances.Keys.Contains(p) && !sensorDistances.Keys.Contains(p))
            .OrderBy(p => p.X)
            .ToImmutableArray();
    }
}