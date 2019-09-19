namespace Patterns
{
    public interface IPatternBuilderExternal<T> where T : PatternC<T>
    {
        void SetSuffixLength(int suffixLength);

        void Reset();

        T CreatePattern(string[] letters, byte letterSize);
        T CreatePattern(ulong[] words, byte letterSize, int length);
    }
}