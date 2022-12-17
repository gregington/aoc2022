using System.Collections.Immutable;

public static class Dropper {
    public static ImmutableArray<byte> Drop(IEnumerable<ImmutableArray<byte>> rocks, IEnumerable<char> jets, int numRocks) {
        ImmutableArray<byte> board = ImmutableArray<byte>.Empty;

        using (var rockEnumerator = rocks.GetEnumerator())
        using (var jetEnumerator = jets.GetEnumerator()) {

            for (var i = 0; i < numRocks; i++) {
                rockEnumerator.MoveNext();
                board = DropRock(board, rockEnumerator.Current, jetEnumerator);
            }
        }
        return board;
    }

    private static ImmutableArray<byte> DropRock(ImmutableArray<byte> board, ImmutableArray<byte> rock, IEnumerator<char> jets) {
        // extend board by two lines, then rock height
        var bottomOfRock = board.Count() + 4 - 1;
        var rockHeight = rock.Count();
        var extendedBoard = board.AddRange(Enumerable.Range(0, 3 + rockHeight).Select(_ => (byte)0));
        
        while (true) {
            // First jet
            jets.MoveNext();
            var movedRock = rock.Move(jets.Current);
            if (!Collision(extendedBoard, movedRock, bottomOfRock)) {
                rock = movedRock;
            }

            if (Collision(extendedBoard, rock, bottomOfRock - 1)) {
                break;
            }
            
            // No collisions, drop rock
            bottomOfRock--;
        }

        extendedBoard = PlaceRock(extendedBoard, rock, bottomOfRock);
        extendedBoard = RemoveEmptyLines(extendedBoard);
        return extendedBoard;
    }

    private static bool Collision(ImmutableArray<byte> board, ImmutableArray<byte> rock, int bottomOfRock) {
        if (bottomOfRock < 0) {
            return true;
        }
        for (var i = 0; i < rock.Count(); i++) {
            if ((rock[i] & board[i + bottomOfRock]) != 0) {
                return true;
            }
        }
        return false;
    }

    private static ImmutableArray<byte> PlaceRock(ImmutableArray<byte> board, ImmutableArray<byte> rock, int bottomOfRock) {
        for (var i = 0; i < rock.Count(); i++) {
            if ((rock[i] & board[i + bottomOfRock]) != 0) {
                throw new Exception("Should not happen");
            }
            var boardLine = board[i + bottomOfRock];
            var rockLine = rock[i];
            var updatedLine = (byte) (boardLine | rockLine);
            board = board.SetItem(i + bottomOfRock, updatedLine);
        }
        return board;
    }

    private static ImmutableArray<byte> RemoveEmptyLines(ImmutableArray<byte> board) {
        for (var i = board.Count() - 1; i >= 0; i--) {
            if (board[i] != 0) {
                break;
            }
            board = board.RemoveAt(i);
        }
        return board;
    }
}