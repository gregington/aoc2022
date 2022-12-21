using System.Collections.Immutable;

public interface IExpression {
    long Evaluate(ImmutableDictionary<string, long> variables);

}