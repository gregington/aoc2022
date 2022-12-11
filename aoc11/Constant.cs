public sealed class Constant : IElement {
    
    private long _value;

    public Constant(long value) {
        _value = value;
    }

    public long Value { get => _value; }

    public long Evaluate(IReadOnlyDictionary<string, long> variables) {
        return Value;
    }

    public override string ToString() {
        return Value.ToString();
    }
}