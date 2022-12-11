public sealed class Variable : IElement {
    private readonly string _name;

    public Variable(string name) {
        _name = name;
    }

    public string Name { get => _name; }

    public long Evaluate(IReadOnlyDictionary<string, long> variables) {
        return variables[Name];
    }

    public override string ToString()
    {
        return Name;
    }


}