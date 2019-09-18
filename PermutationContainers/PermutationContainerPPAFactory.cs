using ExtensionMaps;
using ExtensionMapsComputations;
using Patterns;
using PermutationsCollections;

namespace PermutationContainers
{
    public class PermutationContainerPPAFactory : IPermutationContainerPPAFactory
    {
        private IPermutationsCollection avoidedPermutations;
        
        public PermutationContainerPPA CreatePermutation(Permutation permutation, PatternBasic positions, 
                                                            ExtensionMap extensionMap)
        {
            return  new PermutationContainerPPA(permutation, positions,
                                            new MinimumLettersChecked(avoidedPermutations), new ExMapComputationUnsorted(),
                                            extensionMap ,avoidedPermutations.LengthLongestPermutation);
        }

        public PermutationContainerPPAFactory(IPermutationsCollection avoidedPermutations)
        {
            this.avoidedPermutations = avoidedPermutations;
        }
    }
}