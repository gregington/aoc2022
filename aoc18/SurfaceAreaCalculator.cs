using System.Collections.Immutable;

public static class SurfaceAreaCalculator {
    public static int Calculate(ImmutableHashSet<(int X, int Y, int Z)> scan) {
        return scan
            .Select(voxel => Neighbors(voxel).Where(n => !scan.Contains(n)).Count())
            .Sum();
    }

    public static int CalculateExternalSurfaceArea(ImmutableHashSet<(int X, int Y, int Z)> scan) {
        // Calculate bounding box that fully encloses droplet
        var boundingBox = new BoundingBox(
            scan.Select(voxel => voxel.X).Min() - 1,
            scan.Select(voxel => voxel.X).Max() + 1,
            scan.Select(voxel => voxel.Y).Min() - 1,
            scan.Select(voxel => voxel.Y).Max() + 1,
            scan.Select(voxel => voxel.Z).Min() - 1,
            scan.Select(voxel => voxel.Z).Max() + 1);

        var outside = CalculateOutsideVoxels(boundingBox, scan);

        return scan
            .Select(voxel => Neighbors(voxel).Where(n => outside.Contains(n)).Count())
            .Sum();
    }

    private static IEnumerable<(int X, int Y, int Z)> Neighbors((int X, int Y, int Z) voxel) {
        yield return (voxel.X - 1, voxel.Y, voxel.Z);
        yield return (voxel.X + 1, voxel.Y, voxel.Z);
        yield return (voxel.X, voxel.Y - 1, voxel.Z);
        yield return (voxel.X, voxel.Y + 1, voxel.Z);
        yield return (voxel.X, voxel.Y, voxel.Z - 1);
        yield return (voxel.X, voxel.Y, voxel.Z + 1);
    }

    public static ImmutableHashSet<(int X, int Y, int Z)> CalculateOutsideVoxels(BoundingBox boundingBox, ImmutableHashSet<(int X, int Y, int Z)> scan) {
        var visited = ImmutableHashSet<(int X, int Y, int Z)>.Empty;
        var outside = ImmutableHashSet<(int X, int Y, int Z)>.Empty;

        var queue = ImmutableQueue<(int X, int Y, int Z)>.Empty
            .Enqueue((boundingBox.MinX, boundingBox.MinY, boundingBox.MinZ));
        

        while (!queue.IsEmpty) {
            queue = queue.Dequeue(out var point);

            if (visited.Contains(point)) {
                continue;
            }
            visited = visited.Add(point);

            if (!boundingBox.Contains(point)) {
                continue;
            }

            if (scan.Contains(point)) {
                continue;
            }

            outside = outside.Add(point);

            var nextPoints = Neighbors(point);

            foreach (var nextPoint in nextPoints) {
                queue = queue.Enqueue(nextPoint);
            }
        }
        return outside;
    }
}