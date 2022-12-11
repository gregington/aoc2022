public sealed class If : IElement {
    
    private readonly IElement _condition;
    private readonly IElement _trueExpression;
    private readonly IElement _falseExpression;
    
    public If(IElement condition, IElement trueExpression, IElement falseExpression) {
        _condition = condition;
        _trueExpression = trueExpression;
        _falseExpression = falseExpression;
    }

    public IElement Condition { get => _condition; }
    public IElement TrueExpression { get => _trueExpression; }
    public IElement FalseExpression { get => _falseExpression; }

    public long Evaluate(IReadOnlyDictionary<string, long> variables) {
        return Condition.Evaluate(variables) != 0 ? TrueExpression.Evaluate(variables) : FalseExpression.Evaluate(variables);
    }

    public override string ToString() {
        return $"{Condition.ToString()} ? {TrueExpression.ToString()} : {FalseExpression.ToString()}";
    }
}