using NumericalSequences;

namespace Patterns
{
    public class PatternBasicCheckFree : IIsPatternBasic
    {
        public bool IsPatternBasic(NumSequenceBasic numSequenceBasic)
        {
            if (numSequenceBasic == null)
                return false;
            
            return true;
        }
    }
}