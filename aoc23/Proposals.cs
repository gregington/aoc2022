using System.Collections.Immutable;

public static class Proposals {
    
    public static IEnumerable<ImmutableArray<Func<Point, ISet<Point>, Point?>>> ProposalFunctionsEnumerable() {
        var funcs = ProposalFunctions;
        var offset = 0;
        
        while (true) {
            var builder = ImmutableArray.CreateBuilder<Func<Point, ISet<Point>, Point?>>();
            for (int i = 0; i < funcs.Length; i++) {
                builder.Add(funcs[(offset + i) % funcs.Length]);
            }
            yield return builder.ToImmutableArray();
            offset = (offset + 1) % funcs.Length;
        }
    }

    public static ImmutableArray<Func<Point, ISet<Point>, Point?>> ProposalFunctions =
        new Func<Point, ISet<Point>, Point?>[] {
            (p, a) => Propose(p, a, new [] { "N", "NE", "NW" }, "N"),
            (p, a) => Propose(p, a, new [] { "S", "SE", "SW" }, "S"),
            (p, a) => Propose(p, a, new [] { "W", "NW", "SW" }, "W"),
            (p, a) => Propose(p, a, new [] { "E", "NE", "SE" }, "E"),
        }.ToImmutableArray();

    private static Point? Propose(Point elfPosition, ISet<Point> allElfPositions, IEnumerable<string> directionsToCheck, string directionProposal) {
        var positionsToCheck = elfPosition.Adjacent(directionsToCheck).ToArray();
        if (positionsToCheck.All(p => !allElfPositions.Contains(p))) {
            return elfPosition.Adjacent(directionProposal);
        }
        return null;
    }
}