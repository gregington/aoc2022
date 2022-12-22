using System.Collections.Immutable;

public static class GraphCreator {
    public static Square Create(ImmutableArray<ImmutableArray<char>> map) {
        var squares = new Square[map.Length][];

        for (var row = 0; row < map.Length; row++) {
            var y = row + 1;
            squares[row] = new Square[map[row].Length];
            for (var col = 0; col < map[row].Length; col++) {
                var x = col + 1;
                squares[row][col] = new Square(x, y, (Terrain) map[row][col]);
            }
        }

        var startSquare = squares[0].Where(s => s.Terrain == Terrain.Open).First();

        // Create links, go in each of the directions. Right first
        var rowLen = squares.Length;
        var colLen = squares[0].Length;
        for (var row = 0; row < rowLen; row++) {
            for (var col = 0; col < colLen; col++) {
                var square = squares[row][col];
                if (square.Terrain == Terrain.Blank || square.Terrain == Terrain.Wall) {
                    continue;
                }
                square.Left = FindNextLink(row, col, ((row, col) => (row, ((col + colLen - 1) % colLen))), squares);
                square.Right = FindNextLink(row, col, ((row, col) => (row, ((col + colLen + 1) % colLen))), squares);
                square.Up = FindNextLink(row, col, ((row, col) => ((row + rowLen - 1) % rowLen, col)), squares);
                square.Down = FindNextLink(row, col, ((row, col) => ((row + rowLen + 1) % rowLen, col)), squares);
            }
        }

        return startSquare;
    }

    private static Square FindNextLink(int row, int col, Func<int, int, (int row, int col)> nextCoordinates, Square[][] squares) {
        var currentSquare = squares[row][col];

        Square? nextSquare = null;

        while (nextSquare == null || nextSquare.Terrain == Terrain.Blank) {
            (row, col) = nextCoordinates(row, col);
            nextSquare = squares[row][col];
        }

        if (nextSquare.Terrain == Terrain.Open) {
            return nextSquare;
        }

        if (nextSquare.Terrain == Terrain.Wall) {
            return currentSquare;
        }

        throw new Exception("Unexpected");
    }
}