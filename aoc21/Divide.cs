public record struct Divide : IOperation {

    private IExpression left;
    private IExpression right;

    public Divide(IExpression left, IExpression right) {
        this.left = left;
        this.right = right;
    }

    public IExpression Left { get => left; }
    public IExpression Right { get => right; }

    public long Evaluate() {
        return left.Evaluate() / right.Evaluate();
    }

    public override string ToString() {
        return $"({left} / {right})";
    }
}