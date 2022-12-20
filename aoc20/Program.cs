using System.Text;

public class Program {
    public static void Main(string[] args) {
        var nodes = Parser.Parse(args[0], 1);
        Mixer.Mix(nodes, 1);
        var coordinate1 = CoordinateFinder.Find(nodes[0]);
        Console.WriteLine($"Coordinate 1: {coordinate1}");

        var keyNodes = Parser.Parse(args[0], 811589153);
        Mixer.Mix(keyNodes, 10);
        var coordinate2 = CoordinateFinder.Find(keyNodes[0]);
        Console.WriteLine($"Coordinate 2: {coordinate2}");
    }
}