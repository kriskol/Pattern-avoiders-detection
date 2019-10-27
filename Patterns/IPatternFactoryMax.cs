using NumericalSequences;

namespace Patterns
{
    public interface IPatternFactoryMax<T> where T : PatternC<T>
    {
        T GetPattern(NumSequenceBasic numSequenceBasic, int highestPosition, int maximum);
    }
}