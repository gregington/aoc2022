using System.Collections.Immutable;

public record struct Subtract : IOperation {

    private IExpression left;
    private IExpression right;

    public Subtract(IExpression left, IExpression right) {
        this.left = left;
        this.right = right;
    }

    public IExpression Left { get => left; }
    public IExpression Right { get => right; }

    public long Evaluate(ImmutableDictionary<string, long> variables) {
        return left.Evaluate(variables) - right.Evaluate(variables);
    }

    public override string ToString() {
        return $"({left} - {right})";
    }
}