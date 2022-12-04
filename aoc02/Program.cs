public class Progran {
    public static void Main(string[] args) {
        var strategy1 = Parser.ParseAsHandShapes(args[0]);
        var scores1 = strategy1.Select(x => Scorer.ScoreRound(x[0], x[1]));
        var totalScore1 = scores1.Sum();

        Console.WriteLine($"Total Score as Hand Shape: {totalScore1}");

        var strategy2 = Parser.ParseAsOpponentAndResult(args[0]);
        var scores2 = strategy2.Select(x => Scorer.ScoreRound(x.Opponent, PlayCalculator.Calculate(x.Opponent, x.Result)));
        var totalScore2 = scores2.Sum();

        Console.WriteLine($"Total Score as Oppoenent Hand Shape and Result: {totalScore2}");
    }
}
