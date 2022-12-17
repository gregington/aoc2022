public class Program {
    public static void Main(string[] args) {
        var jets = Jets.Parse(args[0]);
        var rocks = Shapes.ShapeEnumerator();
        var board = Dropper.Drop(rocks, jets, Convert.ToInt32(args[1]));
        Console.WriteLine($"Height: {board.Count()}");
    }
}