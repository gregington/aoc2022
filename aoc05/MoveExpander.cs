public static class MoveExpander {
    public static IEnumerable<(int From, int To)> Expand(this IEnumerable<(int Quantity, int From, int To)> source) {
        using (var enumerator = source.GetEnumerator()) {
            while (enumerator.MoveNext()) {
                var current = enumerator.Current;
                for (var i = 0; i < current.Quantity; i++) {
                    yield return (current.From, current.To);
                }
            }
        }
    }
}