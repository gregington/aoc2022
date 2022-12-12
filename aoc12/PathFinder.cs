using System.Collections.Immutable;

public static class PathFinder {
    public static ImmutableArray<Point> FindPath(Point start, Point end, int maxLength, ImmutableArray<ImmutableArray<char>> grid) {
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

            if (path.Count() >= maxLength) {
                continue;
            }

            visited = visited.Add(current);
            foreach(var nextMove in FindNextMoves(current, grid)) {
                queue.Enqueue(path.Add(nextMove));
            }
        }

        return ImmutableArray<Point>.Empty;
    }

    public static ImmutableArray<Point> FindShortestPathFromLowestPoints(Point end, ImmutableArray<ImmutableArray<char>> grid) {
        var startPoints = FindOrderedStartPoints(end, grid);
        var minLength = int.MaxValue;
        var bestPath = ImmutableArray<Point>.Empty;

        foreach (var start in startPoints) {
            var path = FindPath(start, end, minLength, grid);
            if (path.Count() > 0 && path.Count() < minLength) {
                minLength = path.Count();
                bestPath = path;
            }
        }

        return bestPath;
    }

    private static ImmutableArray<Point> FindOrderedStartPoints(Point end, ImmutableArray<ImmutableArray<char>> grid) {
        return Enumerable.Range(0, grid.Count())
            .SelectMany(y => Enumerable.Range(0, grid[y].Count()).Select(x => new Point(x, y)))
            .Where(p => grid[p.Y][p.X] == 'a')
            .OrderBy(s => ManhattanDistance(s, end))
            .ToImmutableArray();
    }

    private static int ManhattanDistance(Point a, Point b) {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
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