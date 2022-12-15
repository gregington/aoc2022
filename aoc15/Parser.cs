using System.Collections.Immutable;
using System.Text.RegularExpressions;

public static class Parser {
    public static Regex LineRegex = new Regex(@"^Sensor at x=(?<sensorX>-?\d+), y=(?<sensorY>-?\d+): closest beacon is at x=(?<beaconX>-?\d+), y=(?<beaconY>-?\d+)$");

    public static (ImmutableArray<Point> sensors, ImmutableArray<Point> beacons) Parse(string path) {
        return Parse(File.ReadLines(path));
    }

    private static (ImmutableArray<Point> sensors, ImmutableArray<Point> beacons) Parse(IEnumerable<string> lines) {
        var groups = lines.Select(x => LineRegex.Match(x))
            .Where(x => x.Success)
            .Select(x => x.Groups).ToArray();

        var sensors = groups
            .Select(x => new Point(Convert.ToInt32(x["sensorX"].Value), Convert.ToInt32(x["sensorY"].Value)))
            .ToImmutableArray();

        var beacons = groups
            .Select(x => new Point(Convert.ToInt32(x["beaconX"].Value), Convert.ToInt32(x["beaconY"].Value)))
            .ToImmutableArray();

        return (sensors, beacons);
    }
}