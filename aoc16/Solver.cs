using System.Collections.Immutable;

public static class Solver {


    public static int Solve(ImmutableDictionary<string, ImmutableArray<string>> network, ImmutableDictionary<string, int> flowRates, int numPlayers, int timeLimit) {
        var distances = DistanceCalculator.Calculate(network);

        var valvesToOpen = flowRates.Where(kvp => kvp.Value != 0)
            .Select(kvp => kvp.Key)
            .ToImmutableHashSet();

        var initialPlayerStates = Enumerable.Range(0, numPlayers)
            .Select(_ => new PlayerState("AA", 0))
            .ToImmutableArray();

        return MaxFlow(distances, flowRates, 0, 0, initialPlayerStates, valvesToOpen, timeLimit);

    }

    private static int MaxFlow(
        ImmutableDictionary<string, ImmutableDictionary<string, int>> distances,
        ImmutableDictionary<string, int> flowRates,
        int maxFlow,
        int currentFlow,
        ImmutableArray<PlayerState> playerStates,
        ImmutableHashSet<string> valvesToOpen,
        int timeRemaining) {
        
        var nextPlayerStates = ImmutableArray<ImmutableArray<PlayerState>>.Empty;

        for (var i = 0; i < playerStates.Length; i++) {
            var player = playerStates[i];

            if (player.DistanceToValve > 0) {
                // Move closer to targer
                nextPlayerStates = nextPlayerStates.Add(
                    ImmutableArray.Create<PlayerState>(player with {DistanceToValve = player.DistanceToValve - 1}));
            } else if (valvesToOpen.Contains(player.NextValve)) {
                // At target, open valve
                currentFlow += flowRates[player.NextValve] * (timeRemaining - 1);
                maxFlow = Math.Max(maxFlow, currentFlow);

                valvesToOpen = valvesToOpen.Remove(player.NextValve);

                // No new state for player - took time unit opening valve
                nextPlayerStates = nextPlayerStates.Add(ImmutableArray.Create<PlayerState>(player));
            } else {
                // Valve already open - look for next valve
                // Subtract distance by 1 as we move one step closer in the time slot
                nextPlayerStates = nextPlayerStates.Add(
                    valvesToOpen.Select(v => new PlayerState(v, distances[player.NextValve][v] - 1))
                    .ToImmutableArray());
            }
        }

        if (--timeRemaining <= 0) {
            return maxFlow;
        }

        var maxPossibleFlow = CalculateMaxPossibleFlow(valvesToOpen, distances, flowRates, currentFlow, timeRemaining, playerStates.Count());
        if (maxPossibleFlow <= maxFlow) {
            return maxFlow;
        }

        foreach (var nextMove in CartesianProductExtensions.CartesianProduct(nextPlayerStates)) {
            maxFlow = MaxFlow(distances, flowRates, maxFlow, currentFlow, nextMove.ToImmutableArray(), valvesToOpen, timeRemaining);
        }

        return maxFlow;
    }

    private static int CalculateMaxPossibleFlow(ImmutableHashSet<string> valvesToOpen, ImmutableDictionary<string, ImmutableDictionary<string, int>> distances, ImmutableDictionary<string, int> flowRates, int currentFlow, int timeRemaining, int numPlayers) {
        var sortedFlowRates = flowRates.Where(kvp => valvesToOpen.Contains(kvp.Key))
            .Select(kvp => kvp.Value)
            .OrderDescending();

        var chunks = sortedFlowRates.Chunk(numPlayers);

        var futureFlow = 0;
        foreach (var chunk in chunks) {
            if (timeRemaining <= 0) {
                break;
            }

            timeRemaining--;
            foreach (var valveFlow in chunk) {
                futureFlow += timeRemaining * valveFlow;
            }
            timeRemaining--;
        }

        return currentFlow + futureFlow;
    }


/*
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
    }
    */
}