public record struct Multiply : IOperation {

    private IExpression left;
    private IExpression right;

    public Multiply(IExpression left, IExpression right) {
        this.left = left;
        this.right = right;
    }

    public IExpression Left { get => left; }
    public IExpression Right { get => right; }

    public long Evaluate() {
        return left.Evaluate() * right.Evaluate();
    }

    public override string ToString() {
        return $"({left} * {right})";
    }
}