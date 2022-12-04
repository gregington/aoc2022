using System.Collections.Immutable;

public static class CalorieCounter {

    public static IEnumerable<int> CountCaloriesPerElf(string path) {
        var lines = File.ReadAllLines(path);

        return lines.GroupByNewLine()
            .Select(x => x.Select(y => Convert.ToInt32(y)))
            .Select(x => x.Sum());
    }

    public static int GetMaxCalories(IEnumerable<int> caloriesPerElf) => caloriesPerElf.Max();

    public static IEnumerable<int> GetTopCalories(IEnumerable<int> caloriesPerElf, int topN) =>
        caloriesPerElf.OrderDescending().Take(topN);

    private static IEnumerable<ImmutableList<string>> GroupByNewLine(this IEnumerable<string> source) {
        using (var enumerator = source.GetEnumerator()) {
            var list = new List<string>();
            while (enumerator.MoveNext()) {
                var current = enumerator.Current.Trim();
                if (current == "" && list.Any()) {
                    yield return list.ToImmutableList();
                    list = new List<string>();
                } else {
                    list.Add(current);
                }
            }
            if (list.Any()) {
                yield return list.ToImmutableList();
            }
        }
    }
}