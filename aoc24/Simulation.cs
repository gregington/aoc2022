public static class Simulation {
    
    private record struct State(int Time, Point Expedition);

    public static int Run(Valley valley) {

        var valleyCache = new Dictionary<int, Valley> {
            [0] = valley
        };

        var queue = new Queue<State>();
        queue.Enqueue(new State(0, valley.Start));

        return Run(queue, valleyCache);
    }

    private static int Run(Queue<State> queue, Dictionary<int, Valley> valleyCache) {
        var seen = new HashSet<State>();

        while (queue.Count() > 0) {
            var state = queue.Dequeue();

            if (seen.Contains(state)) {
                continue;
            }
            seen.Add(state);

            var (time, expedition) = state;
            var valley = valleyCache[time];

            if (expedition == valley.Goal) {
                return time;
            }

            var nextTime = time + 1;
            Valley nextValley;
            if (valleyCache.ContainsKey(nextTime)) {
                nextValley = valleyCache[nextTime];
            } else {
                nextValley = valley.IncrementTime();
                valleyCache[nextTime] = nextValley;
            }

            var potentialMoves = Enum.GetValues<Direction>()
                .Select(d => expedition.Move(d))
                .Append(expedition); // wait

            foreach (var potentialMove in potentialMoves) {
                var (x, y) = potentialMove;

                if (!(x >= 0 && x < valley.XLength && y >= 0 && y < valley.YLength)) {
                    continue;
                }

                if (nextValley.Walls.Contains(potentialMove) || nextValley.Blizzards.ContainsKey(potentialMove)) {
                    continue;
                }

                queue.Enqueue(new State(nextTime, potentialMove));
            }
        }
        throw new Exception("Solution not found");
    }
}