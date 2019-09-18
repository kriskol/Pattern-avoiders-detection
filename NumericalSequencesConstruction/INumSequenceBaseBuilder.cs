namespace NumericalSequences
{
    public interface INumSequenceBaseBuilder
    {
        byte LetterSize { get; }
        int Length { get; }
        int SuffixLength { get; }
        bool SuffixLengthSet { get; }
    }
}