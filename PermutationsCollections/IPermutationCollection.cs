using System.Collections.Generic;
using GeneralCollectionsInterfaces;
using Patterns;

namespace PermutationsCollections
{
    public interface IPermutationsCollection : ICollection<Permutation>
    {
        int LengthLongestPermutation { get; }
    }
}