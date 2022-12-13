using System.Text;

public static class ListExtensions {
    public static String ToStringAoc(this List<object> list) {
        var contents = list.Select(x => x is int ? Convert.ToString(x) : ToStringAoc(x as List<object>));
        return $"[{string.Join(",", contents)}]";
    }
}