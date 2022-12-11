public sealed class Multiply : BinaryOperation {
    public Multiply(IElement left, IElement right) : base(left, right) {
    }

    public override long Evaluate(IReadOnlyDictionary<string, long> variables) {
        return Left.Evaluate(variables) * Right.Evaluate(variables);
    }

    public override string ToString() {
        return $"{Left.ToString()} * {Right.ToString()}";
    }


}