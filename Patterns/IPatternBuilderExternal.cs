using GeneralInterfaces;

namespace Patterns
{
    public interface IPatternBuilderExternal<T,U> : ICloneable<U> where T : PatternC<T> 
                                                                    where U: IPatternBuilderExternal<T,U>
    {
        void SetSuffixLength(int suffixLength);

        void Reset();

        T CreatePattern(string[] letters, byte letterSize);
        T CreatePattern(ulong[] words, byte letterSize, int length);
    }
}