using System;
using GeneralInterfaces;

namespace Patterns
{
    public abstract class PatternC<T>: Pattern<T>, ICloneable<T> where T: PatternC<T>
    {
        protected PatternC() : base()
        {
        }

        public abstract T Clone();
    }
}