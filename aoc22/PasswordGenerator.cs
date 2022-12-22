public static class PasswordGenerator {
    public static int Generate(int x, int y, Direction direction) {
        return y * 1000 + x * 4 + (int) direction;
    }
}