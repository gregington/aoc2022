public static class OrderChecker {


    public static OrderResult InOrder(object left, object right) {
        if (left is List<object> && right is List<object>) {
            return ListsInOrder(left as List<object>, right as List<object>);
        }
        if (left is int && right is int) {
            return IntsInOrder((int) left, (int) right);
        }
        if (left is int) {
            return ListsInOrder(new List<object>{left}, right as List<object>);
        }
        return ListsInOrder(left as List<object>, new List<object>{right});
    }

    private static OrderResult ListsInOrder(List<object> left, List<object> right) {
        var leftCount = left.Count();
        var rightCount = right.Count();
        var minLength = Math.Min(leftCount, rightCount);

        for (int i = 0; i < minLength; i++) {
            var result = InOrder(left[i], right[i]);
            if (result != OrderResult.CONTINUE) {
                return result;
            }
        }

        if (leftCount < rightCount) {
            return OrderResult.RIGHT_ORDER;
        }
        
        if (leftCount > rightCount) {
            return OrderResult.WRONG_ORDER;
        }

        return OrderResult.CONTINUE;
    }

    private static OrderResult IntsInOrder(int left, int right) {
        if (left < right) {
            return OrderResult.RIGHT_ORDER;
        }
        
        if (left > right) {
            return OrderResult.WRONG_ORDER;
        }

        return OrderResult.CONTINUE;
    }
}