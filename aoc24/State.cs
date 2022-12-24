public record struct State(int Time, Point Expedition, int TargetCount, int MaxTargets, Point GoalCoordiate, Point StartCoordinate) {

    public Point CurrentTarget => TargetCount % 2 == 0 ? GoalCoordiate : StartCoordinate;
}