namespace NumericalSequences
{
    public interface INumSequenceExtendedWsBuilder : INumSequenceExtendedBuilder
    {
        ulong[] Words { get; }
    }
}