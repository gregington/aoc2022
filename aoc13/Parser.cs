using System.Collections.Immutable;

public static class Parser {
    public static IEnumerable<(List<object>, List<object>)> Parse(string path) {
        return Parse(File.ReadLines(path));
    }

    private static IEnumerable<(List<object>, List<object>)> Parse(IEnumerable<string> lines) {
        var nonEmptyLines = lines.Where(x => x.Trim().Length != 0);
        var lists = nonEmptyLines.Select(ParseLine).ToImmutableArray();

        var evens = lists.Where((x, i) => (i % 2) == 0);
        var odds = lists.Where((x, i) => (i % 2) == 1);

        return evens.Zip(odds);
    }

    public static List<object> ParseLine(string line) {
        var stack = new Stack<List<object>>();
        List<object>? list = null;
        var chars = "";

        foreach (var c in line) {
            if (c == '[') {
                if (list != null) {
                    stack.Push(list);
                }
                list = new List<object>();
                continue;
            }

            if (c == ']') {
                if (chars.Length > 0) {
                    list!.Add(Convert.ToInt32(chars));
                    chars = "";
                }
                if (stack.Count() > 0) {
                    var tempList = list;
                    list = stack.Pop();
                    if (tempList != null) {
                        list.Add(tempList);
                    }
                }
                continue;
            }

            if (c == ',') {
                if (chars.Length > 0) {
                    list!.Add(Convert.ToInt32(chars));
                    chars = "";
                }
                continue;
            }

            chars = chars + c;
        }

        return list!;
    }
}