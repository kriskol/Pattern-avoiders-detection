using NumberOperationsInterfaces;

namespace NumericalSequences
{
    public class NumSequenceBasicFactory : NumSequenceFactory, INumSequenceBasicFactory
    {
        protected IBasicWsBuilderFactory GetBuilderFactory()
        {
            return new NumSequenceBasicWsBuilder.BasicWsBuilderFactory();
        }

        private void SetBaseAttributes(ulong[] words, byte letterSize, 
                                    int length, IBasicWsBuilderFactory builderFactory)
        {
            builderFactory.SetWords(words);
            builderFactory.SetLetterSize(letterSize);
            builderFactory.SetLength(length);
        }
        private NumSequenceBasic GetNumSequence(IBasicWsBuilderFactory builderFactory)
        {
            INumSequenceBasicWsBuilder builder;
            builderFactory.TryGetBuilder(out builder);
            Reset();
            return new NumSequenceBasicWs(builder);
        }
        
        public NumSequenceBasic GetNumSequenceDefault(byte letterSize, int length, 
                                                    bool set)
        {
            IBasicWsBuilderFactory builderFactory = GetBuilderFactory();
            SetBaseAttributes(CreteDefaultWords(letterSize, length, set), letterSize, length, builderFactory);
            builderFactory.SetSuffixLength();
            builderFactory.SetMaximalLength();
            return GetNumSequence(builderFactory);
        }

        public NumSequenceBasic GetNumSequence(ulong[] words, byte letterSize, 
                                                int length)
        {
            IBasicWsBuilderFactory builderFactory = GetBuilderFactory();
            SetBaseAttributes(words, letterSize, length, builderFactory);
            builderFactory.SetSuffixLength();
            builderFactory.SetMaximalLength();
            return GetNumSequence(builderFactory);
        }

        public NumSequenceBasic GetNumSequence(ulong[] words, byte letterSize, int length, 
                                                int suffixLength)
        {
            IBasicWsBuilderFactory builderFactory = GetBuilderFactory();
            SetBaseAttributes(words, letterSize, length, builderFactory);
            builderFactory.SetSuffixLength(suffixLength);
            builderFactory.SetMaximalLength();
            return GetNumSequence(builderFactory);
        }

        public NumSequenceBasic GetNumSequence(ulong[] words, byte letterSize, int length, 
                                                int maximalLength, int countBitsFromStart)
        {
            IBasicWsBuilderFactory builderFactory = GetBuilderFactory();
            SetBaseAttributes(words, letterSize, length, builderFactory);
            builderFactory.SetSuffixLength();
            builderFactory.SetMaximalLength(maximalLength, countBitsFromStart);
            return GetNumSequence(builderFactory);
        }

        public NumSequenceBasic GetNumSequence(ulong[] words, byte letterSize, int length, 
                                                int suffixLength, int maximalLength, int countBitsFromStart)
        {
            IBasicWsBuilderFactory builderFactory = GetBuilderFactory();
            SetBaseAttributes(words, letterSize, length, builderFactory);
            builderFactory.SetSuffixLength(suffixLength);
            builderFactory.SetMaximalLength(maximalLength, countBitsFromStart);
            return GetNumSequence(builderFactory);
        }

        public NumSequenceBasicFactory()
        {
            Reset();
        }

        public void Reset()
        {
        }
    }
}