using System.Collections.Immutable;

public record Monkey(
    int Id, 
    ImmutableArray<long> Items, 
    IElement WorryOperation, 
    IElement NextMonkey,
    int InspectionCount = 0) {

    public override string ToString()
    {
        var parts = new [] {
            $"{nameof(Id)} = {Id}",
            $"{nameof(Items)} = [{string.Join(',', Items)}]",
            $"{nameof(WorryOperation)} = {WorryOperation}",
            $"{nameof(NextMonkey)} = {NextMonkey}",
            $"{nameof(InspectionCount)} = {InspectionCount}"
        };

        return $"{nameof(Monkey)} {{ {string.Join(", ", parts)} }}";
    }
}