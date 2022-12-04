public static class Prioritiser {
    public static int Priority(char c) {
        if (c >= 'a' && c <= 'z') {
            return c - 'a' + 1;
        }
        if (c >= 'A' && c <= 'Z') {
            return c - 'A' + 27;
        }
        throw new ArgumentException(Convert.ToString(c));
    }
}