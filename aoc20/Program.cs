using System.Text;

public class Program {
    public static void Main(string[] args) {
        var nodes = Parser.Parse(args[0]);
        Mixer.Mix(nodes);
        var coordinate = CoordinateFinder.Find(nodes[0]);
        Console.WriteLine($"Coordinate: {coordinate}");
    }
}