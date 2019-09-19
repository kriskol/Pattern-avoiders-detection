using NumericalSequences;

namespace Patterns
{
    public abstract class PatternFactoryExternal
    {
        protected void FillNumSequenceLetters(NumSequenceBasic numSequenceBasic, string[] letters)
        {
            int letter;
            
            for (int i = letters.Length - 1; i >= 0; i--)
            {
                int.TryParse(letters[i], out letter);
                numSequenceBasic.InsertLetterMutable(0, (ulong)letter);
            }
        }
    }
}