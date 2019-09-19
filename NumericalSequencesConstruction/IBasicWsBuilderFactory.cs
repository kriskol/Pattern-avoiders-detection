namespace NumericalSequences
{
    public interface IBasicWsBuilderFactory : IBasicBuilderFactory
    {
        void SetWords(ulong[] words);
        bool TryGetBuilder(out INumSequenceBasicWsBuilder builder);
    }
}