using System.Collections.Immutable;

public static class Round {
    public static ImmutableArray<Monkey> Execute(ImmutableArray<Monkey> monkeys) {
        return monkeys.Aggregate(monkeys, (monkeys, monkey) => Turn.Execute(monkeys, monkey.Id));
    }
}