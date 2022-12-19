public record Cost(int Ore, int Clay, int Obsidian) {
    
    public int this[Resource resource] =>
        resource switch {
            Resource.Ore => Ore,
            Resource.Clay => Clay,
            Resource.Obsidian => Obsidian,
            Resource.Geode => 0,
            _ => throw new Exception($"{resource}")
        };
}