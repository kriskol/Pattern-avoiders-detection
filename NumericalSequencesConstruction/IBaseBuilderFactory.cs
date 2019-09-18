namespace NumericalSequences
{
    public interface IBaseBuilderFactory
    {
        void SetLetterSize(byte letterSize);
        void SetLength(int length);
        void SetSuffixLength(int suffixLength);
        void SetSuffixLength();
    }
}