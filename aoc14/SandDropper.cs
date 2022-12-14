using System.Collections.Immutable;

public static class SandDropper {
    
    public static ImmutableHashSet<Point> Drop(ImmutableHashSet<Point> rocks, Point source) {
        int yLimit = rocks.Select(r => r.Y).Max();
        var sand = ImmutableHashSet<Point>.Empty;
        return Drop(rocks, sand, yLimit, source);
    }

    public static ImmutableHashSet<Point> Drop(ImmutableHashSet<Point> rocks, ImmutableHashSet<Point> sand, int yLimit, Point source) {
        var position = (Point?) source;

        position = DropSingleGrain(rocks, sand, yLimit, source);
        if (!position.HasValue) {
            return sand;
        }

        return Drop(rocks, sand.Add(position.Value), yLimit, source);
    }

    public static Point? DropSingleGrain(ImmutableHashSet<Point> rocks, ImmutableHashSet<Point> sand, int yLimit, Point position) {
        if (position.Y > yLimit) {
            return null;
        }

        var nextPosition = DropCandidates(position)
            .Select(candidate => (Point?) candidate)
            .FirstOrDefault(candidate => !(rocks.Contains(candidate!.Value) || sand.Contains(candidate!.Value)), (Point?) null);

        if (!nextPosition.HasValue) {
            return position;
        }

        return DropSingleGrain(rocks, sand, yLimit, nextPosition.Value);
    }

    private static IEnumerable<Point> DropCandidates(Point position) {
        yield return new Point(position.X, position.Y + 1);
        yield return new Point(position.X - 1, position.Y + 1);
        yield return new Point(position.X + 1, position.Y + 1);
    } 
}