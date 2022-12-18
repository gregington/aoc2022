public record struct BoundingBox(int MinX, int MaxX, int MinY, int MaxY, int MinZ, int MaxZ) {
    public bool Contains((int X, int Y, int Z) point) {
        return point.X >= MinX
            && point.X <= MaxX
            && point.Y >= MinY
            && point.Y <= MaxY
            && point.Z >= MinZ
            && point.Z <= MaxZ;
    }

    public bool OnEdge((int X, int Y, int Z) point) {
        return ((point.X == MinX || point.X == MaxX) && (point.Y >= MinY && point.Y <= MaxY) && (point.Z >= MinZ && point.Z <= MaxZ))
            || ((point.X >= MinX && point.X <= MaxX) && (point.Y == MinY || point.Y == MaxY) && (point.Z >= MinZ && point.Z <= MaxZ))
            || ((point.X >= MinX && point.X <= MaxX) && (point.Y >= MinY && point.Y <= MaxY) && (point.Z == MinZ || point.Z == MaxZ));
    }
}