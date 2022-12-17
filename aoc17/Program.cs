public class Program {
    public static void Main(string[] args) {
        var jets = Jets.GetJets(args[0]);
        var rocks = Shapes.GetShapes();
        var board = Dropper.Drop(rocks, jets, Convert.ToInt32(args[1]));
        Console.WriteLine($"Height at {args[1]}: {board.Count()}");

        var height = Dropper.FindHeightAfterDrops(rocks, jets, Convert.ToInt64(args[2]));
        Console.WriteLine($"Height at {args[2]}: {height}");        
    }
}