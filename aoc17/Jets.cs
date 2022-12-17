using System.Collections.Immutable;

public static class Jets {
    public static IEnumerable<char> Parse(string path) {
        var line = File.ReadAllLines(path).First();

        return RepeatJets(line);
    }

    private static IEnumerable<char> RepeatJets(string line) {
        while (true) {
            foreach (var c in line) {
                yield return c;
            }
        }
    }
}