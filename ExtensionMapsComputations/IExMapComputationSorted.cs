using ExtensionMaps;
using GeneralInterfaces;
using Patterns;
using PermutationsCollections;

namespace ExtensionMapsComputations
{
    public interface IExMapComputationSorted
    {
        ExtensionMap Compute(IPermutationDictionary<ExtensionMap>[] permExMaps, Permutation permutation,
                                IPosition positions, int letterSortedByPosition , int lettersConsidered);
    }
}