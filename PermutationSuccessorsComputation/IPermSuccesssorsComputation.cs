using ExtensionMaps;
using Patterns;
using PermutationsCollections;

namespace PermutationSuccessorsComputation
{
    public interface IPermSuccessorsComputation
    {
        IPermutationsCollection Successors(Permutation permutation, ExtensionMap extensionMaps,
                                            IPermutationsCollection avoidedPermutations);
    }
}