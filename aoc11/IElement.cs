public interface IElement {
    long Evaluate(IReadOnlyDictionary<string, long> variables);
}