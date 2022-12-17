using System.Collections.Immutable;

public static class Shapes {
    public static ImmutableArray<ImmutableArray<byte>> GetShapes() {
        return ShapesAsStrings()
            .Select(x => x.Select(StringAsBits).Reverse().ToImmutableArray())
            .ToImmutableArray();
    }

    public static ImmutableArray<byte> Move(this ImmutableArray<byte> shape, char direction) {
        if (direction == '>') {
            if (shape.Any(b => (b & (1 << 6)) > 0)) {
                return shape;
            }
            return shape.Select(b => (byte) (b << 1)).ToImmutableArray();
        } else if (direction == '<') {
            if (shape.Any(b => (b & 1) > 0)) {
                return shape;
            }
            return shape.Select(b => (byte) (b >> 1)).ToImmutableArray();
        }

        throw new ArgumentException(direction.ToString());
    }

    private static byte StringAsBits(String s) {
        var shapeWidth = s.Length;

        s = new String(s.ToArray());
        byte b = 0;
        for (var i = 0; i < shapeWidth; i++) {
            b = (byte) (b | (s[i] == '#' ? 1 : 0) << i);
        }

        return (byte) (b << 2);
    }

    private static ImmutableArray<ImmutableArray<string>> ShapesAsStrings() {
        return ImmutableArray.Create(
            ImmutableArray.Create(
                "####"
            ),
            ImmutableArray.Create(
                ".#.",
                "###",
                ".#."
            ),
            ImmutableArray.Create(
                "..#",
                "..#",
                "###"
            ),
            ImmutableArray.Create(
                "#",
                "#",
                "#",
                "#"
            ),
            ImmutableArray.Create(
                "##",
                "##"
            )
        );
    }
}