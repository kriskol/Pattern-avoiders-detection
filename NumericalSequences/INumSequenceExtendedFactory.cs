using NumberOperationsInterfaces;

namespace NumericalSequences
{
    public interface INumSequenceExtendedFactory : INumSequenceFactory<NumSequenceExtended>
    {
        NumSequenceExtended GetNumSequence(ulong[] words, byte letterSize, 
                                        int length, ICtzCompute ctzCompute, 
                                        IPopCountCompute popCountCompute);
        NumSequenceExtended GetNumSequence(ulong[] words, byte letterSize, 
                                        int length, int suffixLength ,ICtzCompute ctzCompute, 
                                        IPopCountCompute popCountCompute);
    }
}