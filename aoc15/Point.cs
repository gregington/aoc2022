public record struct Point(int X, int Y) {
    public int ManhattanDistance(Point other) {
        return Math.Abs(this.X - other.X) + Math.Abs(this.Y - other.Y);
    }
}