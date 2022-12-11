using System.Collections.Immutable;

public static class ChunkExtensions {
    public static IEnumerable<ImmutableArray<string>> ChunkByNewLines(this IEnumerable<string> source) {
        var list = ImmutableArray<string>.Empty;
        using (var enumerator = source.GetEnumerator()) {
            while (enumerator.MoveNext()) {
                var line = enumerator.Current;
                if (line.Trim() == "" && list.Count() > 0) {
                    yield return list;
                    list = ImmutableArray<string>.Empty;
                    continue;
                }
                list = list.Add(line);
            }
        }
        if (list.Count() > 0) {
            yield return list;
        }
    }
}