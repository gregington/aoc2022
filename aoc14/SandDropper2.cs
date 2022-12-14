using System.Collections.Immutable;

public static class SandDropper2 {
    public static ImmutableHashSet<Point> Drop(ImmutableHashSet<Point> rocks, Point source) {
        int floor = rocks.Select(r => r.Y).Max() + 2;
        var sand = ImmutableHashSet<Point>.Empty;
        return Drop(rocks, sand, floor, source);
    }

    private static ImmutableHashSet<Point> Drop(ImmutableHashSet<Point> rocks, ImmutableHashSet<Point> sand, int floor, Point source) {
        var position = (Point?) source;

        position = DropSingleGrain(rocks, sand, floor, source);
        sand = sand.Add(position.Value);
        if (position == source) {
            return sand;
        }

        return Drop(rocks, sand, floor, source);
    }
    private static Point DropSingleGrain(ImmutableHashSet<Point> rocks, ImmutableHashSet<Point> sand, int floor, Point position) {
        var nextPosition = SandDropperUtils.DropCandidates(position)
            .Select(candidate => (Point?) candidate)
            .FirstOrDefault(candidate => !(rocks.Contains(candidate!.Value) || sand.Contains(candidate!.Value) || candidate!.Value.Y >= floor), (Point?) null);

        if (!nextPosition.HasValue) {
            return position;
        }

        return DropSingleGrain(rocks, sand, floor, nextPosition.Value);
    }

}