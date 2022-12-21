using System.Collections.Immutable;
using System.Text.RegularExpressions;

public static class Parser {
    private static Regex ConstantRegex = new Regex(@"^(?<value>-?\d+)$");
    private static Regex AddRegex = new Regex(@"^(?<left>[a-zA-Z]+) \+ (?<right>[a-zA-Z]+)$");
    private static Regex SubtractRegex = new Regex(@"^(?<left>[a-zA-Z]+) - (?<right>[a-zA-Z]+)$");
    private static Regex MultiplyRegex = new Regex(@"^(?<left>[a-zA-Z]+) \* (?<right>[a-zA-Z]+)$");
    private static Regex DivideRegex = new Regex(@"^(?<left>[a-zA-Z]+) / (?<right>[a-zA-Z]+)$");

    public static ImmutableDictionary<string, string> ParseExpressionStrings(string path) {
        return File.ReadLines(path)
            .Select(line => line.Split(": "))
            .ToImmutableDictionary(x => x[0], x => x[1]);
    }

    public static IExpression ParseExpression(string name, ImmutableDictionary<string, string> expressionStrings) {
        var expressionString = expressionStrings[name];

        var match = ConstantRegex.Match(expressionString);
        if (match.Success) {
            return new Constant(Int64.Parse(match.Groups["value"].Value));
        }

        match = AddRegex.Match(expressionString);
        if (match.Success) {
            return new Add(
                ParseExpression(match.Groups["left"].Value, expressionStrings), 
                ParseExpression(match.Groups["right"].Value, expressionStrings));
        }

        match = SubtractRegex.Match(expressionString);
        if (match.Success) {
            return new Subtract(
                ParseExpression(match.Groups["left"].Value, expressionStrings), 
                ParseExpression(match.Groups["right"].Value, expressionStrings));
        }

        match = MultiplyRegex.Match(expressionString);
        if (match.Success) {
            return new Multiply(
                ParseExpression(match.Groups["left"].Value, expressionStrings), 
                ParseExpression(match.Groups["right"].Value, expressionStrings));
        }

        match = DivideRegex.Match(expressionString);
        if (match.Success) {
            return new Divide(
                ParseExpression(match.Groups["left"].Value, expressionStrings), 
                ParseExpression(match.Groups["right"].Value, expressionStrings));
        }

        throw new Exception($"{name}: {expressionString}");
    }
}