public static class LinePrinter {
    public static void Print(this IEnumerable<byte> lines) {
        foreach (var line in lines.Reverse()) {
            Console.Write('|');
            for (var b = 0; b < 7; b++) {
                var rock = (line & (1 << b)) == 0 ? '.' : '#';
                Console.Write(rock);
            }
            Console.WriteLine("|");
        }
        Console.WriteLine("+-------+");
    }
}