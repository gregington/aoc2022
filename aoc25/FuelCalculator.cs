public static class FuelCalculator {

    public static long CalculateFuel(IEnumerable<string> values) =>
        (values.Select(SnafuNumber.SnafuToInt).Sum());
}