namespace NumericalSequences
{
    public interface IExtendedWsBuilderFactory : IExtendedBuilderFactory
    {
        void SetWords(ulong[] words);
        bool TryGetBuilder(out INumSequenceExtendedWsBuilder builder);
    }
}