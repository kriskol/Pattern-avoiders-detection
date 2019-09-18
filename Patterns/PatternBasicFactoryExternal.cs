namespace Patterns
{
    public class PatternBasicFactoryExternal : PatternFactoryExternal, IPatternBasicFactoryExternal
    {
        public PatternBasic CreatePatternBasic(ulong[] words, int length, int maximalLength, int letterSize)
        {
            throw new System.NotImplementedException();
        }

        public PatternBasic CreatePatternBasic(string[] letters, int maximalLength, int letterSize)
        {
            throw new System.NotImplementedException();
        }
    }
}