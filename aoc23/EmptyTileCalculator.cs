public static class EmptyTileCalculator {
    public static int Calculate(IEnumerable<Point> elfPositions) {
        var boundingBox = BoundingBoxes.FindBoundingBox(elfPositions);
        var area = (boundingBox.Right - boundingBox.Left + 1) * (boundingBox.Bottom - boundingBox.Top + 1);
        return area - elfPositions.Count();
    }
}