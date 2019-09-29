namespace NumericalSequences
{
    public abstract class BasicBuilderFactory : BaseBuilderFactory, IBasicBuilderFactory
    {
        protected bool maximalLengthSet;

        protected virtual void MaximalLengthSet()
        {
            maximalLengthSet = true;
        }

        public abstract void SetMaximalLength(int maximalLength, int countBlockedBitsFromStart);
        public abstract void SetMaximalLength();
        
        public void SetDefaultMaximalLength()
        {
            SetMaximalLength();
        }

        public override void Reset()
        {
            base.Reset();
            SetDefaultMaximalLength();
        }
    }
}