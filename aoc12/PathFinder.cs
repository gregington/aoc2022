using System.Collections.Immutable;

public static class PathFinder {
    public static ImmutableArray<Point> FindPath(Point start, Point end, ImmutableArray<ImmutableArray<char>> grid) {
        var queue = new Queue<ImmutableArray<Point>>();
        queue.Enqueue(ImmutableArray.Create<Point>(start));
        var visited = ImmutableHashSet<Point>.Empty;

        while (queue.Count > 0) {
            var path = queue.Dequeue();
            var current = path.Last();
            
            if (current == end) {
                return path;
            }

            if (visited.Contains(current)) {
                continue;
            }

            visited = visited.Add(current);
            foreach(var nextMove in FindNextMoves(current, grid)) {
                queue.Enqueue(path.Add(nextMove));
            }
        }

        return ImmutableArray<Point>.Empty;
    }

    private static ImmutableArray<Point> FindNextMoves(Point current, ImmutableArray<ImmutableArray<char>> grid) {
        var xLength = grid[0].Count();
        var yLength = grid.Count();
        var candidateMoves = FindCandidateMoves(current, xLength, yLength);
        var maxHeight = (char) (grid[current.Y][current.X] + 1);
        return candidateMoves
            .Where(c => grid[c.Y][c.X] <= maxHeight)
            .ToImmutableArray();
    }

    private static ImmutableArray<Point> FindCandidateMoves(Point current, int xLength, int yLength) {
        var cardinalNewPoints = new [] {
            new Point(current.X - 1, current.Y),
            new Point(current.X + 1, current.Y),
            new Point(current.X, current.Y - 1),
            new Point(current.X, current.Y + 1)
        };

        return cardinalNewPoints
            .Where(p => p.X >= 0)
            .Where(p => p.X < xLength)
            .Where(p => p.Y >= 0)
            .Where(p => p.Y < yLength)
            .ToImmutableArray();
    }
}