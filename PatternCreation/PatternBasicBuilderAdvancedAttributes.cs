namespace Patterns
{
    public class PatternBasicBuilderAdvancedAttributes : PatternBuilderAdvancedAttributes, 
                                                            IPatternBasicBuilderAdvancedAttributes,
                                                            IPatternBasicBuilderAdvancedAttributesFactory
    {
        private bool maximalLengthSet;
        private int maximalLength;

        public bool MaximalLengthSet => maximalLengthSet;
        public int MaximalLength => maximalLength;
        
        public void SetMaximalLength(int maximalLength)
        {
            maximalLengthSet = true;
            this.maximalLength = maximalLength;
        }

        protected override void Reset()
        {
            base.Reset();
            maximalLengthSet = false;
        }

        public PatternBasicBuilderAdvancedAttributes()
        {
            Reset();
        }
    }
}