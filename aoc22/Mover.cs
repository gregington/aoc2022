public static class Mover {
    public static (int Row, int Column, Direction Direction) Move(Square startSquare, Direction direction, IEnumerable<Instruction> instructions) {
        var currentSquare = startSquare;

        foreach(var instruction in instructions) {
            if (instruction == Instruction.Left) {
                direction = direction.TurnLeft();
            } else if (instruction == Instruction.Right) {
                direction = direction.TurnRight();
            } else if (instruction == Instruction.Forward) {
                (currentSquare, direction) = currentSquare.Move(direction);
            }
        }

        return (currentSquare.X, currentSquare.Y, direction);
    }
}