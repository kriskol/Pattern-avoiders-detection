using ExtensionMaps;
using GeneralInterfaces;
using Patterns;
using PermutationsCollections;

namespace ExtensionMapsComputations
{
    public interface IExMapComputationUnsorted
    {
        ExtensionMap Compute(IPermutationDictionary<ExtensionMap> permExMaps, Permutation permutation,
                                IPosition positions, int lettersConsidered);
    }
}