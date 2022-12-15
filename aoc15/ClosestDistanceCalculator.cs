using System.Collections.Immutable;

public static class ClosestDistanceCalculator {
    public static (ImmutableDictionary<Point, int> sensorDistances, ImmutableDictionary<Point, int> beaconDistances) Calculate(IEnumerable<Point> sensors, IEnumerable<Point> beacons) {
        var distances = sensors.Zip(beacons, (s, b) => (Sensor: s, Beacon: b))
            .Select(x => (x.Sensor, x.Beacon, Distance: x.Sensor.ManhattanDistance(x.Beacon)))
            .ToImmutableArray();

        var sensorDistances = distances.ToImmutableDictionary(x => x.Sensor, x => x.Distance);
        var beaconDistances = beacons.Distinct()
            .ToImmutableDictionary(b => b, b => distances.Where(d => d.Beacon == b).Select(d => d.Distance).Min());
        return (sensorDistances, beaconDistances);
    }
}