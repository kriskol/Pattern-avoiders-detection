using System;
using GeneralInterfaces;

namespace Patterns
{
    public abstract class Pattern<T>: IPosition , IEquatable<T> where  T: Pattern<T>
    {
        public abstract int LowestPosition { get; }
        public abstract int HighestPosition { get; }
        public abstract byte LetterSize { get; }
        public int Length => HighestPosition - LowestPosition + 1;
        
        public abstract T InsertLetter(ulong letter);
        public abstract T Insert(int position, ulong letter);
        public abstract ulong Get(int position);
        public abstract T Switch(int positionFrom, int positionTo);
        public abstract T Delete(int position);
        int IPosition.GetPosition(int index)
        { 
            return (int)Get(index);
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

        protected Pattern()
        {
        }
    }
}