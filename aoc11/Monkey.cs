using System.Collections.Immutable;

public record Monkey(
    int Id, 
    ImmutableArray<long> Items, 
    IElement WorryOperation, 
    long Divisor,
    int nextMonkeyIfDivisible,
    int nextMonkeyIfNotDivisible,
    long InspectionCount = 0) {

    public override string ToString()
    {
        var parts = new [] {
            $"{nameof(Id)} = {Id}",
            $"{nameof(Items)} = [{string.Join(',', Items)}]",
            $"{nameof(WorryOperation)} = {WorryOperation}",
            $"{nameof(Divisor)} = {Divisor}",
            $"{nameof(nextMonkeyIfDivisible)} = {nextMonkeyIfDivisible}",
            $"{nameof(nextMonkeyIfNotDivisible)} = {nextMonkeyIfNotDivisible}",
            $"{nameof(InspectionCount)} = {InspectionCount}"
        };

        return $"{nameof(Monkey)} {{ {string.Join(", ", parts)} }}";
    }
}