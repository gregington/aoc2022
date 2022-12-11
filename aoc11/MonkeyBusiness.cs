public static class MonkeyBusiness {
    public static int Calculate(IEnumerable<Monkey> monkeys, int topN) {
        return monkeys.Select(m => m.InspectionCount)
            .OrderDescending()
            .Take(topN)
            .Aggregate((x, y) => x * y);
    }
}