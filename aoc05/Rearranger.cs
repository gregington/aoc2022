using System.Collections.Immutable;

public class Rearranger {
    public static ImmutableDictionary<int, ImmutableStack<char>> RearrangeCrateMover9000(ImmutableDictionary<int, ImmutableStack<char>> stacks, IEnumerable<(int Quantity, int From, int To)> moves) {
        return moves.Expand()
            .Aggregate(stacks, MoveCrateMover9000);
    }

    public static ImmutableDictionary<int, ImmutableStack<char>> RearrangeCrateMover9001(ImmutableDictionary<int, ImmutableStack<char>> stacks, IEnumerable<(int Quantity, int From, int To)> moves) {
        return moves.Aggregate(stacks, MoveCrateMover9001);
    }

    private static ImmutableDictionary<int, ImmutableStack<char>> MoveCrateMover9000(ImmutableDictionary<int, ImmutableStack<char>> stacks, (int From, int To) move) {
        var fromStack = stacks[move.From];
        fromStack = fromStack.Pop(out var crate);

        var toStack = stacks[move.To];
        toStack = toStack.Push(crate);

        return stacks
            .SetItem(move.From, fromStack)
            .SetItem(move.To, toStack);
    }

    private static ImmutableDictionary<int, ImmutableStack<char>> MoveCrateMover9001(ImmutableDictionary<int, ImmutableStack<char>> stacks, (int Quantity, int From, int To) move) {
        var fromStack = stacks[move.From];

        var tempStack = new Stack<char>();
        for (var i = 0; i < move.Quantity; i++) {
            fromStack = fromStack.Pop(out var crate);
            tempStack.Push(crate);    
        }

        var toStack = stacks[move.To];
        while (tempStack.Count > 0) {
            var crate = tempStack.Pop();
            toStack = toStack.Push(crate);
        }

        return stacks
            .SetItem(move.From, fromStack)
            .SetItem(move.To, toStack);
    }
}