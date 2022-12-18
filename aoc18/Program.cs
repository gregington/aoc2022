﻿public class Program {
    public static void Main(string[] args) {
        var scan = Parser.Parse(args[0]);
        var surfaceArea = SurfaceAreaCalculator.Calculate(scan);
        Console.WriteLine($"Surface area: {surfaceArea}");
    }
}