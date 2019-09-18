namespace NumericalSequences
{
    public interface INumSequenceBasicWsBuilder : INumSequenceBasicBuilder
    {
        ulong[] Words { get; }
    }
}