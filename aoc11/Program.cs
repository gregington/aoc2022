public class Program {
    public static void Main(string[] args) {
        var monkeys = Parser.ParseMonkeys(args[0]);

        var finalStateMonkeys = Enumerable.Range(0, 20)
            .Aggregate(monkeys, (m, _) => Round.Execute(m));

        foreach (var monkey in finalStateMonkeys) {
            Console.WriteLine(monkey);
        }

        var monkeyBusiness = MonkeyBusiness.Calculate(finalStateMonkeys, 2);

        Console.WriteLine($"Monkey busineness after 20 rounds: {monkeyBusiness}");
    }
}
