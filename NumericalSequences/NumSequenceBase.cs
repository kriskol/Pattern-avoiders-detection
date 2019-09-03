using System;
using System.Collections.Generic;

namespace NumericalSequences
{
    public abstract class NumSequenceBase<T> : IEquatable<T> where T:NumSequenceBase<T>
    {
        private readonly byte letterSize;
        private int length;
        private int suffixLength;
        private int maximalLength;
        protected bool suffixLengthSet = false;
        protected bool maximalLengthSet = false;

        public byte LetterSize => letterSize;
        public int Length => length;
        protected int SuffixLength => suffixLengthSet ? suffixLength : length;
        protected int MaximalLength => maximalLengthSet ? maximalLength : Int32.MaxValue;
   
        public abstract byte GetLetter(int position);
        public abstract T SetLetter(int position, byte letter);
        public abstract T DeleteLetterPosition(int position);
        
        public abstract T DeleteLetter(byte letter);
        public abstract T InsertLetter(int position, byte letter);
        public abstract T Switch(int positionFrom, int positionTo);
        public abstract T Change(IEnumerable<int> positions, int difference);

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return Equals(obj as T);
        }
        
        public abstract bool Equals(T other);
        public abstract override int GetHashCode();
        public abstract override string ToString();
        
        protected NumSequenceBase(byte letterSize, int length, int suffixLength, int maximalLength)
        {
            this.letterSize = letterSize;
            this.length = length;
            this.suffixLength = suffixLength;
            this.maximalLength = maximalLength;
        }
    }
}