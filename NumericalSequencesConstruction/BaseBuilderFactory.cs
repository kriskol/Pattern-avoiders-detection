namespace NumericalSequences
{
    public abstract class BaseBuilderFactory : IBaseBuilderFactory

    {
        protected bool letterSizeS;
        protected bool lengthS;
        protected bool suffixLengthS;

        protected virtual void LetterSizeSet()
        {
            letterSizeS = true;
        }
        protected virtual void LengthSet()
        {
            lengthS = true;
        }

        protected virtual void SuffixLengthSet()
        {
            suffixLengthS = true;
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
            letterSizeS = false;
            lengthS = false;
            SetDefaultSuffixLength();
        }
    }
}