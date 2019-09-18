using NumericalSequences;

namespace Patterns
{
    public class PermutationFactoryExternal : PatternFactoryExternal, IPermutationFactoryExternal
    {
        private NumSequenceBasicFactory numSequenceBasicFactory;
        private IPatternFactory<Permutation> permutationFactory;
        
        public Permutation CreatePermutation(ulong[] words, int length, int letterSize)
        {
            NumSequenceBasic numSequenceBasic = numSequenceBasicFactory.GetNumSequence(words, letterSize, length);
            permutationFactory.GetPattern(numSequenceBasic, 0, length - 1, length - 1);
        }

        public Permutation CreatePermutation(string[] letters, int letterSize)
        {
            
        }
    }
}