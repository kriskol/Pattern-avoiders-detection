using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Patterns;

namespace PermutationsCollections
{
    public class PermutationDictionary<T> : IPermutationDictionary<T>
    {
        private Dictionary<Permutation, T> dictionary;
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
            dictionary.Add(item.Key,item.Value);
        }

        public void Clear()
        {
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
            return dictionary.Remove(item.Key);
        }

        public int Count => dictionary.Count;
        public bool IsReadOnly => false;
        public void Add(Permutation key, T value)
        {
            dictionary.Add(key,value);
        }

        public bool ContainsKey(Permutation key)
        {
            return dictionary.ContainsKey(key);
        }

        public bool Remove(Permutation key)
        {
            return dictionary.Remove(key);
        }

        public bool TryGetValue(Permutation key, out T value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        public T this[Permutation key]
        {
            get => dictionary[key];
            set => dictionary[key] = value;
        }

        public ICollection<Permutation> Keys => dictionary.Keys;
        public ICollection<T> Values => dictionary.Values;
    }
}