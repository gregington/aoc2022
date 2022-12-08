using System.Collections.Immutable;

public static class ScenicScoreCalculator {
    public static int CalculateMax(ImmutableArray<ImmutableArray<int>> grid) {
        var rowCount = grid.Count();
        var colCount = grid[0].Count();
        return Enumerable.Range(0, rowCount)
            .SelectMany(row => Enumerable.Range(0, colCount).Select(col => (Row: row, Col: col)))
            .Select(x => Score(grid, x.Row, x.Col))
            .Max();
    }

    private static int Score(ImmutableArray<ImmutableArray<int>> grid, int row, int col) {
        return ViewingDistanceNorth(grid, row, col)
            * ViewingDistanceEast(grid, row, col)
            * ViewingDistanceSouth(grid, row, col)
            * ViewingDistanceWest(grid, row, col);
    }

    public static int ViewingDistanceNorth(ImmutableArray<ImmutableArray<int>> grid, int row, int col) {
        var values = Enumerable.Range(0, row).Select(r => grid[r][col]).Reverse().ToImmutableArray();
        return ViewingDistance(values, grid[row][col]);
    }

    private static int ViewingDistanceEast(ImmutableArray<ImmutableArray<int>> grid, int row, int col) {
        var values = Enumerable.Range(col + 1, grid[0].Length - col - 1).Select(c => grid[row][c]).ToImmutableArray();
        return ViewingDistance(values, grid[row][col]);
    }

    private static int ViewingDistanceSouth(ImmutableArray<ImmutableArray<int>> grid, int row, int col) {
        var values = Enumerable.Range(row + 1, grid.Length - row - 1).Select(r => grid[r][col]).ToImmutableArray();
        return ViewingDistance(values, grid[row][col]);
    }

    private static int ViewingDistanceWest(ImmutableArray<ImmutableArray<int>> grid, int row, int col) {
        var values = Enumerable.Range(0, col).Select(c => grid[row][c]).Reverse().ToImmutableArray();
        return ViewingDistance(values, grid[row][col]);
    }

    private static int ViewingDistance(IEnumerable<int> treeHeights, int treeHouseHeight) {
        var count = treeHeights.Count();
        if (count == 0) {
            return 0;
        }
        return Math.Min(count, treeHeights.TakeWhile(h => h < treeHouseHeight).Count() + 1);
    }
}