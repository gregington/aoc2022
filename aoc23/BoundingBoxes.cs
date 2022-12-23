public static class BoundingBoxes {
    public static (int Top, int Right, int Bottom, int Left) FindBoundingBox(IEnumerable<Point> points) {
        var top = points.Select(p => p.Y).Min();
        var right = points.Select(p => p.X).Max();
        var bottom = points.Select(p => p.Y).Max();
        var left = points.Select(p => p.X).Min();

        return (top, right, bottom, left);
    }
}