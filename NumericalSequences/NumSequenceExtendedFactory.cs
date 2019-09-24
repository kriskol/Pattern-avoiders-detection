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

        protected virtual IExtendedWsBuilderFactory GetBuilderFactory()
        {
            return new NumSequenceExtendedWsBuilder.ExtendedWsBuilderFactory();
        }
        
        protected void SetBaseAttributes(ulong[] words, byte letterSize, 
                                        int length, IExtendedWsBuilderFactory builderFactory)
        {
            builderFactory.SetWords(words);
            builderFactory.SetLetterSize(letterSize);
            builderFactory.SetLength(length);
        }
        private void SetCtzPopCountCompute(IExtendedWsBuilderFactory builderFactory)
        {
            SetCtzPopCountCompute(ctzCompute, popCountCompute, builderFactory);
        }
        private void SetCtzPopCountCompute(ICtzCompute ctzCompute, IPopCountCompute popCountCompute,
                                                IExtendedWsBuilderFactory builderFactory)
        {
            builderFactory.SetCtzCompute(ctzCompute);
            builderFactory.SetPopCountCompute(popCountCompute);
        }
        private NumSequenceExtended GetNumSequence(IExtendedWsBuilderFactory builderFactory)
        {
            INumSequenceExtendedWsBuilder builder;
            builderFactory.TryGetBuilder(out builder);
            Reset();
            return new NumSequenceExtendedWs(builder);
        }
        
        public NumSequenceExtended GetNumSequenceDefault(byte letterSize, int length, bool set)
        {
            IExtendedWsBuilderFactory builderFactory = GetBuilderFactory();
            SetBaseAttributes(CreteDefaultWords(letterSize, length, set), 
                                            letterSize, length, builderFactory);
            builderFactory.SetSuffixLength();
            SetCtzPopCountCompute(builderFactory);

            return GetNumSequence(builderFactory);
        }

        public NumSequenceExtended GetNumSequence(ulong[] words, byte letterSize, int length)
        {
            IExtendedWsBuilderFactory builderFactory = GetBuilderFactory();
            SetBaseAttributes(words, letterSize, length, builderFactory);
            SetCtzPopCountCompute(builderFactory);
            builderFactory.SetSuffixLength();
            return GetNumSequence(builderFactory);
        }
        
        public NumSequenceExtended GetNumSequence(ulong[] words, byte letterSize, int length, int suffixLength)
        {
            IExtendedWsBuilderFactory builderFactory = GetBuilderFactory();
            SetBaseAttributes(words, letterSize, length, builderFactory);
            builderFactory.SetSuffixLength(suffixLength);
            SetCtzPopCountCompute(builderFactory);
            return GetNumSequence(builderFactory);
        }
        
        public NumSequenceExtended GetNumSequence(ulong[] words, byte letterSize, int length, 
                                                    ICtzCompute ctzCompute, IPopCountCompute popCountCompute)
        {
            IExtendedWsBuilderFactory builderFactory = GetBuilderFactory();
            SetBaseAttributes(words, letterSize, length, builderFactory);
            SetCtzPopCountCompute(ctzCompute, popCountCompute, builderFactory);
            builderFactory.SetSuffixLength();
            return GetNumSequence(builderFactory);
        }
        public NumSequenceExtended GetNumSequence(ulong[] words, byte letterSize, int length, int suffixLength, 
                                                    ICtzCompute ctzCompute, IPopCountCompute popCountCompute)
        {
            IExtendedWsBuilderFactory builderFactory = GetBuilderFactory();
            SetBaseAttributes(words, letterSize, length, builderFactory);
            SetCtzPopCountCompute(ctzCompute, popCountCompute, builderFactory);
            builderFactory.SetSuffixLength(suffixLength);
            return GetNumSequence(builderFactory);
        }
        
        public void Reset()
        {
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