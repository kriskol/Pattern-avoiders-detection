using NumericalSequences;

namespace Patterns
{
    public class PermutationCheckFree : IIsPermutation
    {
        public bool IsPermutation(NumSequenceBasic numSequenceBasic)
        {
            if (numSequenceBasic == null)
                return false;

            return true;
        }
    }
}