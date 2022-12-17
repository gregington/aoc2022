using System.Collections.Immutable;

public static class Dropper {

    public static long FindHeightAfterDrops(ImmutableArray<ImmutableArray<byte>> rocks, ImmutableArray<char> jets, long numDrops) {
        var rockPeriod = rocks.Count();
        var jetPeriod = jets.Count();
        var maxPeriod = rockPeriod * jetPeriod;

        var dropIndexDict = new Dictionary<(int Rock, int Jet, byte TopLine), int>();
        var heightList = new List<int>();

        var rockIndex = 0;
        var jetIndex = 0;

        ImmutableArray<byte> board = ImmutableArray<byte>.Empty;
        var firstIndex = -1;
        var secondIndex = -1;
        for (var i = 0; i < maxPeriod; i++) {
            (board, jetIndex) = DropRock(board, rocks[rockIndex], jets, jetIndex);

            heightList.Add(board.Count());
            var key = (rockIndex, jetIndex, board.Last());
            if (dropIndexDict.ContainsKey(key)) {
                firstIndex = dropIndexDict[key];
                secondIndex = i;
                break;
            } else {
                dropIndexDict.Add(key, i);
            }

            rockIndex = (rockIndex + 1) % rocks.Count();
        }
        if (firstIndex == -1 || secondIndex == -1) {
            throw new Exception("Unexpected");
        }

        var offset = firstIndex;
        var period = secondIndex - firstIndex;

        var heightAtOffset = heightList[offset];
        var heightOverPeriod = heightList[secondIndex] - heightList[firstIndex];

        var numDropsExcludingOffset = numDrops - offset;
        var numPeriodsInDrops = numDropsExcludingOffset / period;
        var numRemainingDrops = numDropsExcludingOffset % period;

        var height = heightAtOffset 
            + (heightOverPeriod * numPeriodsInDrops) 
            + (heightList[(int) numRemainingDrops + offset - 1] - heightAtOffset);

        return height;
    }

    public static ImmutableArray<byte> Drop(ImmutableArray<ImmutableArray<byte>> rocks, ImmutableArray<char> jets, int numRocks) {
        ImmutableArray<byte> board = ImmutableArray<byte>.Empty;

        var rockIndex = 0;
        var jetIndex = 0;

        for (var i = 0; i < numRocks; i++) {
            (board, jetIndex) = DropRock(board, rocks[rockIndex], jets, jetIndex);
            rockIndex = (rockIndex + 1) % rocks.Count();
        }

        return board;
    }

    private static (ImmutableArray<byte> Board,  int jetIndex) DropRock(ImmutableArray<byte> board, ImmutableArray<byte> rock, ImmutableArray<char> jets, int jetIndex) {
        // extend board by two lines, then rock height
        var bottomOfRock = board.Count() + 4 - 1;
        var rockHeight = rock.Count();
        var extendedBoard = board.AddRange(Enumerable.Range(0, 3 + rockHeight).Select(_ => (byte)0));
        
        while (true) {
            // First jet
            var movedRock = rock.Move(jets[jetIndex]);
            jetIndex = (jetIndex + 1) % jets.Count();
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
        return (extendedBoard, jetIndex);
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