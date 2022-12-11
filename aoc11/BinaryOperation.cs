public abstract class BinaryOperation : IElement {

    private readonly IElement _left;
    private readonly IElement _right;

    public BinaryOperation(IElement left, IElement right) {
        _left = left;
        _right = right;
    }

    public IElement Left { get => _left; }
    public IElement Right { get => _right; }

    public abstract long Evaluate(IReadOnlyDictionary<string, long> variables);

    public override abstract string ToString();
}
