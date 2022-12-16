using System.Collections.Immutable;

public static class Solver {

    private record struct State(string CurrentPosition, ImmutableDictionary<string, int> FlowRates, int TimeRemaining, int Pressure);

    public static int Solve(ImmutableDictionary<string, ImmutableArray<string>> network, ImmutableDictionary<string, int> flowRates) {

        var queue = new PriorityQueue<State, int>();
        var state = new State("AA", flowRates, 30, 0);
        queue.Enqueue(state, Heuristic(state));

        return Solve(queue, network);
    }

    private static int Solve(PriorityQueue<State, int> queue, ImmutableDictionary<string, ImmutableArray<string>> network) {
        var maxPressure = 0;

        while (queue.Count > 0) {
            var state = queue.Dequeue();
            var (currentPosition, flowRates, timeRemaining, pressure) = state;

            maxPressure = Math.Max(maxPressure, state.Pressure);

            if (timeRemaining <= 0) {
                continue;
            }

            if (MaxPossiblePressure(state) < maxPressure) {
                continue;
            }


            // These possibilities are for moving without turning off the valve. Pressure does not change
            foreach (var nextPosition in network[currentPosition]) {
                var nextState = new State(nextPosition, flowRates, timeRemaining - 1, pressure);
                queue.Enqueue(nextState, Heuristic(nextState));
            }

            // These possibilities are for turning off the valve and then moving. Pressure does change
            if (flowRates[currentPosition] > 0) {
                var newPressure = pressure + flowRates[currentPosition] * (timeRemaining - 1);
                var newFlowRates = flowRates.SetItem(currentPosition, 0);
                foreach (var nextPosition in network[currentPosition]) {
                    var nextState = new State(nextPosition, newFlowRates, timeRemaining - 2, newPressure);
                    queue.Enqueue(nextState, Heuristic(nextState));
                }
            }
        }

        return maxPressure;
    }

    private static int MaxPossiblePressure(State state) {
        var fr = state.FlowRates.Values
            .OrderDescending()
            .ToImmutableArray();

        var sum = 0;
        for (int tr = state.TimeRemaining - 2, i = 0; tr > 0 && i < fr.Length; tr -= 2, i++) {
            sum += tr * fr[i];
        }
        
        return state.Pressure + sum;
    }

    private static int Heuristic(State state) {
        return -1 * MaxPossiblePressure(state);
//        var timeToNextValve = Math.Max(0, state.TimeRemaining - 2);
//        return -1 * state.Pressure + timeToNextValve * state.FlowRates[state.NextMove];
    }
}