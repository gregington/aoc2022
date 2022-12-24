public record struct Blizzard(Point Location, Direction Direction) {
    public Blizzard Move() {
        return this with { Location = Location.Move(Direction) };
    }
}