public class Program {
    public static void Main(string[] args) {
        var file = args[0];
        var row = Convert.ToInt32(args[1]);
        var maxSearchCoordinate = Convert.ToInt32(args[2]);

        var (sensors, beacons) = Parser.Parse(file);
        var (sensorDistances, beaconDistances) = ClosestDistanceCalculator.Calculate(sensors, beacons);

        var nonBeaconPositions = NonBeaconPositionCalculator.ForRow(sensorDistances, beaconDistances, row);
        var numNonBeaconPositions = nonBeaconPositions.Select(x => x.Length).Sum();
        var numSensorsAndBeaconsInRow = sensors.Concat(beacons)
            .Distinct()
            .Where(a => a.Y == row)
            .Where(a => nonBeaconPositions.Any(r => r.Includes(a.X)))
            .Count();
        numNonBeaconPositions -= numSensorsAndBeaconsInRow;

        Console.WriteLine($"Non beacon positions: {numNonBeaconPositions}");

        var tuningFrequency = TuningFrequencyCalculator.Calculate(sensorDistances, beaconDistances, maxSearchCoordinate);
        Console.WriteLine($"Tuning frequency: {tuningFrequency}");
    }
}
