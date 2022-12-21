public record struct Constant : IExpression {
    
    private long value;
    
    public Constant(long value) {
        this.value = value;
    }

   public long Evaluate() => value;

    public override string ToString() => Convert.ToString(value);
}