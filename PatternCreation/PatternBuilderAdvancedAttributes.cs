namespace Patterns
{
    public class PatternBuilderAdvancedAttributes : IPatternBuilderAdvancedAttributes, 
                                                    IPatternBuilderAdvancedAttributesFactory
    {
        private bool suffixLengthSet;
        private int suffixLength;

        public bool SuffixLengthSet => suffixLengthSet;
        public int SuffixLength => suffixLengthSet ? suffixLength : -1;
        
        public void SetSuffixLength(int suffixLength)
        {
            suffixLengthSet = true;
            this.suffixLength = suffixLength;
        }

        protected virtual void Reset()
        {
            suffixLengthSet = false;
        }

        public PatternBuilderAdvancedAttributes()
        {
            Reset();
        }
    }
}