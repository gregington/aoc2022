public class Program {
    public static void Main(string[] args) {
        var blueprints = Parser.Parse(args[0]);
        var qualityLevel = Solver.QualityLevel(blueprints, 24);
        Console.WriteLine($"Quality level: {qualityLevel}");

        var multipliedGeodes = Solver.MultiplyGeodes(blueprints, 32, Math.Min(blueprints.Count(), 3));
        Console.WriteLine($"Multiplied geodes: {multipliedGeodes}");
    }
}