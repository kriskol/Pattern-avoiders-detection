using System;

using Patterns;

namespace PermutationContainers
{
    public abstract class PermutationContainerPositions<T> : PermutationContainer where T: Pattern<T>
    {
        private readonly Pattern<T> permutationPositions;

        protected Pattern<T> PermutationPositions => permutationPositions;

        public int Position(int letter)
        {
            if (letter <=  permutationPositions.HighestPosition &&
                letter >=  permutationPositions.LowestPosition)
                return permutationPositions.Get(letter);
            
            throw new ArgumentException();
        }

        public int PositionPreceedingLetters(int letter)
        {
            int position = Position(letter);
            int lowerPositions = 0;
            for (int i = letter+1; i <= permutationPositions.HighestPosition; i++)
            {
                if (permutationPositions.Get(i) < position)
                    lowerPositions++;
            }

            return position - lowerPositions;
        }
        public abstract int CountSuccessors { get; }
        
        protected PermutationContainerPositions(Permutation permutation, Pattern<T> permutationPositions) 
                                                : base(permutation)
        {
            this.permutationPositions = permutationPositions;
        }
    }
}