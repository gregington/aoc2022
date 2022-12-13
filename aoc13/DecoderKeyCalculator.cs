using System.Collections.Immutable;

public static class DecoderKeyCalculator {
    public static int Calculate(IEnumerable<List<object>> packets, IEnumerable<List<object>> dividers) {
        var dividerStrings = dividers.Select(x => x.ToStringAoc()).ToImmutableHashSet();
        return packets
            .Select((x, i) => (Index: i + 1, Packet: x.ToStringAoc()))
            .Where(x => dividerStrings.Contains(x.Packet))
            .Select(x => x.Index)
            .Aggregate(1, (x, y) => x * y);
    }
}