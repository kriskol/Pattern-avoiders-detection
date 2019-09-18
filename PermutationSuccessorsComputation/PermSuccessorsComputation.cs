using ExtensionMaps;
using Patterns;
using PermutationsCollections;

namespace PermutationSuccessorsComputation
{
    public class PermSuccessorsComputation : IPermSuccessorsComputation
    {
        private IPermutationsCollection CreatePermutationsChecked(Permutation permutation, ExtensionMap extensionMap,
            IPermutationsCollection avoidedPermutations)
        {
            PermutationCollection permutationCollection = new PermutationCollection();
            Permutation newPermutation;
            
            foreach (var position in extensionMap.Ctz())
            {
                newPermutation = permutation.InsertPosition(position);
                if(!avoidedPermutations.Contains(newPermutation))
                    permutationCollection.Add(newPermutation);
            }

            return permutationCollection;
        }

        private IPermutationsCollection CreatePermutationsUnChecked(Permutation permutation, ExtensionMap extensionMap)
        {
            PermutationCollection permutationCollection = new PermutationCollection();
            
            foreach (var position in extensionMap.Ctz())
                permutationCollection.Add(permutation.InsertPosition(position));

            return permutationCollection;
        }
        
        public IPermutationsCollection Successors(Permutation permutation, ExtensionMap extensionMap,
            IPermutationsCollection avoidedPermutations)
        {
            if (permutation.Length < avoidedPermutations.LengthLongestPermutation)
                return CreatePermutationsChecked(permutation, extensionMap, avoidedPermutations);
            else
                return CreatePermutationsUnChecked(permutation, extensionMap);
        }
    }
}