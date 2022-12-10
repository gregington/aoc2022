public static class ScreenCalculator {

    public static string GetScreen(IEnumerable<(int Cycle, int XDuring, int xAfter)> xValues) => 
        string.Join('\n', GetScreenLines(xValues));

    private static IEnumerable<String> GetScreenLines(IEnumerable<(int Cycle, int XDuring, int XAfter)> xValues) => 
        CalculatePixels(xValues)
            .Chunk(40)
            .Select(x => new string(x));

    private static IEnumerable<char> CalculatePixels(IEnumerable<(int Cycle, int XDuring, int XAfter)> xValues) => 
        xValues
            .Select(x => (Column: (x.Cycle - 1) % 40, x.XDuring))
            .Select(x => (x.Column, SpriteMin: x.XDuring - 1, SpriteMax: x.XDuring + 1))
            .Select(x => x.Column >= x.SpriteMin && x.Column <= x.SpriteMax ? '#' : ' ');
}