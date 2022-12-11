using System.Collections.Immutable;
using System.Text.RegularExpressions;

public static class MonkeyParser {
    private static Regex MonkeyRegex = new Regex(@"^\s*Monkey (?<monkeyId>\d+):$");

    private static Regex ItemsRegex = new Regex(@"^ *Starting items: (?<items>(\d+(, )?)*)$");

    private static Regex OperationRegex = new Regex(@"^\s*Operation: new = (?<expr1>[\w|\d]+) (?<operation>[\*\+]) (?<expr2>[\w|\d]+)$");

    private static Regex TestRegex = new Regex(@"^ *Test: divisible by (?<value>\d+)$");

    private static Regex MonkeyThrowRegex = new Regex(@"^ *If (?<condition>(true|false)): throw to monkey (?<monkey>\d+)$");

    public static Monkey Parse(IEnumerable<string> lines) {
        int? id = null;
        ImmutableArray<long>? items = null;
        IElement? operation = null;
        int? divisor = null;
        int? trueMonkey = null;
        int? falseMonkey = null;

        foreach (var line in lines) {
            var match = MonkeyRegex.Match(line);
            if (match.Success) {
                id = Convert.ToInt32(match.Groups["monkeyId"].Value);
                continue;
            }

            match = ItemsRegex.Match(line);
            if (match.Success) {
                items = ParseItems(match.Groups["items"].Value);
                continue;
            }

            match = OperationRegex.Match(line);
            if (match.Success) {
                var groups = match.Groups;
                operation = ParseOperation(groups["operation"].Value, groups["expr1"].Value, groups["expr2"].Value);
                continue;
            }

            match = TestRegex.Match(line);
            if (match.Success) {
                divisor = Convert.ToInt32(match.Groups["value"].Value);
                continue;
            }

            match = MonkeyThrowRegex.Match(line);
            if (match.Success) {
                var groups = match.Groups;
                var condition = groups["condition"].Value;
                var monkey = Convert.ToInt32(groups["monkey"].Value);

                if (condition == "true") {
                    trueMonkey = monkey;
                    continue;
                }
                if (condition == "false") {
                    falseMonkey = monkey;
                    continue;
                }
                throw new ArgumentException(condition);
            }
        }

        if (id == null) {
            throw new ArgumentNullException("id");
        }
        if (items == null) {
            throw new ArgumentException("items");
        }
        if (operation == null) {
            throw new ArgumentNullException("operation");
        }
        if (divisor == null) {
            throw new ArgumentNullException("divisor");
        }
        if (trueMonkey == null) {
            throw new ArgumentNullException("trueMonkey");
        }
        if (falseMonkey == null) {
            throw new ArgumentNullException("falseMonkey");
        }

        var throwExpression = new If(
            new Equals(
                new Mod(
                    new Variable("worryValue"), 
                    new Constant(divisor.Value)
                ),
                new Constant(0)
            ), 
            new Constant(trueMonkey.Value), 
            new Constant(falseMonkey.Value));

        return new Monkey(id.Value, items.Value, operation, throwExpression);
    }

    private static ImmutableArray<long> ParseItems(string rawItems) {
        return rawItems.Split(", ")
            .Select(x => Convert.ToInt64(x))
            .ToImmutableArray();
    }

    private static IElement ParseOperation(string operation, string expr1, string expr2) {
        if (operation == "+") {
            return new Add(ParseExpression(expr1), ParseExpression(expr2));
        }

        if (operation == "*") {
            return new Multiply(ParseExpression(expr1), ParseExpression(expr2));
        }

        throw new ArgumentException(operation);
    }

    private static IElement ParseExpression(string expression) {
        if (int.TryParse(expression, out var number)) {
            return new Constant(number);
        }

        return new Variable(expression);
    }
}