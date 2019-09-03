namespace NumericalSequences
{
    public class NumSequenceExtended:NumSequenceExtensionIm<NumSequenceExtended>
    {
        public class NumSequenceExtendedFactory: INumSequenceFactory<NumSequenceExtended>
        {
            public NumSequenceExtended GetNumSequenceDefault(byte letterSize, int length, bool set)
            {
                throw new System.NotImplementedException();
            }

            public NumSequenceExtended GetNumSequence(ulong[] words, byte letterSize, int length)
            {
                throw new System.NotImplementedException();
            }

            public NumSequenceExtended GetNumSequence(ulong[] words, byte letterSize, int length, int maximalLength)
            {
                throw new System.NotImplementedException();
            }

            public NumSequenceExtended GetNumSequence(ulong[] words, byte letterSize, int length, int maximalLength, int suffixLength)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}