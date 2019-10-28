using ExtensionMaps;
using GeneralInterfaces;
using NumericalSequences;
using Patterns;
using PermutationsCollections;

namespace ExtensionMapsComputations
{
    public class MinimumLettersChecked : IMinimumLettersChecked
    {
        private IPermutationsCollection[] permutationsUpfixes;
        private static IPermutationBuilderExternal permutationBuilder;
        private static NumSequenceExtendedFactory numSeqExtFactory;
        
        private void ComputeUpfixes(Permutation permutation, IPosition positions)
        {

            IPermutationBuilderExternal permutationBuilderExternal = permutationBuilder.Clone();
                
            Permutation permutationUpfix = permutationBuilderExternal.CreatePattern(new ulong[] {0},
                                            permutation.LetterSize, 0);
            
            int length = permutation.Length;

            NumSequenceExtended numSequenceExtended =
                numSeqExtFactory.GetNumSequenceDefault(1, length, false);

            int index;
            int popCount;
            
            for (int i = 0; i < length-1; i++)
            {
                index = positions.GetPosition(length - i - 2);
                numSequenceExtended.SetLetterMutable(index, 1);
                popCount = numSequenceExtended.PopCount(index);
                permutationUpfix = permutationUpfix.InsertPosition(i+1-popCount);
                permutationsUpfixes[i].Add(permutationUpfix);
            }
        }
        
        public int GetMinimumLettersConsidered(IPermutationDictionary<ExtensionMap> permExMaps, 
                                                Permutation permutation, 
                                                IPosition positions, int maximumLettersConsidered)
        {
            IPermutationBuilderExternal permutationBuilderExternal = permutationBuilder.Clone();

            int length = permutation.Length;
            NumSequenceExtended numSequenceExtended =
                numSeqExtFactory.GetNumSequenceDefault(1, length, false);

            Permutation permutationUpfix = permutationBuilderExternal.CreatePattern(new ulong[] {0},
                                                                    permutation.LetterSize, 0);
            
            int minimumLettersConsidered = 0;
            int index;
            int popCount;
            
            for (int i = 0; i < maximumLettersConsidered; i++)
            {
                minimumLettersConsidered++;
                index = positions.GetPosition(length - i - 1);
                numSequenceExtended.SetLetterMutable(index, 1);
                popCount = numSequenceExtended.PopCount(index);
                permutationUpfix = permutationUpfix.InsertPosition(i + 1 - popCount);
                if(!permutationsUpfixes[i].Contains(permutationUpfix))
                    break;
            }

            return minimumLettersConsidered;
        }

        public MinimumLettersChecked(IPermutationsCollection permutationsAvoided)
        {
            permutationsUpfixes = new IPermutationsCollection[permutationsAvoided.LengthLongestPermutation];

            for (int i = 0; i < permutationsAvoided.LengthLongestPermutation; i++)
            {
                permutationsUpfixes[i] = new PermutationCollection();
            }
            
            foreach (var permutation in permutationsAvoided)
            {
                ComputeUpfixes(permutation, permutation.ComputeInversion());
            }
        }

        static MinimumLettersChecked()
        {

            permutationBuilder = new PermutationBuilderExternal();
            numSeqExtFactory = new NumSequenceExtendedFactory();
        }
    }
}