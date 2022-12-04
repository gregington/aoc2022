public static class Scorer {

    public static int ScoreRound(HandShape opponent, HandShape me) {
        return me.Score() + Play(opponent, me).Score();
    }

    public static Result Play(HandShape opponent, HandShape me) {
        if (me == opponent) {
            return Result.Draw;
        }

        if ((me == HandShape.Rock && opponent == HandShape.Scissors) ||
            (me == HandShape.Paper && opponent == HandShape.Rock) ||
            (me == HandShape.Scissors && opponent == HandShape.Paper)) {
                return Result.Win;
        }

        return Result.Lose;
    }
}