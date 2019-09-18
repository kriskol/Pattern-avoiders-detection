namespace NumericalSequences
{
    public class NumSequenceBasicWsBuilder : NumSequenceBasicBuilder, INumSequenceBasicWsBuilder
    {
        private ulong[] words;

        public ulong[] Words
        {
            get { return words; }
            protected set { words = value; }
        }
        
        public NumSequenceBasicWsBuilder(byte letterSize, int length, int suffixLength,
                                        int maximalLength, int countBlockedBitsFromStart,
                                        bool suffixLengthSet, bool maximalLengthSet, ulong[] words) 
                                        : base(letterSize, length, suffixLength, 
                                        maximalLength, countBlockedBitsFromStart, 
                                        suffixLengthSet, maximalLengthSet)
        {
            this.words = words;
        }

        protected NumSequenceBasicWsBuilder()
        {
        }
        
        public sealed class BasicWsBuilderFactory : BasicBuilderFactory, IBasicWsBuilderFactory
        {
            protected bool wordsS;
            private NumSequenceBasicWsBuilder builder;

            protected void WordsSet()
            {
                wordsS = true;
            }

            public BasicWsBuilderFactory()
            {
                Reset();
            }

            public override void SetLetterSize(byte letterSize)
            {
                LetterSizeSet();
                builder.LetterSize = letterSize;
            }

            public override void SetLength(int length)
            {
                LengthSet();
                builder.Length = length;
            }

            public override void SetSuffixLength(int suffixLength)
            {
                SuffixLengthSet();
                builder.SuffixLength = suffixLength;
                builder.SuffixLengthSet = true;
            }

            public override void SetSuffixLength()
            {
                SuffixLengthSet();
                builder.SuffixLength = 0;
                builder.SuffixLengthSet = false;
            }

            public override void SetMaximalLength(int maximalLength, int countBlockedBitsFromStart)
            {
                MaximalLengthSet();
                builder.MaximalLength = maximalLength;
                builder.CountBlockedBitsFromStart = countBlockedBitsFromStart;
                builder.MaximalLengthSet = true;
            }

            public override void SetMaximalLength()
            {
                MaximalLengthSet();
                builder.MaximalLength = 0;
                builder.CountBlockedBitsFromStart = 0;
                builder.MaximalLengthSet = false;
            }
            

            public void SetWords(ulong[] words)
            {
                WordsSet();
                builder.Words = words;
            }

            public bool TryGetBuilder(ref INumSequenceBasicWsBuilder builder)
            {
                if (lengthS && letterSizeS && suffixLengthS 
                    && maximalLengthS && wordsS)
                {
                    builder = this.builder;
                    return true;
                }                
                else
                {
                    builder = null;
                    return false;
                }
            }

            protected override void Reset()
            {
                builder = new NumSequenceBasicWsBuilder();
                base.Reset();
                wordsS = false;
            }
        }
    }
}