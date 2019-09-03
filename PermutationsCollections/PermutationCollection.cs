using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Patterns;

namespace PermutationsCollections
{
    public class PermutationCollection : IPermutationsCollection
    {
        private HashSet<Permutation> hashSet;
        private bool isReadOnly = false;
        private int lengthLongestPermutation = -1;
        
        public IEnumerator<Permutation> GetEnumerator()
        {
            return hashSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Permutation item)
        {
            if (item.Length > lengthLongestPermutation)
                lengthLongestPermutation = item.Length;
            
            hashSet.Add(item);
        }

        public void Clear()
        {
            hashSet.Clear();
            lengthLongestPermutation = -1;
        }

        public bool Contains(Permutation item)
        {
            return hashSet.Contains(item);
        }

        public void CopyTo(Permutation[] array, int arrayIndex)
        {
            hashSet.CopyTo(array, arrayIndex);
        }

        public bool Remove(Permutation item)
        {
            bool result =  hashSet.Remove(item);

            int newLengthLongest = -1;
            
            if(result)
                foreach (var permutation in hashSet)
                    if (permutation.Length > newLengthLongest)
                        newLengthLongest = permutation.Length;

            lengthLongestPermutation = newLengthLongest;

            return result;
        }

        public int Count => hashSet.Count();
        public bool IsReadOnly => isReadOnly;
        public int LengthLongestPermutation => lengthLongestPermutation;
    }
}