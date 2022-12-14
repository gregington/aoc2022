using System.Collections.Immutable;

public class Program {
    public static void Main(string [] args) {
        var source = new Point(500, 0);
        var rocks = Parser.ParseRocks(args[0]);

        var sand1 = SandDropper1.Drop(rocks, source);
        Console.WriteLine($"Number of grains part 1: {sand1.Count()}");

        var sand2 = SandDropper2.Drop(rocks, source);
        Console.WriteLine($"Number of grains part 2: {sand2.Count()}");

    }
}
