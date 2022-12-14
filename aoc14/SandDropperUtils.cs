public static class SandDropperUtils {
    public static IEnumerable<Point> DropCandidates(Point position) {
        yield return new Point(position.X, position.Y + 1);
        yield return new Point(position.X - 1, position.Y + 1);
        yield return new Point(position.X + 1, position.Y + 1);
    } 

}