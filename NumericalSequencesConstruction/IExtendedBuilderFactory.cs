using NumberOperationsInterfaces;

namespace NumericalSequences
{
    public interface IExtendedBuilderFactory : IBaseBuilderFactory
    {
        void SetCtzCompute(ICtzCompute ctzCompute);
        void SetPopCountCompute(IPopCountCompute popCountCompute);
    }
}