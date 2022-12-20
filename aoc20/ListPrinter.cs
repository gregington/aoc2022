using System.Text;

public static class ListPrinter {
    public static void Print(Node node) {
        var sb = new StringBuilder();
        var head = node;
        var current = head;
        while (true) {
            sb.Append(current.Value);
            if (current.Next == head) {
                break;
            }
            sb.Append(", ");
            current = current.Next;
        }

        Console.WriteLine(sb.ToString());
    }

}