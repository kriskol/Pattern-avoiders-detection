using GeneralInterfaces;

namespace NumericalSequences
{
    public abstract class NumSequenceBaseC<T>: NumSequenceBase<T>, ICloneable<T> where T: NumSequenceBaseC<T>
    {
        public abstract T Clone();
        
        protected NumSequenceBaseC(INumSequenceBaseBuilder builder) : base(builder)
        {
        }   
    }
}