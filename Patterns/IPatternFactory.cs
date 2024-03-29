using NumericalSequences;

namespace Patterns
{
    public interface IPatternFactory<T> where T: PatternC<T>
    {
        T GetPattern(NumSequenceBasic numSequenceBasic, int highestPosition);
    }
}