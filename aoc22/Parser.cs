using System.Collections.Immutable;
using System.Text;

public static class Parser {
    
    public static (ImmutableArray<ImmutableArray<char>> Map, ImmutableArray<Instruction> Path) Parse(String filePath) {
        var lines = File.ReadAllLines(filePath);

        var map = ParseMap(lines.TakeWhile(l => l.Trim().Length > 0));
        var insrutions = ParseInstructions(lines.Last()).ToImmutableArray();
        return (map, insrutions);
    }

    private static ImmutableArray<ImmutableArray<char>> ParseMap(IEnumerable<string> lines) {
        var rows = lines.ToImmutableArray();
        var maxCols = rows.Select(row => row.Length).Max();
        rows = rows.Select(row => row.Length < maxCols ? row + new string(' ', maxCols - row.Length) : row).ToImmutableArray();

        return rows.Select(row => row.ToCharArray().ToImmutableArray()).ToImmutableArray();
    }

    private static IEnumerable<Instruction> ParseInstructions(string line) {
        using (var enumerator = SplitLettersAndNumbers(line).GetEnumerator()) {
            while (enumerator.MoveNext()) {
                var s = enumerator.Current;
                if (s == "L") {
                    yield return Instruction.Left;
                } else if (s == "R") {
                    yield return Instruction.Right;
                } else {
                    var numForward = Convert.ToInt32(s);
                    for (var i = 0; i < numForward; i++) {
                        yield return Instruction.Forward;
                    }
                }
            }
        }
    }

    private static IEnumerable<string> SplitLettersAndNumbers(string line) {
        var sb = new StringBuilder();
         var enumerator = line.ToCharArray().GetEnumerator();
        while (enumerator.MoveNext()) {
            var c = enumerator.Current;
            if (char.IsDigit((char) c)) {
                sb.Append(c);
            } else {
                if (sb.Length > 0) {
                    yield return sb.ToString();
                    sb.Clear();
                }
                yield return Convert.ToString(c)!;
            }
        }
        if (sb.Length > 0) {
            yield return sb.ToString();
        }
    }
}