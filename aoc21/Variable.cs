using System.Collections.Immutable;

public class Variable : IExpression {
    private string name;
    
    public Variable(string name) {
        this.name = name;
    }

    public string Name => name;
    public long Evaluate(ImmutableDictionary<string, long> variables) => variables[name];

    public override string ToString() => name;

}