namespace Patterns
{
    public interface IPatternBasicBuilderExternal : IPatternBuilderExternal<PatternBasic, IPatternBasicBuilderExternal>
    {
        void SetMaximalLength(int maximalLength);
    }
}