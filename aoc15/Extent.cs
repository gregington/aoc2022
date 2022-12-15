using System.Collections.Immutable;

public record struct Extent(Point top, Point right, Point bottom, Point left) {
    public ImmutableHashSet<Point> IntersectRow(int y) {
        if (y < top.Y || y > bottom.Y) {
            return ImmutableHashSet<Point>.Empty;
        }

        if (y == left.Y) {
            // On mid point
            return Enumerable.Range(left.X, right.X - left.X + 1)
                .Select(x => new Point(x, y))
                .ToImmutableHashSet();
        }

        var verticalDistanceFromCentre = Math.Abs(y - left.Y);
        var minX = left.X + verticalDistanceFromCentre;
        var maxX = right.X - verticalDistanceFromCentre;

        return Enumerable.Range(minX, maxX - minX + 1)
            .Select(x => new Point(x, y))
            .ToImmutableHashSet();
    }
}