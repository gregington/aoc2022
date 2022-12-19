using System.Collections.Immutable;

public static class Solver {

    private record struct State(int ore, int clay, int obsidian, int geodes, int oreRobots, int clayRobots, int obsidianRobots, int geodeRobots, int timeRemaining);

    public static int QualityLevel(ImmutableDictionary<int, ImmutableDictionary<Resource, Cost>> blueprints, int timeLimit) {
        return blueprints
            .OrderBy(kvp => kvp.Key)
            .AsParallel()
            .Select(kvp => kvp.Key * Solve(kvp.Key, kvp.Value, timeLimit))
            .Sum();
    }

    public static int Solve(int blueprintNum, ImmutableDictionary<Resource, Cost> blueprint, int timeLimit) {
        ImmutableDictionary<Resource, int> resources = Enum.GetValues<Resource>()
            .ToImmutableDictionary(r => r, _ => 0);

        ImmutableDictionary<Resource, int> robots = Enum.GetValues<Resource>()
            .ToImmutableDictionary(r => r, _ => 0)
            .SetItem(Resource.Ore, 1);
        
        var queue = new Queue<State>();
        queue.Enqueue(new State(0, 0, 0, 0, 1, 0, 0, 0, timeLimit));

        var maxSpends = Enum.GetValues<Resource>()
            .ToImmutableDictionary(
                r => r,
                r => blueprint.Values.Select(c => c[r]).Max());

        var seen = new HashSet<State>();

        var maxGeode = 0;

        while (queue.Count() > 0) {
            var state = queue.Dequeue();
            if (seen.Contains(state)) {
                continue;
            }
            seen.Add(state);

            var (ore, clay, obsidian, geode, oreRobots, clayRobots, obsidianRobots, geodeRobots, timeRemaining) = state;

            maxGeode = Math.Max(geode, maxGeode);

            if (seen.Count() % 1_000_000 == 0) {
                Console.WriteLine($"{blueprintNum:d2} -> Seen: {seen.Count():n0}, Queue: {queue.Count():n0}, Best: {maxGeode}");
            }

            if (timeRemaining <= 0) {
                continue;
            }

            var canBuild = CanBuild(blueprint, ore, clay, obsidian, geode);


            // filter where building more robots than we can spend

            foreach (var build in canBuild) {
                var cost = blueprint[build];
                if (build == Resource.Ore && oreRobots + 1 <= maxSpends[Resource.Ore]) {
                    queue.Enqueue(new State(
                        ore - cost[Resource.Ore] + oreRobots, 
                        clay - cost[Resource.Clay] + clayRobots, 
                        obsidian -cost[Resource.Obsidian] + obsidianRobots, 
                        geode - cost[Resource.Geode] + geodeRobots, 
                        oreRobots + 1, clayRobots, obsidianRobots, geodeRobots, timeRemaining - 1));
                } else if (build == Resource.Clay && clayRobots + 1 <= maxSpends[Resource.Clay]) {
                    queue.Enqueue(new State(
                        ore - cost[Resource.Ore] + oreRobots, 
                        clay - cost[Resource.Clay] + clayRobots, 
                        obsidian -cost[Resource.Obsidian] + obsidianRobots, 
                        geode - cost[Resource.Geode] + geodeRobots, 
                        oreRobots, clayRobots + 1, obsidianRobots, geodeRobots, timeRemaining - 1));
                } else if (build == Resource.Obsidian && obsidianRobots + 1 <= maxSpends[Resource.Obsidian]) {
                    queue.Enqueue(new State(
                        ore - cost[Resource.Ore] + oreRobots, 
                        clay - cost[Resource.Clay] + clayRobots, 
                        obsidian -cost[Resource.Obsidian] + obsidianRobots, 
                        geode - cost[Resource.Geode] + geodeRobots, 
                        oreRobots, clayRobots, obsidianRobots + 1, geodeRobots, timeRemaining - 1));
                } else if (build == Resource.Geode) {
                    queue.Enqueue(new State(
                        ore - cost[Resource.Ore] + oreRobots, 
                        clay - cost[Resource.Clay] + clayRobots, 
                        obsidian -cost[Resource.Obsidian] + obsidianRobots, 
                        geode - cost[Resource.Geode] + geodeRobots, 
                        oreRobots, clayRobots, obsidianRobots, geodeRobots + 1, timeRemaining - 1));
                }
            }
            // build nothing
            queue.Enqueue(new State(ore + oreRobots, clay + clayRobots, obsidian + obsidianRobots, geode + geodeRobots, oreRobots, clayRobots, obsidianRobots, geodeRobots, timeRemaining - 1));
        }

        Console.WriteLine($"{blueprintNum:d2} -> Done. Max geodes = {maxGeode}");
        return maxGeode;
    }

    private static IEnumerable<Resource> CanBuild(ImmutableDictionary<Resource, Cost> blueprint, int ore, int clay, int obsidian, int geode) {
        foreach (var resource in Enum.GetValues<Resource>()) {
            var cost = blueprint[resource];
            if (ore >= cost[Resource.Ore] && clay >= cost[Resource.Clay] && obsidian >= cost[Resource.Obsidian] && geode >= cost[Resource.Geode]) {
                yield return resource;
            }
        }
    }
}