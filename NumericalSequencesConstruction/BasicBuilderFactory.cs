namespace NumericalSequences
{
    public abstract class BasicBuilderFactory : BaseBuilderFactory, IBasicBuilderFactory
    {
        protected bool maximalLengthS;

        protected virtual void MaximalLengthSet()
        {
            maximalLengthS = true;
        }

        public abstract void SetMaximalLength(int maximalLength, int countBlockedBitsFromStart);
        public abstract void SetMaximalLength();
        
        public override void Reset()
        {
            base.Reset();
            maximalLengthS = false;
        }
    }
}