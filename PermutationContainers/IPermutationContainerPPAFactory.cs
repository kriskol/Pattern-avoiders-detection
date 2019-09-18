using ExtensionMaps;
using Patterns;

namespace PermutationContainers
{
    public interface IPermutationContainerPPAFactory
    {
        PermutationContainerPPA CreatePermutation(Permutation permutation, PatternBasic positions,
                                                ExtensionMap extensionMap);
    }
}