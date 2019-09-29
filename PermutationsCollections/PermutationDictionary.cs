using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using Patterns;

namespace PermutationsCollections
{
    public class PermutationDictionary<T> : IPermutationDictionary<T>
    {
        private Dictionary<Permutation, T> dictionary;
        private int lengthLongestPermutation = -1;
        
        public IEnumerator<KeyValuePair<Permutation, T>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<Permutation, T> item)
        {
            if (item.Key.Length > lengthLongestPermutation)
                lengthLongestPermutation = item.Key.Length;
            dictionary.Add(item.Key,item.Value);
        }

        public void Clear()
        {
            lengthLongestPermutation = -1;
            dictionary.Clear();
        }

        public bool Contains(KeyValuePair<Permutation, T> item)
        {
            return dictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<Permutation, T>[] array, int arrayIndex)
        {
            int index = 0;
            
            foreach (var item in dictionary)
            {
                array[index + arrayIndex] = item;
                index++;
            }
        }

        public bool Remove(KeyValuePair<Permutation, T> item)
        {
            bool result =  dictionary.Remove(item.Key);

            if (item.Key.Length == lengthLongestPermutation)
            {

                lengthLongestPermutation = -1;
                foreach (var key in dictionary.Keys)
                {
                    if (key.Length > lengthLongestPermutation)
                        lengthLongestPermutation = key.Length;
                }
            }

            return result;
        }

        public int Count => dictionary.Count;
        public bool IsReadOnly => false;
        public void Add(Permutation key, T value)
        {
            if (key.Length > lengthLongestPermutation)
                lengthLongestPermutation = key.Length;
            
            dictionary.Add(key,value);
        }

        public bool ContainsKey(Permutation key)
        {
            return dictionary.ContainsKey(key);
        }

        public bool Remove(Permutation key)
        {
            bool result = dictionary.Remove(key);

            if (key.Length == lengthLongestPermutation)
            {

                lengthLongestPermutation = -1;
                foreach (var permutation in dictionary.Keys)
                {
                    if (permutation.Length > lengthLongestPermutation)
                        lengthLongestPermutation = permutation.Length;
                }
            }

            return result;
        }

        public bool TryGetValue(Permutation key, out T value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        public T this[Permutation key]
        {
            get => dictionary[key];
            set
            {
                if (lengthLongestPermutation > key.Length)
                    lengthLongestPermutation = key.Length;
                
                dictionary[key] = value;
            }
        }

        public ICollection<Permutation> Keys => dictionary.Keys;
        public ICollection<T> Values => dictionary.Values;
        public int LengthLongestPermutation { get; }
        public IPermutationsCollection GetKeys()
        {
            PermutationCollection permutationCollection = new PermutationCollection();
            permutationCollection.AddItems(dictionary.Keys);

            return permutationCollection;
        }

        public PermutationDictionary()
        {
            dictionary = new Dictionary<Permutation, T>();
        }
    }
}