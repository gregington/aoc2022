using System.Collections.Immutable;

public record struct Equal : IOperation {

    private IExpression left;
    private IExpression right;

    public Equal(IExpression left, IExpression right) {
        this.left = left;
        this.right = right;
    }

    public IExpression Left { get => left; }
    public IExpression Right { get => right; }

    public long Evaluate(ImmutableDictionary<string, long> variables) {
        return right.Evaluate(variables) - left.Evaluate(variables);
    }

    public override string ToString() {
        return $"({left} = {right})";
    }
}