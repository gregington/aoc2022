public sealed class Equals : BinaryOperation {

    public Equals(IElement left, IElement right) : base(left, right) {
    }

    public override long Evaluate(IReadOnlyDictionary<string, long> variables)
    {
        return Left.Evaluate(variables) == Right.Evaluate(variables) ? 1 : 0;
    }

    public override string ToString()
    {
        return $"{Left.ToString()} == {Right.ToString()}";
    }
}