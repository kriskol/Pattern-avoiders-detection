using NumberOperationsImplementations;
using NumberOperationsInterfaces;

namespace NumericalSequences
{
    public abstract class NumSequenceExtended:NumSequenceExtensionIm<NumSequenceExtended>
    {
        private static ICtzCompute ctzCompute;
        private static IPopCountCompute popCountCompute;
        private static readonly INumSequenceExtendedFactory numSequenceFactory;

        protected override ICtzCompute CtzCompute => ctzCompute;
        protected override IPopCountCompute PopCountCompute => popCountCompute;
        protected INumSequenceExtendedFactory NumSequenceFactory => numSequenceFactory;
        protected override void ConvertPosition(int position, out int index, out byte positionWord, out int offset)
        {
            index = (position * LetterSize ) / bitLengthWord;
            offset = ((position * LetterSize ) % bitLengthWord) % LetterSize;
            positionWord = (byte)(((((position * LetterSize ) % bitLengthWord)) 
                                   - offset) / LetterSize);
        }

        protected override NumSequenceExtended CreateNumSequenceThisProp(ulong[] words)
        {
            return CreateNumSequence(words, Length, LetterSize);
        }

        protected override NumSequenceExtended CreateNumSequence(ulong[] words, int length, byte letterSize)
        {
            if (SuffixLengthSet)
                return numSequenceFactory.GetNumSequence(words, letterSize, length, 
                                                    SuffixLength, ctzCompute, popCountCompute);

            return numSequenceFactory.GetNumSequence(words, letterSize, length, ctzCompute, popCountCompute);
        }

        protected NumSequenceExtended(INumSequenceExtendedBuilder builder) : base(builder)
        {
            ctzCompute = builder.GetCtzCompute();
            popCountCompute = builder.GetPopCountCompute();
        }
        
        
        public static INumSequenceFactory<NumSequenceExtended> GetNumSequenceFactory()
        {
            return numSequenceFactory;
        }
        static NumSequenceExtended()
        {
            numSequenceFactory = new NumSequenceExtendedFactory();
        }
    }
}