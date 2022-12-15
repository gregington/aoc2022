public class Program {
    public static void Main(string[] args) {
        var (sensors, beacons) = Parser.Parse(args[0]);
        var (sensorDistances, beaconDistances) = ClosestDistanceCalculator.Calculate(sensors, beacons);

        var row = Convert.ToInt32(args[1]);
        var nonBeaconPositions = NonBeaconPositionCalculator.ForRow(sensorDistances, beaconDistances, row);
        Console.WriteLine($"Non beacon positions: {nonBeaconPositions.Count()}");
    }
}
