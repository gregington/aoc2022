using System.Collections.Immutable;

public record struct Extent(Point top, Point right, Point bottom, Point left) {
    public Range? IntersectRow(int y) {
        if (y < top.Y || y > bottom.Y) {
            return null;
        }

        if (y == left.Y) {
            // On mid point
            return new Range(left.X, right.X);
        }

        var verticalDistanceFromCentre = Math.Abs(y - left.Y);
        var minX = left.X + verticalDistanceFromCentre;
        var maxX = right.X - verticalDistanceFromCentre;

        return new Range(minX, maxX);
    }
}