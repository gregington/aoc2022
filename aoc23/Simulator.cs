using System.Collections.Immutable;

public static class Simulator {
    public static ImmutableHashSet<Point> Simulate(ImmutableHashSet<Point> initialElfPositions, int numRounds) {
        var positions = initialElfPositions;
        var proposalFunctionsEnumerable = Proposals.ProposalFunctionsEnumerable();
        using (var enumerator = proposalFunctionsEnumerable.GetEnumerator()) {
            for (int i = 0; i < numRounds; i++) {
                if (!enumerator.MoveNext()) {
                    throw new Exception("Enumerator should never end");
                }
                positions = Round(positions, enumerator.Current);
            }
            return positions;
        }
    }

    public static int SimulateUntilNoMovement(ImmutableHashSet<Point> initialElfPositions) {
        var positions = initialElfPositions;
        var proposalFunctionsEnumerable = Proposals.ProposalFunctionsEnumerable();
        
        using (var enumerator = proposalFunctionsEnumerable.GetEnumerator()) {
            var round = 0;
            while(true) {
                if (!enumerator.MoveNext()) {
                    throw new Exception("Enumerator should never end");
                }
                var newPositions = Round(positions, enumerator.Current);
                round = round + 1;
                if (newPositions.SetEquals(positions)) {
                    return round;
                }
                positions = newPositions;
            }
        }        
    }

    public static ImmutableHashSet<Point> Round(ImmutableHashSet<Point> elfPositions, ImmutableArray<Func<Point, ISet<Point>, Point?>> proposalFunctions) {
        // First half, propose
        var proposals = elfPositions.ToImmutableDictionary(
            p => p,
            p => Propose(p, elfPositions, proposalFunctions));

        // Second half, move.
        var uniqueProposals = proposals.Values.GroupBy(p => p)
            .Where(x => x.Count() == 1)
            .Select(x => x.Key)
            .ToHashSet();

        var positions = proposals
            .Select(p => uniqueProposals.Contains(p.Value) ? p.Value : p.Key)
            .ToImmutableHashSet();

        if (proposals.Count() != positions.Count()) {
            throw new Exception("Unexpected");
        }
        return positions;
    }

    private static Point Propose(Point elf, ImmutableHashSet<Point> allElfPositions, ImmutableArray<Func<Point, ISet<Point>, Point?>> proposalFunctions) {
        if (elf.Adjacent().All(a => !allElfPositions.Contains(a))) {
            return elf;
        }

        foreach (var proposalFunction in proposalFunctions) {
            var proposal = proposalFunction(elf, allElfPositions);
            if (proposal.HasValue) {
                return proposal.Value;
            }
        }

        return elf;

/*
        return proposalFunctions
            .Select(f => f(elf, allElfPositions))
            .FirstOrDefault(r => r != null, elf)!.Value;
*/
    }
}