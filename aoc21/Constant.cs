using System.Collections.Immutable;

public record struct Constant : IExpression {
    
    private long value;
    
    public Constant(long value) {
        this.value = value;
    }

    public long Value => value;
   public long Evaluate(ImmutableDictionary<string, long> variables) => value;

    public override string ToString() => Convert.ToString(value);
}