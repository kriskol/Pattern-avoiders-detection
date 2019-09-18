namespace NumericalSequences
{
    public interface IExtendedWsBuilderFactory : IExtendedBuilderFactory
    {
        void SetWords(ulong[] words);
        bool TryGetBuilder(ref INumSequenceExtendedWsBuilder builder);
    }
}