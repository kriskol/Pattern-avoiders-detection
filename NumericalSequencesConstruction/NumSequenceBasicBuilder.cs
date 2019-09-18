namespace NumericalSequences
{
    public abstract class NumSequenceBasicBuilder : NumSequenceBaseBuilder, INumSequenceBasicBuilder
    {
        private int maximalLength;
        private int countBlockedBitsFromStart;
        private bool maximalLengthSet;

        public int MaximalLength
        {
            get { return maximalLength; }
            protected set { maximalLength = value; }
        }
        public int CountBlockedBitsFromStart
        {
            get { return countBlockedBitsFromStart; }
            protected set { countBlockedBitsFromStart = value; }
        }
        public bool MaximalLengthSet
        {
            get { return maximalLengthSet; }
            protected set { maximalLengthSet = value; }
        }

        public NumSequenceBasicBuilder(byte letterSize, int length, int suffixLength,
                                        int maximalLength, int countBlockedBitsFromStart, 
                                        bool suffixLengthSet, bool maximalLengthSet) 
                                        : base(letterSize, length, suffixLength, 
                                                suffixLengthSet)
        {
            this.maximalLength = maximalLength;
            this.countBlockedBitsFromStart = countBlockedBitsFromStart;
            this.maximalLengthSet = maximalLengthSet;
        }

        protected NumSequenceBasicBuilder(){}
    }
}