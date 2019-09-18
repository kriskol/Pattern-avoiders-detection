using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Security.Policy;
using NumericalSequences;

namespace Patterns
{
    public class PermutationCheck : IIsPermutation
    {
        public bool IsPermutation(NumSequenceBasic numSequenceBasic)
        {
            if (numSequenceBasic == null)
                return false;
            
            HashSet<byte> letters = new HashSet<byte>();
            int length = numSequenceBasic.Length;

            for (int i = 0; i < length; i++)
            {
                byte letter = (byte)numSequenceBasic.GetLetter(i);
                if (letters.Contains(letter) || letter >= length)
                    return false;
                else
                    letters.Add(letter);
            }

            return true;
        }
    }
}