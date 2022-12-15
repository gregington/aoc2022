public record struct Range(int Min, int Max) {

    public int Length => this.Max - this.Min + 1;

    public bool Includes(int a) => a >= this.Min && a <= this.Max;

    public bool Intersects(Range other) {
        var (lowestMin, highestMin) = FindLowestAndHighestMin(this, other);

        return highestMin.Min <= lowestMin.Max;
    }

    public Range Union(Range other) {
        var (lowestMin, highestMin) = FindLowestAndHighestMin(this, other);

        return new Range(lowestMin.Min, Math.Max(lowestMin.Max, highestMin.Max));
    }

    private (Range lowestMin, Range highestMin) FindLowestAndHighestMin(Range a, Range b) {
        if (a.Min < b.Min) {
            return (a, b);
        }
        return (b, a);
    }
}