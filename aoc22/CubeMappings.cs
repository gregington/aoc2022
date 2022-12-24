using System.Collections.Immutable;

public static class CubeMappings {

    private static ImmutableDictionary<int, (int X, int Y)> ExampleFaceGrid = new Dictionary<int, (int X, int Y)> {
            [1] = (2, 0),
            [2] = (0, 1),
            [3] = (1, 1),
            [4] = (2, 1),
            [5] = (2, 2),
            [6] = (3, 2)
        }.ToImmutableDictionary();

    private static ImmutableDictionary<int, (int X, int Y)> InputFaceGrid = new Dictionary<int, (int X, int Y)> {
        [1] = (1, 0),
        [2] = (2, 0),
        [3] = (1, 1),
        [4] = (0, 2),
        [5] = (1, 2),
        [6] = (0, 3),
    }.ToImmutableDictionary();

    private static ImmutableDictionary<int, ImmutableDictionary<Direction, (int NewFace, int NumRightRotations, bool transpose, bool ReverseX, bool ReverseY, Direction NewDirection)>> ExampleFaceLinks = new Dictionary<int, ImmutableDictionary<Direction, (int NewFace, int NumRightRotations, bool Transpose, bool InvertX, bool InvertY, Direction NewDirection)>> {
        [1] = new Dictionary<Direction, (int NewFace, int NumRotationsRight, bool Transpose, bool ReverseX, bool ReverseY, Direction NewDirection)> {
            [Direction.Left] = (3, 0, true, false, false, Direction.Down),
            [Direction.Right] = (6, 1, true, false, false, Direction.Left),
            [Direction.Up] = (2, 0, false, true, false, Direction.Down),
            [Direction.Down] = (4, 1, true, false, false, Direction.Down),
        }.ToImmutableDictionary(),
        [2] = new Dictionary<Direction, (int NewFace, int numRightRotations, bool Transpose, bool ReverseX, bool ReverseY, Direction NewDirection)> {
            [Direction.Left] = (6, 2, true, false, false, Direction.Up),
            [Direction.Right] = (3, 3, true, false, false, Direction.Right),
            [Direction.Up] = (1, 3, true, false, false, Direction.Down),
            [Direction.Down] = (5, 3, true, false, false, Direction.Up),
        }.ToImmutableDictionary(),
        [3] = new Dictionary<Direction, (int NewFace, int numRightRotations, bool Transpose, bool ReverseX, bool ReverseY, Direction NewDirection)> {
            [Direction.Left] = (2, 1, true, true, true, Direction.Left),
            [Direction.Right] = (4, 3, true, false, false, Direction.Right),
            [Direction.Up] = (1, 0, true, false, false, Direction.Right),
            [Direction.Down] = (5, 0, true, true, true, Direction.Right),
        }.ToImmutableDictionary(),
        [4] = new Dictionary<Direction, (int NewFace, int numRightRotations, bool Transpose,bool ReverseX, bool ReverseY, Direction NewDirection)> {
            [Direction.Left] = (3, 1, true, true, true, Direction.Left),
            [Direction.Right] = (6, 0, true, true, true, Direction.Down),
            [Direction.Up] = (1, 3, true, true, true, Direction.Up),
            [Direction.Down] = (5, 1, true, false, false, Direction.Down),
        }.ToImmutableDictionary(),
        [5] = new Dictionary<Direction, (int NewFace, int numRightRotations, bool Transpose, bool ReverseX, bool ReverseY, Direction NewDirection)> {
            [Direction.Left] = (3, 2, true, false, false, Direction.Up),
            [Direction.Right] = (6, 3, true, false, false, Direction.Right),
            [Direction.Up] = (4, 3, true, true, true, Direction.Up),
            [Direction.Down] = (2, 3, true, false, false, Direction.Up),
        }.ToImmutableDictionary(),
        [6] = new Dictionary<Direction, (int NewFace, int numRightRotations, bool Transpose, bool ReverseX, bool ReverseY, Direction NewDirection)> {
            [Direction.Left] = (5, 1, true, true, true, Direction.Left),
            [Direction.Right] = (1, 1, true, false, false, Direction.Left),
            [Direction.Up] = (4, 2, true, false, false, Direction.Left),
            [Direction.Down] = (2, 0, true, true, true, Direction.Right),
        }.ToImmutableDictionary(),
    }.ToImmutableDictionary();

