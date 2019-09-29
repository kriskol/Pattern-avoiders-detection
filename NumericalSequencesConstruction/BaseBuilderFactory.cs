namespace NumericalSequences
{
    public abstract class BaseBuilderFactory : IBaseBuilderFactory

    {
        protected bool letterSizeSet;
        protected bool lengthSet;
        protected bool suffixLengthSet;

        protected virtual void LetterSizeSet()
        {
            letterSizeSet = true;
        }
        protected virtual void LengthSet()
        {
            lengthSet = true;
        }

        protected virtual void SuffixLengthSet()
        {
            suffixLengthSet = true;
        }
        
        public abstract void SetLetterSize(byte letterSize);
        public abstract void SetLength(int length);
        public abstract void SetSuffixLength(int suffixLength);
        
        public abstract void SetSuffixLength();
        
        
        public virtual void SetDefaultSuffixLength()
        {
            SetSuffixLength();   
        }
        
        public virtual void Reset()
        {
            letterSizeSet = false;
            lengthSet = false;
            SetDefaultSuffixLength();
        }
    }
}