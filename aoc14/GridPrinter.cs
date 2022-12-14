using System.Text;

public static class GridPrinter {
    public static string Print(IEnumerable<Point> rocks, IEnumerable<Point> sand, Point source) {
        var occupiedPoints = rocks.Concat(sand);
        var minX = Math.Min(occupiedPoints.Select(a => a.X).Min(), source.X);
        var maxX = Math.Max(occupiedPoints.Select(a => a.X).Max(), source.X);
        var minY = Math.Min(occupiedPoints.Select(a => a.Y).Min(), source.Y);
        var maxY = Math.Max(occupiedPoints.Select(a => a.Y).Max(), source.Y);

        var sb = new StringBuilder();
        for (var y = minY; y <= maxY; y++) {
            for (var x = minX; x <= maxX; x++) {
                var point = new Point(x, y);
                if (rocks.Contains(point)) {
                    sb.Append("#");
                } else if (sand.Contains(point)) {
                    sb.Append("o");
                } else if (point == source) {
                    sb.Append("+");
                } else {
                    sb.Append(".");
                }
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }
}