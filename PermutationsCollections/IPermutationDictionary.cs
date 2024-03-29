using System.Collections.Generic;
using Patterns;

namespace PermutationsCollections
{
    public interface IPermutationDictionary<T> : IDictionary<Permutation, T>
    {
        int LengthLongestPermutation { get; }
        IPermutationsCollection GetKeys();
    }
}