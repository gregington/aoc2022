public static class Parser {
    public static IEnumerable<((int, int) First, (int, int) Second)> Parse(string path) {
        return File.ReadLines(path)
            .Select(x => ParseLine(x));
    }

    public static ((int, int) First, (int, int) Second) ParseLine(String line) {
        var ranges = line.Trim().Split(",");
        var firstLimits = ranges[0].Split("-");
        var secondLimits = ranges[1].Split("-");

        return (
            (Convert.ToInt32(firstLimits[0]), Convert.ToInt32(firstLimits[1])),
            (Convert.ToInt32(secondLimits[0]), Convert.ToInt32(secondLimits[1])));
    }
}