namespace NumericalSequences
{
    public interface INumSequenceBasicFactory : INumSequenceFactory<NumSequenceBasic>
    {
        NumSequenceBasic GetNumSequence(ulong[] words, byte letterSize, 
                                        int length, int maximalLength, 
                                        int countBitsFromStart);
        NumSequenceBasic GetNumSequence(ulong[] words, byte letterSize,
                                        int length,int suffixLength , int maximalLength,
                                        int countBitsFromStart);
    }
}