    private static ImmutableDictionary<int, ImmutableDictionary<Direction, (int NewFace, int NumRightRotations, bool transpose, bool ReverseX, bool ReverseY, Direction NewDirection)>> InputFaceLinks = new Dictionary<int, ImmutableDictionary<Direction, (int NewFace, int NumRightRotations, bool Transpose, bool InvertX, bool InvertY, Direction NewDirection)>> {
        [1] = new Dictionary<Direction, (int NewFace, int NumRotationsRight, bool Transpose, bool ReverseX, bool ReverseY, Direction NewDirection)> {
            [Direction.Up] = (6, 0, true, false, false, Direction.Right),
            [Direction.Right] = (2, 3, true, false, false, Direction.Right),
            [Direction.Down] = (3, 1, true, false, false, Direction.Down),
            [Direction.Left] = (4, 3, true, true, true, Direction.Right),
        }.ToImmutableDictionary(),
        [2] = new Dictionary<Direction, (int NewFace, int numRightRotations, bool Transpose, bool ReverseX, bool ReverseY, Direction NewDirection)> {
            [Direction.Up] = (6, 3, true, true, true, Direction.Up),
            [Direction.Right] = (5, 1, true, false, false, Direction.Left),
            [Direction.Down] = (3, 2, true, true, true, Direction.Left),
            [Direction.Left] = (1, 0, false, true, false, Direction.Left),
        }.ToImmutableDictionary(),
        [3] = new Dictionary<Direction, (int NewFace, int numRightRotations, bool Transpose, bool ReverseX, bool ReverseY, Direction NewDirection)> {
            [Direction.Up] = (1, 0, false, false, true, Direction.Up),
            [Direction.Right] = (2, 2, true, true, true, Direction.Up),
            [Direction.Down] = (5, 0, false, false, true, Direction.Down),
            [Direction.Left] = (4, 0, true, false, false, Direction.Down),
        }.ToImmutableDictionary(),
        [4] = new Dictionary<Direction, (int NewFace, int numRightRotations, bool Transpose,bool ReverseX, bool ReverseY, Direction NewDirection)> {
            [Direction.Up] = (3, 0, true, false, false, Direction.Right),
            [Direction.Right] = (5, 0, false, true, false, Direction.Right),
            [Direction.Down] = (6, 0, false, false, true, Direction.Down),
            [Direction.Left] = (1, 0, false, false, true, Direction.Right),
        }.ToImmutableDictionary(),
        [5] = new Dictionary<Direction, (int NewFace, int numRightRotations, bool Transpose, bool ReverseX, bool ReverseY, Direction NewDirection)> {
            [Direction.Up] = (3, 0, false, false, true, Direction.Right),
            [Direction.Right] = (2, 1, true, false, false, Direction.Left),
            [Direction.Down] = (6, 2, true, true, true, Direction.Left),
            [Direction.Left] = (4, 0, false, true, false, Direction.Left),
        }.ToImmutableDictionary(),
        [6] = new Dictionary<Direction, (int NewFace, int numRightRotations, bool Transpose, bool ReverseX, bool ReverseY, Direction NewDirection)> {
            [Direction.Up] = (4, 0, false, false, true, Direction.Up),
            [Direction.Right] = (5, 2, true, true, true, Direction.Up),
            [Direction.Down] = (2, 0, false, false, true, Direction.Down),
            [Direction.Left] = (1, 0, true, false, false, Direction.Down),
        }.ToImmutableDictionary(),
    }.ToImmutableDictionary();


    public static ImmutableDictionary<int, (int X, int Y)> FaceGrids(string filename) {
        return IsExample(filename) ? ExampleFaceGrid : InputFaceGrid;
    }

    public static ImmutableDictionary<int, ImmutableDictionary<Direction, (int NewFace, int NumRightRotations, bool transpose, bool ReverseX, bool ReverseY, Direction NewDirection)>> FaceLinks(string filename) {
        return IsExample(filename) ? ExampleFaceLinks : InputFaceLinks;
    }

    private static bool IsExample(string filename) => filename == "example.txt";
    

}