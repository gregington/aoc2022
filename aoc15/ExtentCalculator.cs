public static class ExtentCalculator {
    public static Extent Calculate(Point p, int distance) {
        return new Extent(
            new Point(p.X, p.Y - distance),
            new Point(p.X + distance, p.Y),
            new Point(p.X, p.Y + distance),
            new Point(p.X - distance, p.Y)
        );
    }
}