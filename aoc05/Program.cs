using System.Collections.Immutable;

public class Program {
    public static void Main(string[] args) {
        var (initialState, moves) = Parser.Parse(args[0]);
        var finalState = Rearranger.Rearrange(initialState, moves);

        var topOfStacks = string.Join("", finalState.Keys.Order().Select(k => finalState[k].Peek().ToString()));
        Console.WriteLine($"Top of stacks: {topOfStacks}");
    }
}
