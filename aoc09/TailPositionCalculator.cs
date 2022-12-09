using System.Collections.Immutable;

public static class TailPositionCalculator {

    public static ImmutableHashSet<(int X, int Y)> Calculate(IEnumerable<(int XIncrement, int YIncrement)> moves) {
        var initialState = (Visited: new HashSet<(int X, int Y)>{(0, 0)}.ToImmutableHashSet(), Tail: (X: 0, Y: 0), Head: (X: 0, Y: 0 ));

        var endState = moves.Aggregate(initialState, (state, move) => Move(move, state));
        return endState.Visited;
    }

    private static (ImmutableHashSet<(int X, int Y)> Visited, (int X, int Y) Head, (int X, int Y) Tail) Move(
        (int XIncrement, int YIncrement) move, (ImmutableHashSet<(int X, int Y)> Visited, (int X, int Y) Head, (int X, int Y) Tail) state) {

        var (visited, head, tail) = state;
        var newHead = (X: head.X + move.XIncrement, Y: head.Y + move.YIncrement);
        var newTail = CalculateTailPosition(newHead, tail);
        return (visited.Add(newTail), newHead, newTail);
    }
    
    public static (int X, int Y) CalculateTailPosition((int X, int Y) head, (int X, int Y) tail) {
        if (IsTouching(head, tail)) {
            return tail;
        }

        // Check for two away in cardinal directions
        // Tail south 2, move north 1
        if (tail.X - head.X == 0 && tail.Y - head.Y == -2) {
            return (tail.X, tail.Y + 1);
        }
        // Tail west 2, move east 1
        if (tail.X - head.X == -2 && tail.Y - head.Y == 0) {
            return (tail.X + 1, tail.Y);
        }
        // Tail north 2, move south 1
        if (tail.X - head.X == 0 && tail.Y - head.Y == 2) {
            return (tail.X, tail.Y - 1);
        }
        // Tail east 2, move west 1
        if (tail.X - head.X == 2 && tail.Y - head.Y == 0) {
            return (tail.X - 1, tail.Y);
        }

        // Otherwise try four diagonals to see which one touches
        if (IsTouching(head, (tail.X - 1, tail.Y - 1))) {
            return (tail.X - 1, tail.Y - 1);
        }
        if (IsTouching(head, (tail.X - 1, tail.Y + 1))) {
            return (tail.X - 1, tail.Y + 1);
        }
        if (IsTouching(head, (tail.X + 1, tail.Y + 1))) {
            return (tail.X + 1, tail.Y + 1);
        }
        if (IsTouching(head, (tail.X + 1, tail.Y - 1))) {
            return (tail.X + 1, tail.Y - 1);
        }

        throw new InvalidOperationException();
    }

    public static bool IsTouching((int X, int Y) head, (int X, int Y) tail) {
        var xDiff = Math.Abs(head.X - tail.X);
        var yDiff = Math.Abs(head.Y - tail.Y);

        return (xDiff == 0 || xDiff == 1) && (yDiff == 0 || yDiff == 1);
    }
}