using System.Collections.Immutable;

public static class TuningFrequencyCalculator {
    public static long Calculate(ImmutableDictionary<Point, int> sensorDistances, ImmutableDictionary<Point, int> beaconDistances, int maxSearchCoordinate) {
        var range = new Range(0, maxSearchCoordinate);
        for (var y = 0; y <= maxSearchCoordinate; y++) {
            var nonBeaconPositions = NonBeaconPositionCalculator.ForRow(sensorDistances, beaconDistances, y);
            if (nonBeaconPositions.Count() <= 1) {
                continue;
            }

            return (long) (nonBeaconPositions.First().Max + 1) * 4000000 + y;
        }
        return -1;
    }
}