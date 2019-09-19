using NumericalSequences;

namespace Patterns
{
    public class PermutationFactoryExternal : PatternFactoryExternal, IPermutationFactoryExternal
    {
        private INumSequenceBasicFactory numSequenceBasicFactory;
        private IPatternFactory<Permutation> permutationFactory;
        
        public Permutation CreatePermutation(ulong[] words, int length, byte letterSize)
        {
            NumSequenceBasic numSequenceBasic = numSequenceBasicFactory.GetNumSequence(words, letterSize, length);
            return permutationFactory.GetPattern(numSequenceBasic, 0, 
                                                length - 1, length - 1);
        }
        
        
        public Permutation CreatePermutation(ulong[] words, int length, byte letterSize, int suffixLength)
        {
            NumSequenceBasic numSequenceBasic =
                numSequenceBasicFactory.GetNumSequence(words, letterSize, 
                    length, suffixLength);

            return permutationFactory.GetPattern(numSequenceBasic, 0,
                length - 1, length - 1);
        }
        

        public Permutation CreatePermutation(ulong[] words, int length, byte letterSize,
                            IPermutationBuilderAdvancedAttributes attributes)
        {
            if (attributes.SuffixLengthSet)
                return CreatePermutation(words, length, letterSize, attributes.SuffixLength);
            else
                return CreatePermutation(words, length, letterSize);
        }

        public Permutation CreatePermutation(string[] letters, byte letterSize, 
                            IPermutationBuilderAdvancedAttributes attributes)
        {
            if (attributes.SuffixLengthSet)
                return CreatePermutation(letters, letterSize, attributes.SuffixLength);
            else
                return CreatePermutation(letters, letterSize);
        }
        
        public Permutation CreatePermutation(string[] letters, byte letterSize)
        {
            NumSequenceBasic numSequenceBasic = numSequenceBasicFactory.
                GetNumSequence(new ulong[]{0}, letterSize, 0);
            FillNumSequenceLetters(numSequenceBasic, letters);
            return permutationFactory.GetPattern(numSequenceBasic, 0,
                letters.Length - 1, letters.Length - 1);
        }
        
        public Permutation CreatePermutation(string[] letters, byte letterSize, int suffixLength)
        {
            NumSequenceBasic numSequenceBasic = numSequenceBasicFactory.
                GetNumSequence(new ulong[] {0},letterSize, 0, suffixLength);
            FillNumSequenceLetters(numSequenceBasic, letters);
            return permutationFactory.GetPattern(numSequenceBasic, 0,
                letters.Length - 1, letters.Length - 1);
        }
        
        
        
        public PermutationFactoryExternal()
        {
            numSequenceBasicFactory = new NumSequenceBasicFactory();
            permutationFactory = new Permutation.PermutationFactory();
        }

        public PermutationFactoryExternal(INumSequenceBasicFactory numSequenceBasicFactory,
            IPatternFactory<Permutation> permutationFactory)
        {
            this.numSequenceBasicFactory = numSequenceBasicFactory;
            this.permutationFactory = permutationFactory;
        }
    }
}