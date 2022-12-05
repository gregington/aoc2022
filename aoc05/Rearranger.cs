using System.Collections.Immutable;

public class Rearranger {
    public static ImmutableDictionary<int, ImmutableStack<char>> Rearrange(ImmutableDictionary<int, ImmutableStack<char>> stacks, IEnumerable<(int Quantity, int From, int To)> moves) {
        return moves.Expand()
            .Aggregate(stacks, Move);
    }

    private static ImmutableDictionary<int, ImmutableStack<char>> Move(ImmutableDictionary<int, ImmutableStack<char>> stacks, (int From, int To) move) {
        var fromStack = stacks[move.From];
        fromStack = fromStack.Pop(out var crate);

        var toStack = stacks[move.To];
        toStack = toStack.Push(crate);

        return stacks
            .SetItem(move.From, fromStack)
            .SetItem(move.To, toStack);
    }
}