public static class Ranges {
    public static bool Contains((int, int) first, (int, int) second) {
        // Check second in first
        if (second.Item1 >= first.Item1 && second.Item2 <= first.Item2) {
            return true;
        }

        // Check first in second
        return (first.Item1 >= second.Item1 && first.Item2 <= second.Item2);
    }

    public static Boolean Disjoint((int, int) first, (int, int) second) {
        return second.Item2 < first.Item1 || second.Item1 > first.Item2;
    }

    public static Boolean Overlaps((int, int) first, (int, int) second) {
        return !Disjoint(first, second);
    }
}