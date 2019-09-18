namespace NumericalSequences
{
    public interface INumSequenceBasicBuilder : INumSequenceBaseBuilder
    {
        int MaximalLength { get; }
        int CountBlockedBitsFromStart { get; }
        bool MaximalLengthSet { get; }
    }
}