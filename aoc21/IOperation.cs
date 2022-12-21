public interface IOperation : IExpression {
    
    IExpression Left { get; }
    IExpression Right { get; }

}