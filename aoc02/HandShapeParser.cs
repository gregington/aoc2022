public static class HandShapeParser {
    public static HandShape Parse(string input) {
        switch (input) {
            case "A":
            case "X":
                return HandShape.Rock;
            case "B":
            case "Y":
                return HandShape.Paper;
            case "C":
            case "Z":
                return HandShape.Scissors;
            default:
                throw new ArgumentException(input);
        }
    }
}