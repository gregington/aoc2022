public static class CoordinateFinder {
    public static int Find(Node head) {
        var zero = FindZeroNode(head);

        var current = zero;
        var sum = 0;
        for (var i = 0; i < 3; i++) {
            current = MoveNext(current, 1000);
            sum += current.Value;
        }
        return sum;
    }

    private static Node FindZeroNode(Node node) {
        if (node.Value == 0) {
            return node;
        }
        return FindZeroNode(node.Next);
    }

    private static Node MoveNext(Node node, int movesRemaining) {
        if (movesRemaining == 0) {
            return node;
        }
        return MoveNext(node.Next, movesRemaining -1);
    }
}