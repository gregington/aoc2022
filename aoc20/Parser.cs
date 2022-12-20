using System.Collections.Immutable;

public static class Parser {
    
    public static ImmutableArray<Node> Parse(string path) {
        var values = ParseAsInts(path);
        var nodes = values.Select(v => new Node(v)).ToImmutableArray();

        for (int i = 0; i < nodes.Length; i++) {
            var node = nodes[i];
            node.Prev = nodes[(i + nodes.Length - 1) % nodes.Length];
            node.Next = nodes[(i + 1) % nodes.Length];
        }

        return nodes;
    }

    public static ImmutableArray<int> ParseAsInts(string path) {
        return File.ReadLines(path)
            .Select(x => Convert.ToInt32(x))
            .ToImmutableArray();
    }
}