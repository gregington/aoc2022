public class Program {
    public static void Main(string[] args) {
        var blueprints = Parser.Parse(args[0]);
        var qualityLevel = Solver.QualityLevel(blueprints, 24);
        Console.WriteLine($"Quality level: {qualityLevel}");
    }
}