public static class ResultParser {
    public static Result Parse(string input) {
        switch (input) {
            case "X":
                return Result.Lose;
            case "Y":
                return Result.Draw;
            case "Z":
                return Result.Win;
            default:
                throw new ArgumentException(input);       
        }
    }
}