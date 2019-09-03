using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Patterns;

namespace PermutationContainers
{
    public abstract class PermutationContainerPositions<T> : PermutationContainer where T: PermutationContainerPositions<T>
    {
        private readonly Pattern<PatternBasic> permutationPositions;

        protected Pattern<PatternBasic> PermutationPositions => permutationPositions;
        public abstract int CountSuccessors { get; }
        
        protected PermutationContainerPositions(Permutation permutation, Pattern<PatternBasic> permutationPositions) 
                                                : base(permutation)
        {
            this.permutationPositions = permutationPositions;
        }
    }
}