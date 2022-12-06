using System.Collections.Immutable;
using System.Reactive;
using System.Reactive.Linq;

public static class DataStreamParser {

    public static IObservable<char> GetCharacterObservable(string path) {
        var line = File.ReadLines(path).First();
        return line.ToCharArray().ToObservable();
    }

    public static int FindStartOfPacketMarker(this IObservable<char> dataStream, int numUniqueChars) {
        return Observable.Generate(1, _ => true, x => x + 1, x => x)
            .Zip(dataStream, (i, c) => (Index: i, Character: c))
            .Buffer(numUniqueChars, 1)
            .Select(x => {
                var unique = x.Select(y => y.Character).Distinct().Count() == numUniqueChars;
                return unique ? x.Last().Index : -1;
            })
            .SkipWhile(x => x == -1)
            .Take(1)
            .FirstAsync().Wait();
    }
}