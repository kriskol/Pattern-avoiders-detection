using System;
using System.Text;
using NumericalSequences;

namespace Patterns
{
    public abstract class PatternIm<T>: PatternC<T> where T: PatternIm<T>
    {
        private readonly NumSequenceBasic numSequenceBasic;
        protected PatternIm(NumSequenceBasic numSequenceBasic, int maximum) : base(maximum)
        {
            this.numSequenceBasic = numSequenceBasic;
        }

        protected NumSequenceBasic NumSequenceBasic => numSequenceBasic;
        protected abstract IPatternFactory<T> PatternFactory { get; }

        public override byte LetterSize => numSequenceBasic.LetterSize;

        public override T Clone()
        {
            return PatternFactory.GetPattern(numSequenceBasic.Clone(), LowestPosition, HighestPosition, Maximum);
        }
        
        public override T InsertPosition(int position)
        {
            NumSequenceBasic newNumSequenceBasic = numSequenceBasic.InsertLetter(position - LowestPosition,
                                                                                (byte)(Maximum + 1));
            return PatternFactory.GetPattern(numSequenceBasic, LowestPosition, HighestPosition+1, 
                                            Maximum+1);
        }

        public override T InsertLetter(byte letter)
        {
            int newMax = Math.Max(letter, Maximum);
            NumSequenceBasic newNumSequenceBasic = numSequenceBasic.InsertLetter(numSequenceBasic.Length, letter);
            return PatternFactory.GetPattern(newNumSequenceBasic, LowestPosition, HighestPosition, newMax);
        }

        public override T Insert(int position, byte letter)
        {
            int newMax = Math.Max(letter, Maximum);
            NumSequenceBasic newNumSequenceBasic = numSequenceBasic.InsertLetter(position - LowestPosition, letter);
            return PatternFactory.GetPattern(newNumSequenceBasic, LowestPosition, 
                                            HighestPosition + 1, newMax);
        }

        public override byte Get(int position)
        {
            return numSequenceBasic.GetLetter(position - LowestPosition);
        }

        public override T Switch(int positionFrom, int positionTo)
        {
            NumSequenceBasic newNumSequenceBasic = numSequenceBasic.Switch(positionFrom, positionTo);
            return PatternFactory.GetPattern(newNumSequenceBasic, LowestPosition, HighestPosition, Maximum);
        }

        public override T Delete(int position)
        {
           byte letter = numSequenceBasic.GetLetter(position - LowestPosition);
           int maximum = Maximum;
           if (letter == Maximum)
               maximum--;

           NumSequenceBasic newNumSequenceBasic = numSequenceBasic.DeleteLetterPosition(position - LowestPosition);

           return PatternFactory.GetPattern(newNumSequenceBasic, LowestPosition, HighestPosition, maximum);
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