using System.Collections.Immutable;

public static class Parser {
    
    public static ImmutableArray<Node> Parse(string path, int key) {
        var values = ParseAsLongs(path);
        var nodes = values.Select(v => new Node(v * key)).ToImmutableArray();

        for (int i = 0; i < nodes.Length; i++) {
            var node = nodes[i];
            node.Prev = nodes[(i + nodes.Length - 1) % nodes.Length];
            node.Next = nodes[(i + 1) % nodes.Length];
        }

        return nodes;
    }

    public static ImmutableArray<long> ParseAsLongs(string path) {
        return File.ReadLines(path)
            .Select(x => Convert.ToInt64(x))
            .ToImmutableArray();
    }
}