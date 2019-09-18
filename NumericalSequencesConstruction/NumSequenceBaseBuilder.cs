namespace NumericalSequences
{
    public abstract class NumSequenceBaseBuilder : INumSequenceBaseBuilder
        {
            private byte letterSize;
            private int length;
            private int suffixLength;
            private bool suffixLengthSet;

            public byte LetterSize
            {
                get { return letterSize; }
                protected set { letterSize = value; }
            }
            public int Length
            {
                get { return length; }
                protected set { length = value; }
            }
            public int SuffixLength
            {
                get { return suffixLength; }
                protected set { suffixLength = value; }
            }
            public bool SuffixLengthSet
            {
                get
                {
                    return suffixLengthSet;
                }
                protected set { suffixLengthSet = value; }
            }

            public NumSequenceBaseBuilder(byte letterSize, int length, int suffixLength,
                                         bool suffixLengthSet)
            {
                this.letterSize = letterSize;
                this.length = length;
                this.suffixLength = suffixLength;
                this.suffixLengthSet = suffixLengthSet;
            }


            protected NumSequenceBaseBuilder()
            {
            }
        }
    
}