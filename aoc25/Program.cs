public class Program {
    public static void Main(string[] args) {
        var fuelAmounts = Parser.Parse(args[0]);
        var fuelRequired = FuelCalculator.CalculateFuel(fuelAmounts);
        Console.WriteLine($"Fuel required: {fuelRequired}");
        Console.WriteLine($"Fuel required as Snafu: {SnafuNumber.IntToSnafu(fuelRequired)}");
    }
}