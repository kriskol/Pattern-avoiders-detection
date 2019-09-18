using NumberOperationsInterfaces;

namespace NumericalSequences
{
    public interface INumSequenceExtendedBuilder : INumSequenceBaseBuilder
    {
        ICtzCompute GetCtzCompute();
        IPopCountCompute GetPopCountCompute();
    }
}