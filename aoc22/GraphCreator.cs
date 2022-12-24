using System.Collections.Immutable;

public static class GraphCreator {

    public static Square CreateCartesian(ImmutableArray<ImmutableArray<char>> map) {
        var squares = CreateSquares(map);

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
                square.Left = FindNextLinkSquare(row, col, Direction.Left, ((row, col, dir) => (row, ((col + colLen - 1) % colLen), dir)), squares);
                square.Right = FindNextLinkSquare(row, col, Direction.Right, ((row, col, dir) => (row, ((col + colLen + 1) % colLen), dir)), squares);
                square.Up = FindNextLinkSquare(row, col, Direction.Up, ((row, col, dir) => ((row + rowLen - 1) % rowLen, col, dir)), squares);
                square.Down = FindNextLinkSquare(row, col, Direction.Down, ((row, col, dir) => ((row + rowLen + 1) % rowLen, col, dir)), squares);
            }
        }

        return startSquare;
    }

    public static Square CreateCube(
        ImmutableArray<ImmutableArray<char>> map,
        ImmutableDictionary<int, (int X, int Y)> faceGrids, 
        ImmutableDictionary<int, ImmutableDictionary<Direction, (int NewFace, int NumRightRotations, bool transpose, bool ReverseX, bool ReverseY, Direction NewDirection)>> faceLinks) {
        
        var squares = CreateSquares(map);
        var edgeLength = map[0].Length / (faceGrids.Values.Select(a => a.X).Max() + 1);

        var startSquare = squares[0].Where(s => s.Terrain == Terrain.Open).First();

        var rowLen = squares.Length;
        var colLen = squares[0].Length;
        for (var row = 0; row < rowLen; row++) {
            for (var col = 0; col < colLen; col++) {
                var square = squares[row][col];

                if (square.Terrain == Terrain.Blank || square.Terrain == Terrain.Wall) {
                    continue;
                }
                var faceNumber = CalculateFaceNumber(row, col, edgeLength, faceGrids);

                square.Left = FindNextLinkCube(row, col, faceNumber, Direction.Left, edgeLength, squares, faceGrids, faceLinks);
                square.Right = FindNextLinkCube(row, col, faceNumber, Direction.Right, edgeLength, squares, faceGrids, faceLinks);
                square.Up = FindNextLinkCube(row, col, faceNumber, Direction.Up, edgeLength, squares, faceGrids, faceLinks);
                square.Down = FindNextLinkCube(row, col, faceNumber, Direction.Down, edgeLength, squares, faceGrids, faceLinks);                
            }
        }

        for (var row = 0; row < rowLen; row++) {
            for (var col = 0; col < colLen; col++) {
                var square = squares[row][col];
                if (square.Terrain != Terrain.Open) 
                    {continue;
                }

                foreach (var direction in Enum.GetValues<Direction>()) {
                    var (newSquare, newDirection) = square.Move(Direction.Right);
                    if (square == newSquare) {
                        continue; // wall
                    }
                    if (newSquare.Move(newDirection.Reverse()).Square != square) {
                        throw new Exception($"Inconsistent forward/reverse. From ({square.X - 1}, {square.Y - 1}) to ({newSquare.X - 1}, {newSquare.Y - 1}) going {direction}");
                    }
                }
            }
        }

        return startSquare;
    }

    private static (Square, Direction) FindNextLinkCube(int row, int col, int face, Direction direction, int edgeLength, Square[][] squares, ImmutableDictionary<int, (int X, int Y)> faceGrids, ImmutableDictionary<int, ImmutableDictionary<Direction, (int NewFace, int NumRightRotations, bool transpose, bool ReverseX, bool ReverseY, Direction NewDirection)>> faceLinks) {
        var currentSquare = squares[row][col];
        var currentDirection = direction;

        var currentFace = face;
        var localX = col - (faceGrids[currentFace].X * edgeLength); 
        var localY = row - (faceGrids[currentFace].Y * edgeLength);

        var oldLocalX = localX;
        var oldLocalY = localY;

        Direction nextDirection = currentDirection;
        var numRightRotations = 0;
        var transpose = false;
        var reverseX = false;
        var reverseY = false;
        var nextFace = currentFace;

//        Console.WriteLine($"Local: ({localX}, {localY})");

        bool offEdge;
        if ((localX == 0 && direction == Direction.Left)
            || (localX == edgeLength - 1 && direction == Direction.Right)
            || (localY == 0 && direction == Direction.Up)
            || (localY == edgeLength - 1 && direction == Direction.Down)) {
            
            offEdge = true;
            // Going off an edge
            (nextFace, numRightRotations, transpose, reverseX, reverseY, nextDirection) = faceLinks[currentFace][currentDirection];
            for (var i = 0; i < numRightRotations; i++) {
                (localX, localY) = RotateRight(localX, localY, edgeLength);
            }
        } else {
            offEdge = false;
            if (nextDirection == Direction.Left) {
                localX = localX - 1;            
            } else if (nextDirection == Direction.Right) {
                localX = localX + 1;
            } else if (nextDirection == Direction.Up) {
                localY = localY - 1;
            } else if (nextDirection == Direction.Down) {
                localY = localY + 1;
            } else {
                throw new Exception("Unexpected");
            }
        }
        //Console.WriteLine($"Local: ({localX}, {localY})");

        int globalX;
        int globalY;

        var xDiff = transpose ? localY : localX;
        var yDiff = transpose ? localX : localY;

        xDiff = reverseX ? edgeLength - xDiff - 1: xDiff;
        yDiff = reverseY ? edgeLength - yDiff - 1: yDiff;

        globalX = (faceGrids[nextFace].X * edgeLength) + xDiff;
        globalY = (faceGrids[nextFace].Y * edgeLength) + yDiff;

//        if (offEdge && currentFace == 6 && currentDirection == Direction.Left) {
//            Console.WriteLine($"Face {currentFace}: Global ({col}, {row}) {currentDirection} -> ({globalX}, {globalY}) {nextDirection}, To Face: {nextFace}");
//        }

//      Console.WriteLine($"({row}, {col}, {direction}) -> ({globalX}, {globalY}, {nextDirection})");
        var nextSquare = squares[globalY][globalX];

        if (nextSquare.Terrain == Terrain.Wall) {
            return (currentSquare, currentDirection);
        } else if (nextSquare.Terrain == Terrain.Open || nextSquare.Terrain == Terrain.Wall) {
            return (nextSquare, nextDirection);
        }

        throw new Exception("Hit empty");
    }

    private static (int X, int Y) RotateRight(int x, int y, int edgeLength) => 
        (edgeLength - 1 - y, x);

    private static int CalculateFaceNumber(int row, int col, int edgeLength, ImmutableDictionary<int, (int X, int Y)> faceGrids) {
        foreach (var faceGrid in faceGrids) {
            if (row >= edgeLength * faceGrid.Value.Y && row < edgeLength * (faceGrid.Value.Y + 1)
                && col >= edgeLength * faceGrid.Value.X && col < edgeLength * (faceGrid.Value.X + 1)) {
                
                return faceGrid.Key;
            }
        }
        throw new Exception($"Unexpected (row: {row}, col: {col})");
    }

    private static Square[][] CreateSquares(ImmutableArray<ImmutableArray<char>> map) {
        var squares = new Square[map.Length][];

        for (var row = 0; row < map.Length; row++) {
            var y = row + 1;
            squares[row] = new Square[map[row].Length];
            for (var col = 0; col < map[row].Length; col++) {
                var x = col + 1;
                squares[row][col] = new Square(x, y, (Terrain) map[row][col]);
            }
        }

        return squares;
    }

    private static (Square Square, Direction Direction) FindNextLinkSquare(int row, int col, Direction direction, Func<int, int, Direction, (int row, int col, Direction Direction)> nextCoordinates, Square[][] squares) {
        var currentSquare = squares[row][col];

        Square? nextSquare = null;

        while (nextSquare == null || nextSquare.Terrain == Terrain.Blank) {
            (row, col, direction) = nextCoordinates(row, col, direction);
            nextSquare = squares[row][col];
        }

        if (nextSquare.Terrain == Terrain.Open) {
            return (nextSquare, direction);
        }

        if (nextSquare.Terrain == Terrain.Wall) {
            return (currentSquare, direction);
        }

        throw new Exception("Unexpected");
    }
}