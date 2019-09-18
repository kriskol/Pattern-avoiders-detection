namespace Patterns
{
    public interface IPatternBasicFactoryExternal
    {
        PatternBasic CreatePatternBasic(ulong[] words, int length, int maximalLength, int letterSize);
        PatternBasic CreatePatternBasic(string[] letters, int maximalLength, int letterSize);
    }
}