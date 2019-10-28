using System.Collections.Generic;

using NumericalSequences;

namespace Patterns
{
    public class PermutationCheck : IIsPermutation
    {
        public bool IsPermutation(NumSequenceBasic numSequenceBasic)
        {
            if (numSequenceBasic == null)
                return false;
            
            HashSet<int> letters = new HashSet<int>();
            int length = numSequenceBasic.Length;

            for (int i = 0; i < length; i++)
            {
                int letter = numSequenceBasic.GetLetter(i);
                if (letters.Contains(letter) || letter >= length)
                    return false;
                else
                    letters.Add(letter);
            }

            return true;
        }
    }
}