public class Progran {
    public static void Main(string[] args) {
        var strategy = Parser.Parse(args[0]);
        var scores = strategy.Select(x => Scorer.ScoreRound(x[0], x[1]));
        var totalScore = scores.Sum();

        Console.WriteLine($"Total xScore: {totalScore}");
    }
}
