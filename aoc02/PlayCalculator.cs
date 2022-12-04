using System.Collections.Immutable;

public static class PlayCalculator {

    private static ImmutableDictionary<(HandShape Opponent, Result Result), HandShape> PlayTable = new Dictionary<(HandShape Opponent, Result Result), HandShape> {
        [(HandShape.Rock, Result.Lose)] = HandShape.Scissors,
        [(HandShape.Rock, Result.Draw)] = HandShape.Rock,
        [(HandShape.Rock, Result.Win)] = HandShape.Paper,
        [(HandShape.Paper, Result.Lose)] = HandShape.Rock,
        [(HandShape.Paper, Result.Draw)] = HandShape.Paper,
        [(HandShape.Paper, Result.Win)] = HandShape.Scissors,
        [(HandShape.Scissors, Result.Lose)] = HandShape.Paper,
        [(HandShape.Scissors, Result.Draw)] = HandShape.Scissors,
        [(HandShape.Scissors, Result.Win)] = HandShape.Rock,
    }.ToImmutableDictionary();

    public static HandShape Calculate(HandShape opponent, Result result) {
        return PlayTable[(opponent, result)];
    }
}