using System.Collections.Immutable;

public static class SurfaceAreaCalculator {
    public static int Calculate(ImmutableHashSet<(int X, int Y, int Z)> scan) {
        return scan
            .Select(voxel => Neighbors(voxel).Where(n => !scan.Contains(n)).Count())
            .Sum();
    }

    public static IEnumerable<(int X, int Y, int Z)> Neighbors((int X, int Y, int Z) voxel) {
        yield return (voxel.X - 1, voxel.Y, voxel.Z);
        yield return (voxel.X + 1, voxel.Y, voxel.Z);
        yield return (voxel.X, voxel.Y - 1, voxel.Z);
        yield return (voxel.X, voxel.Y + 1, voxel.Z);
        yield return (voxel.X, voxel.Y, voxel.Z - 1);
        yield return (voxel.X, voxel.Y, voxel.Z + 1);
    }
}