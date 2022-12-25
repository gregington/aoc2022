using System.Text;

public static class SnafuNumber {

    public static long SnafuToInt(string snafuValue) {
        var chars = snafuValue.ToCharArray().Reverse().ToArray();

        return chars
            .Select((v, i) => ((long) Math.Pow(5, i)) * (v switch {
                '2' => 2,
                '1' => 1,
                '0' => 0,
                '-' => -1,
                '=' => -2,
                _ => throw new Exception($"Unexpected numeral {v}")
            }))
            .Sum();
    }

    public static string IntToSnafu(long intValue) {
        var remaining = intValue;
        var sb = new StringBuilder();

        var place = 0;
        while (remaining > 0) {
            var placeValue = (long) Math.Pow(5, place);
            var digit = (remaining / placeValue % 5);
            if (digit >= 0 && digit <= 2) {
                remaining -= (digit * placeValue);
                sb.Append(digit);
            } else {
                var borrow = (int) Math.Pow(5, place + 1);
                digit = -1 * (5 - digit);
                if (digit == -2) {
                    sb.Append('=');
                } else if (digit == -1) {
                    sb.Append('-');
                } else {
                    throw new Exception($"Unexpected. intValue = {intValue}");
                }
                remaining -= digit * placeValue;
            }
            place++;
        } 
        return new String(sb.ToString().Reverse().ToArray());
    }

}