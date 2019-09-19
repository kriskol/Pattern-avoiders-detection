using NumberOperationsInterfaces;

namespace NumericalSequences
{
    public abstract class ExtendedBuilderFactory : BaseBuilderFactory, IExtendedBuilderFactory
    {
        protected bool ctzComputeS;
        protected bool popCountComputeS;

        protected virtual void CtzComputeSet()
        {
            ctzComputeS = true;
        }

        protected virtual void PopCountComputeSet()
        {
            popCountComputeS = true;
        }

        public abstract void SetCtzCompute(ICtzCompute ctzCompute);
        
        public abstract void SetPopCountCompute(IPopCountCompute popCountCompute);

        public override void Reset()
        {
            base.Reset();
            ctzComputeS = false;
            popCountComputeS = false;
        }
    }
}