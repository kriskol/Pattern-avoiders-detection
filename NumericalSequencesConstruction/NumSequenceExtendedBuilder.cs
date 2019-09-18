using NumberOperationsInterfaces;

namespace NumericalSequences
{
    public abstract class NumSequenceExtendedBuilder : NumSequenceBaseBuilder, INumSequenceExtendedBuilder
    {
        private ICtzCompute ctzCompute;
        private IPopCountCompute popCountCompute;

        public ICtzCompute GetCtzCompute()
        {
            return ctzCompute;
        }
        public IPopCountCompute GetPopCountCompute()
        {
            return popCountCompute;
        }

        protected void SetCtzCompute(ICtzCompute ctzCompute)
        {
            this.ctzCompute = ctzCompute;
        }

        protected void SetPopCountCompute(IPopCountCompute popCountCompute)
        {
            this.popCountCompute = popCountCompute;
        }
        
        public NumSequenceExtendedBuilder(byte letterSize, int length, int suffixLength,bool suffixLengthSet,
                                            ICtzCompute ctzCompute, IPopCountCompute popCountCompute) 
                                            :base(letterSize, length, suffixLength,
                                                    suffixLengthSet)
        {
            this.ctzCompute = ctzCompute;
            this.popCountCompute = popCountCompute;
        }
        
        protected NumSequenceExtendedBuilder(){}
    }
}