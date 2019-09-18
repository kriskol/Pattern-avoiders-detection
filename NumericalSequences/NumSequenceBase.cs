using System;
using System.Collections.Generic;

namespace NumericalSequences
{
    public abstract class NumSequenceBase<T> : IEquatable<T> where T:NumSequenceBase<T>
    {
        private readonly byte letterSize;
        private int length;
        private readonly int suffixLength;
        private readonly bool suffixLengthSet;
        public byte LetterSize => letterSize;

        public int Length
        {
            get { return length; }
            protected set { length = value; }
        }
        protected int SuffixLength => SuffixLengthSet ? suffixLength : 0;
        protected  bool SuffixLengthSet => suffixLengthSet;
        
        public abstract ulong GetLetter(int position);
        public abstract T SetLetter(int position, ulong letter);
        public abstract void SetLetterMutable(int position, ulong letter);
        public abstract T DeleteLetterPosition(int position);
        
        public abstract T InsertLetter(int position, ulong letter);
        public abstract void InsertLetterMutable(int position, ulong letter);
        public abstract T Switch(int positionFrom, int positionTo);
        public abstract T Change(IEnumerable<int> positions, int difference);
        public abstract void ChangeMutable(IEnumerable<int> positions, int difference);

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return Equals(obj as T);
        }
        
        public abstract bool Equals(T other);
        public abstract override int GetHashCode();
        public abstract override string ToString();
        
        protected NumSequenceBase(INumSequenceBaseBuilder builder)
        {
            letterSize = builder.LetterSize;
            length = builder.Length;
            suffixLength = builder.SuffixLength;
            suffixLengthSet = builder.SuffixLengthSet;
        }
    }
}
