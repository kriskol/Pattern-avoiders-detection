using System.Linq;
using System.Resources;
using NumberOperationsImplementations;
using NumberOperationsInterfaces;

namespace NumericalSequences
{
    public class NumSequenceExtendedFactory : NumSequenceFactory, INumSequenceExtendedFactory
    {
        private ICtzCompute ctzCompute;
        private IPopCountCompute popCountCompute;
        private NumSequenceExtendedWsBuilder.ExtendedWsBuilderFactory builderFactory;
        private INumSequenceExtendedWsBuilder builder;
        
        protected void SetBaseAttributes(ulong[] words, byte letterSize, int length)
        {
            builderFactory.SetWords(words);
            builderFactory.SetLetterSize(letterSize);
            builderFactory.SetLength(length);
        }
        private void SetCtzPopCountCompute()
        {
            SetCtzPopCountCompute(ctzCompute, popCountCompute);
        }
        private void SetCtzPopCountCompute(ICtzCompute ctzCompute, IPopCountCompute popCountCompute)
        {
            builderFactory.SetCtzCompute(ctzCompute);
            builderFactory.SetPopCountCompute(popCountCompute);
        }
        private NumSequenceExtended GetNumSequence()
        {
            builderFactory.TryGetBuilder(ref builder);
            return new NumSequenceExtendedWs(builder);
        }
        
        public NumSequenceExtended GetNumSequenceDefault(byte letterSize, int length, bool set)
        {
            SetBaseAttributes(CreteDefaultWords(letterSize, length, set), letterSize, length);
            builderFactory.SetSuffixLength();
            SetCtzPopCountCompute();

            return GetNumSequence();
        }

        public NumSequenceExtended GetNumSequence(ulong[] words, byte letterSize, int length)
        {
            SetBaseAttributes(words, letterSize, length);
            SetCtzPopCountCompute();
            builderFactory.SetSuffixLength();
            return GetNumSequence();
        }

        public NumSequenceExtended GetNumSequence(ulong[] words, byte letterSize, int length, int suffixLength)
        {
            SetBaseAttributes(words, letterSize, length);
            builderFactory.SetSuffixLength(suffixLength);
            SetCtzPopCountCompute();
            return GetNumSequence();
        }

        public NumSequenceExtended GetNumSequence(ulong[] words, byte letterSize, int length, 
                                                    ICtzCompute ctzCompute, IPopCountCompute popCountCompute)
        {
            SetBaseAttributes(words, letterSize, length);
            SetCtzPopCountCompute(ctzCompute, popCountCompute);
            builderFactory.SetSuffixLength();
            return GetNumSequence();
        }
        public NumSequenceExtended GetNumSequence(ulong[] words, byte letterSize, int length, int suffixLength, 
                                                    ICtzCompute ctzCompute, IPopCountCompute popCountCompute)
        {
            SetBaseAttributes(words, letterSize, length);
            SetCtzPopCountCompute(ctzCompute, popCountCompute);
            builderFactory.SetSuffixLength(suffixLength);
            return GetNumSequence();
        }

        public void Reset()
        {
            builderFactory = new NumSequenceExtendedWsBuilder.ExtendedWsBuilderFactory();
        }
        
        public NumSequenceExtendedFactory()
        {
            ctzCompute = NumberOperationsDefaultFactory.GetCtzCompute();
            popCountCompute = NumberOperationsDefaultFactory.GetPopCountCompute();
            Reset();
        }
        public NumSequenceExtendedFactory(ICtzCompute ctzCompute, IPopCountCompute popCountCompute)
        {
            this.ctzCompute = ctzCompute;
            this.popCountCompute = popCountCompute;
            Reset();
        }
    }
}