using NumberOperationsInterfaces;

namespace NumericalSequences
{
    public class NumSequenceExtendedWsBuilder : NumSequenceExtendedBuilder, INumSequenceExtendedWsBuilder
    {
        private ulong[] words;

        public ulong[] Words
        {
            get { return words; }
            protected set { words = value; }
        }

        public NumSequenceExtendedWsBuilder(byte letterSize, int length, int suffixLength,
                                            bool suffixLengthSet, ICtzCompute ctzCompute,
                                            IPopCountCompute popCountCompute, ulong[] words)
                                            : base(letterSize, length, suffixLength, 
                                                    suffixLengthSet, ctzCompute, popCountCompute)
        {
            this.words = words;
        }

        protected NumSequenceExtendedWsBuilder()
        {
        }
        
        public sealed class ExtendedWsBuilderFactory : ExtendedBuilderFactory, IExtendedWsBuilderFactory
        {
            protected bool wordsS;
            private NumSequenceExtendedWsBuilder builder;

            protected void WordsSet()
            {
                wordsS = true;
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

            public override void SetCtzCompute(ICtzCompute ctzCompute)
            {
                CtzComputeSet();
                builder.SetCtzCompute(ctzCompute);
            }

            public override void SetPopCountCompute(IPopCountCompute popCountCompute)
            {
                PopCountComputeSet();
                builder.SetPopCountCompute(popCountCompute);
            }

            public void SetWords(ulong[] words)
            {
                WordsSet();
                builder.Words = words;
            }

            public bool TryGetBuilder(out INumSequenceExtendedWsBuilder builder)
            {
                if (letterSizeS && lengthS && suffixLengthS
                    && ctzComputeS && popCountComputeS && wordsS)
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

            public override void Reset()
            {
                base.Reset();
                builder = new NumSequenceExtendedWsBuilder();
                wordsS = false;
            }
        }
    }
}