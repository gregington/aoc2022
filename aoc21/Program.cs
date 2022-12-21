using System.Collections.Immutable;

public class Program {
    public static void Main(string[] args) {
        var dict = Parser.ParseExpressionStrings(args[0]);

        var expression = Parser.ParseExpression("root", dict);
        Console.WriteLine($"Expression is: {expression}");
        Console.WriteLine($"Expression evaluates to: {expression.Evaluate(ImmutableDictionary<string, long>.Empty)}");

        var humanValue = Parser.FindVariableValue("root", "humn", dict);
        Console.WriteLine($"Value of humn is: {humanValue}");
    }
}