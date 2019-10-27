using System.Collections.Generic;
using NumericalSequences;

namespace Patterns
{
    public abstract class PatternMax<T> : PatternIm<T> where T: PatternMax<T>
    {
        private readonly int maximum;
        protected abstract IPatternFactoryMax<T> PatternFactory { get; }
        
        public int Maximum
        {
            get => maximum;
        }
        
        protected ulong FindMaximum(IEnumerable<int> positions)
        {
            ulong maximum = 0;
            ulong letter;
            foreach (var position in positions)
            {
                letter = NumSequenceBasic.GetLetter(position);
                if (letter > maximum)
                    maximum = letter;
            }

            return maximum;
        }
        
        public abstract T InsertPosition(int position);
        
        public override T Clone()
        {
            return PatternFactory.GetPattern(NumSequenceBasic.Clone(), HighestPosition, Maximum);
        }
        
        protected PatternMax(NumSequenceBasic numSequenceBasic, int maximum) : base(numSequenceBasic)
        {
            this.maximum = maximum;
        }
    }
}