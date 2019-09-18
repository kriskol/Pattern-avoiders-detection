using Patterns;
using PermutationsCollections;

namespace Result
{
    public abstract class ResultPatterns : Result
    {
        public abstract void ProcessPermutation(Permutation permutation);

        public virtual void ProcessPermutations(IPermutationsCollection permutations)
        {
            foreach (var permutation in permutations)
                ProcessPermutation(permutation);
        }
    }
}