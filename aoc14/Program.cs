using System.Collections.Immutable;

public class Program {
    public static void Main(string [] args) {
        var source = new Point(500, 0);
        var rocks = Parser.ParseRocks(args[0]);

        var sand = SandDropper.Drop(rocks, source);

        Console.WriteLine(GridPrinter.Print(rocks, sand, source));
        Console.WriteLine($"Number of grains: {sand.Count()}");
    }
}
