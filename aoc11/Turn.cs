using System.Collections.Immutable;

public static class Turn {
    public static ImmutableArray<Monkey> Execute(ImmutableArray<Monkey> initialState, int monkeyId, Func<long, long> worryFunction) {
        var monkeys = initialState;
        var monkey = monkeys[monkeyId];

        foreach (var item in monkey.Items) {
            var variables = new Dictionary<string, long>{ ["old"] = item}.ToImmutableDictionary();
            var newWorryLevel = monkey.WorryOperation.Evaluate(variables);

            newWorryLevel = worryFunction.Invoke(newWorryLevel);

/*
            if (lowWorry) {
                newWorryLevel /= 3;
            } else {
                newWorryLevel = newWorryLevel % divisor;
            }
*/
            var nextMonkeyId = newWorryLevel % monkey.Divisor == 0 ? monkey.nextMonkeyIfDivisible : monkey.nextMonkeyIfNotDivisible;

            var nextMonkey = monkeys[nextMonkeyId];
            nextMonkey = nextMonkey with {Items = nextMonkey.Items.Add(newWorryLevel)};
            monkeys = monkeys.SetItem(nextMonkeyId, nextMonkey);
        }

        monkey = monkey with {
            Items = ImmutableArray<long>.Empty,
            InspectionCount = monkey.InspectionCount + monkey.Items.Count()
        };

        return monkeys.SetItem(monkeyId, monkey);
    }
}