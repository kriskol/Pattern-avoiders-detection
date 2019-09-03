using System;
using GeneralInterfaces;

namespace Patterns
{
    public abstract class Pattern<T>: IPosition , IEquatable<T> where  T: Pattern<T>
    {
        private readonly int maximum;
        
        public abstract int LowestPosition { get; }
        public abstract int HighestPosition { get; }
        public abstract byte LetterSize { get; }
        public int Length => HighestPosition - LowestPosition;
        public int Maximum => maximum;

        public abstract T InsertPosition(int position);
        public abstract T InsertLetter(byte letter);
        public abstract T Insert(int position, byte letter);
        public abstract byte Get(int position);
        public abstract T Switch(int positionFrom, int positionTo);
        public abstract T Delete(int position);
        int IPosition.GetPosition(int index)
        { 
            return Get(index);
        }
        
        public abstract override string ToString();

        public abstract override int GetHashCode();

        public abstract bool Equals(T other);

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            return Equals((T) obj);
        }

        protected Pattern(int newMaximum)
        {
            maximum = newMaximum;
        }
    }
}