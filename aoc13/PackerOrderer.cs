public static class PackerOrderer {

    public static IEnumerable<List<object>> ReorderPackets(IEnumerable<List<object>> input, IEnumerable<List<object>> dividers) {
        var x =  input.Concat(dividers);
        return x.Order(new MessageComparer())
            .Select(x => x as List<object>)!;
    }

}