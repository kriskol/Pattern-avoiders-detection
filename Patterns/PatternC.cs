using GeneralInterfaces;

namespace Patterns
{
    public abstract class PatternC<T>: Pattern<T>, ICloneable<T> where T: PatternC<T>
    {
        public abstract T Clone();
    }
}