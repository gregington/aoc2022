using System.Collections.Immutable;

public static class VisiblityCalculator {
    
    public static int NumberVisible(ImmutableArray<ImmutableArray<int>> grid) {
        var rowCount = grid.Count();
        var colCount = grid[0].Count();
        return Enumerable.Range(0, rowCount)
            .SelectMany(row => Enumerable.Range(0, colCount).Select(col => (Row: row, Col: col)))
            .Select(x => IsVisible(grid, x.Row, x.Col))
            .Where(x => x)
            .Count();
    }

    private static bool IsVisible(ImmutableArray<ImmutableArray<int>> grid, int row, int col) {
        if (IsEdge(grid, row, col)) {
            return true;
        }

        return IsVisibleFromNorth(grid, row, col)
            || IsVisibleFromEast(grid, row, col)
            || IsVisibleFromSouth(grid, row, col)
            || IsVisibleFromWest(grid, row, col);
    }

    private static bool IsEdge(ImmutableArray<ImmutableArray<int>> grid, int row, int col) {
        int rowCount = grid.Count();
        int colCount = grid[0].Count();

        return row == 0 || row == rowCount - 1 || col == 0 || col == colCount - 1;
    }

    private static bool IsVisibleFromNorth(ImmutableArray<ImmutableArray<int>> grid, int row, int col) {
        return IsVisible(
            grid[row][col],
            Enumerable.Range(0, row).Select(r => grid[r][col]).ToImmutableArray());
    }

    private static bool IsVisibleFromEast(ImmutableArray<ImmutableArray<int>> grid, int row, int col) {
        return IsVisible(
            grid[row][col],
            Enumerable.Range(col + 1, grid[0].Length - col - 1).Select(c => grid[row][c]).ToImmutableArray());
    }

    private static bool IsVisibleFromSouth(ImmutableArray<ImmutableArray<int>> grid, int row, int col) {
        return IsVisible(
            grid[row][col],
            Enumerable.Range(row + 1, grid.Length - row - 1).Select(r => grid[r][col]).ToImmutableArray());
    }

    private static bool IsVisibleFromWest(ImmutableArray<ImmutableArray<int>> grid, int row, int col) {
        return IsVisible(
            grid[row][col],
            Enumerable.Range(0, col).Select(c => grid[row][c]).ToImmutableArray());
    }

    private static bool IsVisible(int height, IEnumerable<int> otherHeights) => 
        otherHeights.All(x => x < height);
}