using System.Collections.Immutable;
using System.Text.RegularExpressions;

public static class Parser {
    public static Regex BlueprintRegex = new Regex(@"^Blueprint (?<blueprint>\d+): Each ore robot costs (?<ore_ore>\d+) ore. Each clay robot costs (?<clay_ore>\d+) ore. Each obsidian robot costs (?<obsidian_ore>\d+) ore and (?<obsidian_clay>\d+) clay. Each geode robot costs (?<geode_ore>\d+) ore and (?<geode_obsidian>\d+) obsidian.$");

    public static ImmutableDictionary<int, ImmutableDictionary<Resource, Cost>> Parse(String path) {
        return File.ReadLines(path)
            .Select(line => BlueprintRegex.Match(line).Groups)
            .ToImmutableDictionary(
                g => Convert.ToInt32(g["blueprint"].Value),
                g => new Dictionary<Resource, Cost>() {
                        [Resource.Ore] = new Cost(Convert.ToInt32(g["ore_ore"].Value), 0, 0),
                        [Resource.Clay] = new Cost(Convert.ToInt32(g["clay_ore"].Value), 0, 0),
                        [Resource.Obsidian] = new Cost(Convert.ToInt32(g["obsidian_ore"].Value), Convert.ToInt32(g["obsidian_clay"].Value), 0),
                        [Resource.Geode] = new Cost(Convert.ToInt32(g["geode_ore"].Value), 0, Convert.ToInt32(g["geode_obsidian"].Value))
                    }.ToImmutableDictionary()
                );            
    }
}