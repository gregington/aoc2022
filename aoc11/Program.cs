public class Program {
    public static void Main(string[] args) {
        var monkeys = Parser.ParseMonkeys(args[0]);
        var finalStateMonkeys20 = Enumerable.Range(0, 20)
            .Aggregate(monkeys, (m, _) => Round.Execute(m, x => x / 3));

        var monkeyBusiness20 = MonkeyBusiness.Calculate(finalStateMonkeys20, 2);
        Console.WriteLine($"Monkey busineness after 20 rounds: {monkeyBusiness20}");

        var modulo = monkeys.Select(m => m.Divisor)
            .Aggregate(1L, (a, b) => a * b);

        var finalStateMonkeys10000 = Enumerable.Range(0, 10000)
            .Aggregate(monkeys, (m, _) => Round.Execute(m, x => x % modulo));

        var monkeyBusiness10000 = MonkeyBusiness.Calculate(finalStateMonkeys10000, 2);
        Console.WriteLine($"Monkey busineness after 10000 rounds: {monkeyBusiness10000}");
    }
}
