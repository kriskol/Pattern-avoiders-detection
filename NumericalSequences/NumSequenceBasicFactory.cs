using NumberOperationsInterfaces;

namespace NumericalSequences
{
    public class NumSequenceBasicFactory : NumSequenceFactory, INumSequenceBasicFactory
    {
        private NumSequenceBasicWsBuilder.BasicWsBuilderFactory builderFactory;
        private INumSequenceBasicWsBuilder builder;
        
        private void SetBaseAttributes(ulong[] words, byte letterSize, int length)
        {
            builderFactory.SetWords(words);
            builderFactory.SetLetterSize(letterSize);
            builderFactory.SetLength(length);
        }
        private NumSequenceBasic GetNumSequence()
        {
            builderFactory.TryGetBuilder(out builder);
            Reset();
            return new NumSequenceBasicWs(builder);
        }
        
        public NumSequenceBasic GetNumSequenceDefault(byte letterSize, int length, bool set)
        {
            SetBaseAttributes(CreteDefaultWords(letterSize, length, set), letterSize, length);
            builderFactory.SetSuffixLength();
            builderFactory.SetMaximalLength();
            return GetNumSequence();
        }

        public NumSequenceBasic GetNumSequence(ulong[] words, byte letterSize, int length)
        {
            SetBaseAttributes(words, letterSize, length);
            builderFactory.SetSuffixLength();
            builderFactory.SetMaximalLength();
            return GetNumSequence();
        }

        public NumSequenceBasic GetNumSequence(ulong[] words, byte letterSize, int length, int suffixLength)
        {
            SetBaseAttributes(words, letterSize, length);
            builderFactory.SetSuffixLength(suffixLength);
            builderFactory.SetMaximalLength();
            return GetNumSequence();
        }

        public NumSequenceBasic GetNumSequence(ulong[] words, byte letterSize, int length, 
                                                int maximalLength, int countBitsFromStart)
        {
            SetBaseAttributes(words, letterSize, length);
            builderFactory.SetSuffixLength();
            builderFactory.SetMaximalLength(maximalLength, countBitsFromStart);
            return GetNumSequence();
        }

        public NumSequenceBasic GetNumSequence(ulong[] words, byte letterSize, int length, 
                                                int suffixLength, int maximalLength, int countBitsFromStart)
        {
            SetBaseAttributes(words, letterSize, length);
            builderFactory.SetSuffixLength(suffixLength);
            builderFactory.SetMaximalLength(maximalLength, countBitsFromStart);
            return GetNumSequence();
        }

        public NumSequenceBasicFactory()
        {
            Reset();
        }

        public void Reset()
        {
            builderFactory = new NumSequenceBasicWsBuilder.BasicWsBuilderFactory();
        }
    }
}