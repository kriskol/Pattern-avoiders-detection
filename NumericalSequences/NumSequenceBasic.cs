namespace NumericalSequences
{
    public abstract class NumSequenceBasic : NumSequenceBaseIm<NumSequenceBasic>
    {
        public class NumSequenceBasicFactory : INumSequenceFactory<NumSequenceBasic>
        {
            public NumSequenceBasic GetNumSequenceDefault(byte letterSize, int length, bool set)
            {
                throw new System.NotImplementedException();
            }

            public NumSequenceBasic GetNumSequence(ulong[] words, byte letterSize, int length)
            {
                throw new System.NotImplementedException();
            }

            public NumSequenceBasic GetNumSequence(ulong[] words, byte letterSize, int length, int maximalLength)
            {
                
            }

            public NumSequenceBasic GetNumSequence(ulong[] words, byte letterSize, int length, int maximalLength, int suffixLength)
            {
            }
        }

        public static INumSequenceFactory<NumSequenceBasic> GetNumSequenceFactory()
        {
            return numSequenceFactory;
        }

        static NumSequenceBasic()
        {
            numSequenceFactory = new NumSequenceBasicFactory();
        }
    }
}