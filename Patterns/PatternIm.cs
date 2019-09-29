using System;
using System.Collections.Generic;
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
            return PatternFactory.GetPattern(numSequenceBasic.Clone(), HighestPosition, Maximum);
        }

        protected ulong FindMaximum(IEnumerable<int> positions)
        {
            ulong maximum = 0;
            ulong letter;
            foreach (var position in positions)
            {
                letter = numSequenceBasic.GetLetter(position);
                if (numSequenceBasic.GetLetter(position) > maximum)
                    maximum = letter;
            }

            return maximum;
        }
        
        protected IEnumerable<int> CorrectPositions(IEnumerable<int> positions)
        {
            List<int> newPositions = new List<int>();
            
            foreach (var position in positions)
                newPositions.Add(position - LowestPosition);
            
            return newPositions;
        }
        
        public override T InsertPosition(int position)
        {
            NumSequenceBasic newNumSequenceBasic = numSequenceBasic.InsertLetter(position - LowestPosition,
                                                                                (byte)(Maximum + 1));
            return PatternFactory.GetPattern(newNumSequenceBasic, HighestPosition+1, 
                                            Maximum+1);
        }

        public override T InsertLetter(ulong letter)
        {
            int newMax = Math.Max((int)letter, Maximum);
            NumSequenceBasic newNumSequenceBasic = numSequenceBasic.InsertLetter(numSequenceBasic.Length, letter);
            return PatternFactory.GetPattern(newNumSequenceBasic, HighestPosition+1,
                                                                                            newMax);
        }

        public override T Insert(int position, ulong letter)
        {
            int newMax = Math.Max((int)letter, Maximum);
            NumSequenceBasic newNumSequenceBasic = numSequenceBasic.InsertLetter(position - LowestPosition, letter);
            return PatternFactory.GetPattern(newNumSequenceBasic, 
                                            HighestPosition + 1, newMax);
        }

        public override ulong Get(int position)
        {
            return (byte)numSequenceBasic.GetLetter(position - LowestPosition);
        }

        public override T Switch(int positionFrom, int positionTo)
        {
            NumSequenceBasic newNumSequenceBasic = numSequenceBasic.Switch(positionFrom-LowestPosition,
                                                                            positionTo - LowestPosition);
            return PatternFactory.GetPattern(newNumSequenceBasic, HighestPosition, Maximum);
        }

        public override T Delete(int position)
        {
           byte letter = (byte)numSequenceBasic.GetLetter(position - LowestPosition);
           int maximum = Maximum;
           if (letter == Maximum)
               maximum--;
            
           NumSequenceBasic newNumSequenceBasic = numSequenceBasic.DeleteLetterPosition(position - LowestPosition);

           return PatternFactory.GetPattern(newNumSequenceBasic, HighestPosition-1, maximum);
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