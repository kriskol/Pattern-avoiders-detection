using ExtensionMaps;
using GeneralInterfaces;
using Patterns;
using PermutationsCollections;

namespace ExtensionMapsComputations
{
    public interface IMinimumLettersChecked
    {
        int GetMinimumLettersConsidered(IPermutationDictionary<ExtensionMap> permExMaps, Permutation permutation,
                                        IPosition positions, int maximumLettersConsidered);
    }
}