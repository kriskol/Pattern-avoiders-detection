using System.Collections.Generic;
using Patterns;

namespace PermutationsCollections
{
    public interface IPermutationsCollection : ICollection<Permutation>
    {
        int LengthLongestPermutation { get; }
        void UnionWith(IPermutationsCollection permutationCollection);
        void AddItems(IEnumerable<Permutation> items);
    }
}