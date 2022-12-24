using System.Collections.Immutable;

public class Program {
    public static void Main(string[] args) {
        var (map, instructions) = Parser.Parse(args[0]);

        var startSquare = GraphCreator.CreateCartesian(map);
        var (x, y, direction) = Mover.Move(startSquare, Direction.Right, instructions);
        var password = PasswordGenerator.Generate(x, y, direction);
        Console.WriteLine($"Row: {y}, Col: {x}, Facing: {direction} ({(int) direction})");
        Console.WriteLine($"Password for squares: {password}");

        var faceGrids = CubeMappings.FaceGrids(args[0]);
        var faceLinks = CubeMappings.FaceLinks(args[0]);
        var startSquareOnCube = GraphCreator.CreateCube(map, faceGrids, faceLinks);
        var (cubeX, cubeY, cubeDirection) = Mover.Move(startSquareOnCube, Direction.Right, instructions);
        var cubePassword = PasswordGenerator.Generate(cubeX, cubeY, cubeDirection);
        Console.WriteLine($"Row: {cubeY}, Col: {cubeX}, Facing: {cubeDirection} ({(int) cubeDirection})");
        Console.WriteLine($"Password for cubes: {cubePassword}");
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