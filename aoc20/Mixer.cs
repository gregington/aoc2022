using System.Collections.Immutable;

public static class Mixer {
    public static void Mix(ImmutableArray<Node> nodes) {
        foreach (var node in nodes) {
            if (node.Value == 0) {
                continue;
            }
            Action mixFunction = node.Value < 0 ? node.MoveLeft : node.MoveRight;

            for (var i = 0; i < Math.Abs(node.Value); i++) {
                mixFunction();
            }
        }
    }
}