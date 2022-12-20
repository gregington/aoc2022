public class Node {

    private int value;

    public Node(int value) {
        this.value = value;
        Prev = this;
        Next = this;
    }

    public int Value => value;

    public Node Prev;
    public Node Next;

    public void MoveLeft() {
        var oldPrev = Prev;
        var oldPrevPrev = Prev.Prev;
        var oldNext = Next;

        // What was previous becomes next
        this.Next = oldPrev;
        oldPrev.Next = oldNext;
        oldPrev.Prev = this;
        oldNext.Prev = oldPrev;

        // What was prev prev becomes prev
        this.Prev = oldPrevPrev;
        oldPrevPrev.Next = this;
    }

    public void MoveRight() {
        var oldNext = Next;
        var oldNextNext = Next.Next;
        var oldPrev = Prev;

        // What was next becomes prev
        this.Prev = oldNext;
        oldNext.Prev = oldPrev;
        oldNext.Next = this;
        oldPrev.Next = oldNext;

        // What was next next becomes next
        this.Next = oldNextNext;
        oldNextNext.Prev = this;
    }

    public override string ToString() {
        return $"{Prev.Value} <- {Value} -> {Next.Value}";
    }
}