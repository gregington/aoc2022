using System.Collections.Immutable;
using System.Text;

public record Valley(int XLength, int YLength, Point Start, Point Goal, ImmutableDictionary<Point, ImmutableArray<Blizzard>> Blizzards, ImmutableHashSet<Point> Walls) {

    public override string ToString() {
        return ToString(null);
    }

    public Valley IncrementTime() {
        var nextBlizzards = Blizzards.SelectMany(b => b.Value)
            .Select(b => b.Move());

        var blizzardsOnBoard = nextBlizzards.GroupBy(b => !Walls.Contains(b.Location));
        var resetBlizzards = blizzardsOnBoard.Where(x => x.Key == false)
            .SelectMany(b => b)
            .Select(b => ResetBlizzard(b));

        var combinedBlizzards = blizzardsOnBoard.Where(x => x.Key == true)
            .SelectMany(b => b)
            .Concat(resetBlizzards)
            .GroupBy(b => b.Location)
            .ToImmutableDictionary(x => x.Key, x => x.ToImmutableArray());

        return this with { Blizzards = combinedBlizzards };
    }

    private Blizzard ResetBlizzard(Blizzard blizzard) {
        return blizzard.Direction switch {
            Direction.North => blizzard with { Location = blizzard.Location with { Y = YLength - 2 } },
            Direction.East => blizzard with { Location = blizzard.Location with { X = 1 } },
            Direction.South => blizzard with { Location = blizzard.Location with { Y = 1 } },
            Direction.West => blizzard with { Location = blizzard.Location with { X = XLength - 2} },
            _ => throw new Exception($"Unknown direction {blizzard.Direction}")
        };
    }

    public string ToString(Point? expedition) {
        var sb = new StringBuilder();

        for (var y = 0; y < YLength; y++) {
            for (var x = 0; x < XLength; x++) {
                var point = new Point(x, y);
                if (Walls.Contains(point)) {
                    sb.Append('#');
                    continue;
                }
                if (Blizzards.ContainsKey(point)) {
                    var blizzardsAtPoint = Blizzards[point];
                    var blizzardsCount = blizzardsAtPoint.Count();
                    if (blizzardsCount > 1) {
                        sb.Append(blizzardsCount);
                        continue;
                    }
                    sb.Append((char) blizzardsAtPoint.First().Direction);
                    continue;
                }
                if (expedition.HasValue && expedition.Value == point) {
                    sb.Append('E');
                    continue;
                }
                if (point == Start) {
                    sb.Append('S');
                    continue;
                }
                if (point == Goal) {
                    sb.Append('G');
                    continue;
                }
                sb.Append('.');
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
}