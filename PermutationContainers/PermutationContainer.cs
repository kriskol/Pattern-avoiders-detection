using Patterns;

namespace PermutationContainers
{
    public abstract class PermutationContainer
    {
        private readonly Permutation permutation;

        public Permutation Permutation => permutation;
        public int PermutationLength => permutation.Length;
        public byte LetterSize => permutation.LetterSize;
        
        protected PermutationContainer(Permutation permutation)
        {
            this.permutation = permutation;
        }
    }
}