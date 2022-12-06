public class Program {
    public static void Main(string[] args) {
        var dataStream = DataStreamParser.GetCharacterObservable(args[0]);
            
        var startOfPacketPosition = dataStream.FindStartOfPacketPosition();
        Console.WriteLine($"Start of packet position: {startOfPacketPosition}");

        var startOfMessagePosition = dataStream.FindStartOfMessagePosition();
        Console.WriteLine($"Start of message position: {startOfMessagePosition}");
    }
}
