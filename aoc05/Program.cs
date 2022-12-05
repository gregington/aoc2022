using System.Collections.Immutable;

public class Program {
    public static void Main(string[] args) {
        var (initialState, moves) = Parser.Parse(args[0]);
        var finalStateCrateMover9000 = Rearranger.RearrangeCrateMover9000(initialState, moves);
        var topOfStacksCrateMover9000 = string.Join("", finalStateCrateMover9000.Keys.Order().Select(k => finalStateCrateMover9000[k].Peek().ToString()));
        Console.WriteLine($"Top of stacks, CrateMaster 9000: {topOfStacksCrateMover9000}");

        var finalStateCrateMover9001 = Rearranger.RearrangeCrateMover9001(initialState, moves);
        var topOfStacksCrateMover9001 = string.Join("", finalStateCrateMover9001.Keys.Order().Select(k => finalStateCrateMover9001[k].Peek().ToString()));
        Console.WriteLine($"Top of stacks, CrateMaster 9001: {topOfStacksCrateMover9001}");
    }
}
