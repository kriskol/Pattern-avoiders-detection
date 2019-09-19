namespace Patterns
{
    public interface IPatternBasicBuilderExternal : IPatternBuilderExternal<PatternBasic>
    {
        void SetMaximalLength(int maximalLength);
    }
}