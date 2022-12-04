using System.Collections.Immutable;

public static class Program {

    private static ImmutableHashSet<char> AllContents = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().ToImmutableHashSet();

    public static void Main(string[] args) {
        var rucksackContents = File.ReadLines(args[0])
            .Select(x => RucksackParser.CompartmentContents(x));
        var commonContents = rucksackContents
            .Select(x => CommonItemFinder.Find(x[0], x[1]));
        var priorities = commonContents
            .Select(x => x.Select(y => Prioritiser.Priority(y)).Sum());
        var allPrioritiesSum = priorities.Sum();

        Console.WriteLine($"Sum of common item priorities: {allPrioritiesSum}");

        var groupContents = RucksackParser.GroupAndUniqueContents(File.ReadLines(args[0]), 3);
        var badges = groupContents.Select(x => x.Aggregate((a, b) => a.Intersect(b)));
        var badgePriorities = badges.Select(x => x.Select(y => Prioritiser.Priority(y)).Sum());
        var badgePrioritiesSum = badgePriorities.Sum();

        Console.WriteLine($"Sum of badge priorities: {badgePrioritiesSum}");
    }
}
