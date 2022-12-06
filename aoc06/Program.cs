public class Program {
    public static void Main(string[] args) {
        var startOfPacket = DataStreamParser.GetCharacterObservable(args[0])
            .FindStartOfPacketMarker(4);

        Console.WriteLine($"Start of packet marker: {startOfPacket}");
    }
}
