public record struct Point (int X, int Y) {

    public Point PointN() => new Point(X, Y - 1);

    public Point PointE() => new Point(X + 1, Y);

    public Point PointS() => new Point(X, Y + 1);

    public Point PointW() => new Point(X - 1, Y);

    public Point PointNE() => new Point(X + 1, Y - 1);

    public Point PointNW() => new Point(X - 1, Y - 1);

    public Point PointSE() => new Point(X + 1, Y + 1);

    public Point PointSW() => new Point(X - 1, Y + 1);

    public Point Adjacent(string direction) =>
        direction switch {
            "N" => PointN(),
            "E" => PointE(),
            "S" => PointS(),
            "W" => PointW(),
            "NE" => PointNE(),
            "NW" => PointNW(),
            "SE" => PointSE(),
            "SW" => PointSW(),
            _ => throw new Exception($"Unexpected direction {direction}")
        };
    
    public IEnumerable<Point> Adjacent(IEnumerable<string> directions) =>
        directions.Select(Adjacent);

    public IEnumerable<Point> Adjacent() {
        yield return PointN();
        yield return PointE();
        yield return PointS();
        yield return PointW();
        yield return PointNE();
        yield return PointNW();
        yield return PointSE();
        yield return PointSW();
    }

    public override string ToString() => $"({X}, {Y})";
}