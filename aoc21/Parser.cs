using System.Collections.Immutable;
using System.Text.RegularExpressions;

public static class Parser {
    private static Regex ConstantRegex = new Regex(@"^(?<value>-?\d+)$");
    private static Regex AddRegex = new Regex(@"^(?<left>[a-zA-Z]+) \+ (?<right>[a-zA-Z]+)$");
    private static Regex SubtractRegex = new Regex(@"^(?<left>[a-zA-Z]+) - (?<right>[a-zA-Z]+)$");
    private static Regex MultiplyRegex = new Regex(@"^(?<left>[a-zA-Z]+) \* (?<right>[a-zA-Z]+)$");
    private static Regex DivideRegex = new Regex(@"^(?<left>[a-zA-Z]+) / (?<right>[a-zA-Z]+)$");
    private static Regex EqualRegex = new Regex(@"^(?<left>[a-zA-Z]+) = (?<right>[a-zA-Z]+)$");
    private static Regex VariableRegex = new Regex(@"^var$");
    private static Regex OperationRegex = new Regex(@"^(?<left>[a-zA-Z]+) (\+|-|\*|/|=) (?<right>[a-zA-Z]+)$");

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

        match = EqualRegex.Match(expressionString);
        if (match.Success) {
            return new Equal(
                ParseExpression(match.Groups["left"].Value, expressionStrings), 
                ParseExpression(match.Groups["right"].Value, expressionStrings));
        }

        match = VariableRegex.Match(expressionString);
        if (match.Success) {
            return new Variable(name);
        }

        throw new Exception($"{name}: {expressionString}");
    }

    public static long FindVariableValue(string root, string human, ImmutableDictionary<string, string> expressionStrings) { 
        var newExpressionStrings = ReplaceEquals(root, expressionStrings);
        newExpressionStrings = ReplaceVariable(human, newExpressionStrings);

        var expression = ParseExpression(root, newExpressionStrings);
        expression = Simplify(expression);

        return BinomialSearch(expression, human, 0, 100_000_000_000_000);
    }

    private static long BinomialSearch(IExpression expression, string variableName, long min, long max) {
        var mid = min + ((max - min) / 2);
        var dict = ImmutableDictionary<string, long>.Empty;

        var minValue = expression.Evaluate(dict.SetItem(variableName, min));
        var midValue = expression.Evaluate(dict.SetItem(variableName, mid));
        var maxValue = expression.Evaluate(dict.SetItem(variableName, max));

        if (minValue == 0) {
            return min;
        }
        if (midValue == 0) {
            return mid;
        }
        if (maxValue == 0) {
            return max;
        }

        if (Math.Sign(minValue) != Math.Sign(midValue)) {
            // Value somewhere in min to mid
            return BinomialSearch(expression, variableName, min, mid);
        }

        if (Math.Sign(midValue) != Math.Sign(maxValue)) {
            // Value somewhere in min to mid
            return BinomialSearch(expression, variableName, mid, max);
        }

        throw new Exception($"Unexpected; function probably not monotonic.");
    }

    private static IExpression Simplify(IExpression expression) {
        if (expression is Equal) {
            var simplifiedLeft = Simplify(((Equal) expression).Left);
            var simplifiedRight = Simplify(((Equal) expression).Right);

            return new Equal(simplifiedLeft, simplifiedRight);
        } else if (expression is IOperation) {
            var operation = (IOperation) expression;
            var simplifiedLeft = Simplify(operation.Left);
            var simplifiedRight = Simplify(operation.Right);

            var type = expression.GetType();
            var expr = (IExpression) Activator.CreateInstance(type, new object[] {simplifiedLeft, simplifiedRight})!;

            if (simplifiedLeft is Constant && simplifiedRight is Constant) {
                return new Constant(expr.Evaluate(ImmutableDictionary<string, long>.Empty));
            }
            return expr;
        }
        return expression;
    }
    private static ImmutableDictionary<string, string> ReplaceEquals(string name, ImmutableDictionary<string, string> expressionStrings) {
        var expression = expressionStrings[name];
        var groups = OperationRegex.Match(expression).Groups;
        return expressionStrings.SetItem(name, $"{groups["left"].Value} = {groups["right"].Value}");
    }

    private static ImmutableDictionary<string, string> ReplaceVariable(string name, ImmutableDictionary<string, string> expressionStrings) {
        return expressionStrings.SetItem(name, "var");        
    }

    private static long FindMaxConstant(string root, ImmutableDictionary<string, string> expressionStrings) {
        var expression = ParseExpression(root, expressionStrings);
        return FindConstants(expression).Max();
    }

    private static ImmutableArray<long> FindConstants(IExpression expression) {
        var values = new List<long>();
        FindConstants(expression, values);
        return values.ToImmutableArray();
    }

    private static void FindConstants(IExpression expression, List<long> values) {
        if (expression is Constant) {
            values.Add(((Constant) expression).Value);
        }
        if (expression is IOperation) {
            FindConstants(((IOperation) expression).Left);
            FindConstants(((IOperation) expression).Right);
        }
    }

}