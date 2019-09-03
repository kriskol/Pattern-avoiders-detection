using GeneralInterfaces;

namespace NumericalSequences
{
    public abstract class NumSequenceBaseC<T>: NumSequenceBase<T>, ICloneable<T> where T: NumSequenceBaseC<T>
    {
        protected static readonly INumSequenceFactory<T> numSequenceFactory;
        
        public abstract T Clone();
    }
}