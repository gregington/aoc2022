public class Program {
    public static void Main(string[] args) {
        var dict = Parser.ParseExpressionStrings(args[0]);

        var expression = Parser.ParseExpression("root", dict);
        Console.WriteLine($"Expression id: {expression}");
        Console.WriteLine($"Expression evaluates to: {expression.Evaluate()}");
    }
}