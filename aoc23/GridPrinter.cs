public static class GridPrinter {
    public static void Print(IEnumerable<Point> positions) {
        var boundingBox = BoundingBoxes.FindBoundingBox(positions);
        for (int y = boundingBox.Top; y <= boundingBox.Bottom; y++) {
            for (int x = boundingBox.Left; x <= boundingBox.Right; x++) {
                Console.Write(positions.Contains(new Point(x, y)) ? "#" : ".");
            }
            Console.WriteLine();
        }
    }
}