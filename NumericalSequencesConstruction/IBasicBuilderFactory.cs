namespace NumericalSequences
{
    public interface IBasicBuilderFactory : IBaseBuilderFactory
    {
        void SetMaximalLength(int maximalLength, int countBlockedBitsFromStart);
        void SetMaximalLength(); }
}