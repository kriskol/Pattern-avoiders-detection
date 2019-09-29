using NumberOperationsImplementations;
using NumberOperationsInterfaces;

namespace NumericalSequences
{
    public abstract class ExtendedBuilderFactory : BaseBuilderFactory, IExtendedBuilderFactory
    {
        protected bool ctzComputeSet;
        protected bool popCountComputeSet;

        protected virtual void CtzComputeSet()
        {
            ctzComputeSet = true;
        }

        protected virtual void PopCountComputeSet()
        {
            popCountComputeSet = true;
        }

        public abstract void SetCtzCompute(ICtzCompute ctzCompute);
        
        public abstract void SetPopCountCompute(IPopCountCompute popCountCompute);

        public void SetDefaultCtzPopCountCompute()
        {
            SetCtzCompute(NumberOperationsDefaultFactory.GetCtzCompute());
            SetPopCountCompute(NumberOperationsDefaultFactory.GetPopCountCompute());
        }

        public override void Reset()
        {
            base.Reset();
            SetDefaultCtzPopCountCompute();
        }
    }
}