using System.Collections.Immutable;
using System.Text.RegularExpressions;

public static class Parser {
    private static Regex MoveRegex = new Regex(@"^move (?<quantity>\d+) from (?<from>\d+) to (?<to>\d+)$");

    public static (ImmutableDictionary<int, ImmutableStack<char>> InitialState, ImmutableArray<(int Quantity, int From, int To)> Moves) Parse(string path) {
        var lines = File.ReadAllLines(path);
        var initialState = ParseInitialState(lines.TakeWhile(line => line.Trim() != "").ToImmutableArray());
        var moves = ParseMoves(lines.SkipWhile(line => line.Trim() != "").Skip(1));
        return (initialState, moves);
    }

    private static ImmutableDictionary<int, ImmutableStack<char>> ParseInitialState(IList<string> input) {
        var reversed = input.Reverse().ToImmutableArray();
        var stackCharPositions = new Dictionary<int, int>();
        var stackNumbersLine = reversed.First();
        
        for (int i = 0; i < stackNumbersLine.Count(); i++) {
            var c = stackNumbersLine[i];
            if (char.IsNumber(c)) {
                stackCharPositions[i] = c - '0';
            }
        }

        var stackDictionary = stackCharPositions.Values.ToImmutableDictionary(x => x, _ => ImmutableStack<char>.Empty);

        foreach (var line in reversed.Skip(1)) {
            for (int i = 0; i < line.Count(); i++) {
                var c = line[i];
                if (char.IsUpper(c)) {
                    var stackNumber = stackCharPositions[i];
                    var newStack = stackDictionary[stackNumber].Push(c);
                    stackDictionary = stackDictionary.SetItem(stackNumber, newStack);
                }
            }
        }
        return stackDictionary;
    }

    public static ImmutableArray<(int Quantity, int From, int To)> ParseMoves(IEnumerable<string> input) {
        return input.Select(line => MoveRegex.Match(line).Groups)
            .Select(g => (Convert.ToInt32(g["quantity"].Value), Convert.ToInt32(g["from"].Value), Convert.ToInt32(g["to"].Value)))
            .ToImmutableArray();
    }
}