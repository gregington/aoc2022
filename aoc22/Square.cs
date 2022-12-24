public class Square {

    public Square(int x, int y, Terrain terrain) {
        X = x;
        Y = y;
        Terrain = terrain;
        Up = (this, Direction.Up);
        Right = (this, Direction.Right);
        Down = (this, Direction.Down);
        Left = (this, Direction.Left);
    }

    public int X { get; }
    public int Y { get; }
    public Terrain Terrain { get; }
    
    public (Square Square, Direction Direction) Up { get; set; }
    public (Square Square, Direction Direction) Right { get; set; }
    public (Square Square, Direction Direction) Down { get; set; }
    public (Square Square, Direction Direction) Left { get; set; }

    public (Square Square, Direction Direction) Move(Direction direction) => 
        direction switch {
            Direction.Up => Up,
            Direction.Right => Right,
            Direction.Down => Down,
            Direction.Left => Left,
            _ => throw new Exception("Unknown direction")
        };
}