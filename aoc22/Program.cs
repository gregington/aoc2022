using System.Collections.Immutable;

public class Program {
    public static void Main(string[] args) {
        var (map, instructions) = Parser.Parse(args[0]);

        var startSquare = GraphCreator.Create(map);
        var (x, y, direction) = Mover.Move(startSquare, Direction.Right, instructions);
        var password = PasswordGenerator.Generate(x, y, direction);
        Console.WriteLine($"({x}, {y}, {direction})");
        Console.WriteLine($"Password: {password}");
    }

    private static void PrintMap(ImmutableArray<ImmutableArray<char>> map) {
        foreach (var row in map) {
            foreach (var c in row) {
                Console.Write(c);
            }
            Console.WriteLine();
        }
    }

    private static void PrintInstructions(IEnumerable<Instruction> instructions) {
        foreach (var instruction in instructions) {
            Console.WriteLine(instruction);
        }
    }
}