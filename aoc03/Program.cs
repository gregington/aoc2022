public static class Program {
    public static void Main(string[] args) {
        var rucksackContents = File.ReadLines(args[0])
            .Select(x => RucksackParser.CompartmentContents(x));

        var commonContents = rucksackContents
            .Select(x => CommonItemFinder.Find(x[0], x[1]));

        var priorities = commonContents
            .Select(x => x.Select(y => Prioritiser.Priority(y)).Sum());

        var allPrioritiesSum = priorities.Sum();

        Console.WriteLine($"Sum of common item priorities: {allPrioritiesSum}");
    }
}
