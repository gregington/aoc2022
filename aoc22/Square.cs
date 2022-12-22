public class Square {

    public Square(int x, int y, Terrain terrain) {
        X = x;
        Y = y;
        Terrain = terrain;
        Up = this;
        Right = this;
        Down = this;
        Left = this;
    }

    public int X { get; }
    public int Y { get; }
    public Terrain Terrain { get; }
    public Square Up { get; set; }
    public Square Right { get; set; }
    public Square Down { get; set; }
    public Square Left { get; set; }

    public Square Move(Direction direction) => 
        direction switch {
            Direction.Up => Up,
            Direction.Right => Right,
            Direction.Down => Down,
            Direction.Left => Left,
            _ => throw new Exception("Unknown direction")
        };
}