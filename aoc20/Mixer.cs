using System.Collections.Immutable;

public static class Mixer {
    public static void Mix(ImmutableArray<Node> nodes, int numRounds) {
        var nodeCount = nodes.Length;
        for (var i = 0; i < numRounds; i++) {
            foreach (var node in nodes) {
                if (node.Value == 0) {
                    continue;
                }

                var newPosition = Mod(node.Value, nodeCount - 1);

                for (var j = 0; j < newPosition; j++) {
                    node.MoveRight();
                }
            }
        }
    }

    private static long Mod(long a, long b) {
        var c = a % b;
        return (c < 0) ? c + b : c;
    }

}