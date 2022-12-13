public class MessageComparer : IComparer<object>
{
    public int Compare(object? left, object? right)
    {
        return OrderChecker.InOrder(left!, right!) switch {
            OrderResult.RIGHT_ORDER => -1,
            OrderResult.WRONG_ORDER => 1,
            OrderResult.CONTINUE => 0
        };
    }
}