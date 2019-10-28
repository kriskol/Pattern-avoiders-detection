using System.Collections.Generic;
using NumericalSequences;

namespace Patterns
{
    public abstract class PatternIm<T>: PatternC<T> where T: PatternIm<T>
    {
        private readonly NumSequenceBasic numSequenceBasic;
        protected PatternIm(NumSequenceBasic numSequenceBasic)
        {
            this.numSequenceBasic = numSequenceBasic;
        }

        protected NumSequenceBasic NumSequenceBasic => numSequenceBasic;

        public override byte LetterSize => numSequenceBasic.LetterSize;

        protected IEnumerable<int> CorrectPositions(IEnumerable<int> positions)
        {
            List<int> newPositions = new List<int>();
            
            foreach (var position in positions)
                newPositions.Add(position - LowestPosition);
            
            return newPositions;
        }

        public override int Get(int position)
        {
            return numSequenceBasic.GetLetter(position - LowestPosition);
        }

        protected virtual NumSequenceBasic DeleteInternal(int position)
        {
            return NumSequenceBasic.DeleteLetterPosition(position - LowestPosition);
        }

        protected virtual NumSequenceBasic SwitchInternal(int positionFrom, int positionTo)
        {
            return NumSequenceBasic.Switch(positionFrom-LowestPosition,
                positionTo - LowestPosition);
        }

        public override string ToString()
        {
            return numSequenceBasic.ToString();
        }

        public override int GetHashCode()
        {
            return numSequenceBasic.GetHashCode();
        }

        public override bool Equals(T other)
        {
            if (other == null)
                return false;

            if (LowestPosition == other.LowestPosition &&
                HighestPosition == other.HighestPosition &&
                numSequenceBasic.Equals(other.NumSequenceBasic))
                return true;

            return false;
        }
        
    }
}