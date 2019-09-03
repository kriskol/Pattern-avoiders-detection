using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using NumericalSequences;

namespace Patterns
{
    public class Permutation: PatternIm<Permutation>
    {
        private readonly int highestPosition;
        private static IPatternFactory<Permutation> patternFactory;

        public override int LowestPosition => 1;
        public override int HighestPosition => highestPosition;
        protected override IPatternFactory<Permutation> PatternFactory => patternFactory;

        public override Permutation InsertLetter(byte letter)
        {
            NumSequenceBasic numSequenceBasicTemp = base.InsertLetter(letter).NumSequenceBasic;
            NumSequenceBasic newNumSequenceBasic = IncreaseHigherLetters(numSequenceBasicTemp, letter,
                                                            numSequenceBasicTemp.Length-1);
            return patternFactory.GetPattern(newNumSequenceBasic, LowestPosition, HighestPosition +1,
                                            Maximum + 1);
        }
        public override Permutation Insert(int position, byte letter)
        {
            NumSequenceBasic numSequenceBasicTemp = base.Insert(position, letter).NumSequenceBasic;
            NumSequenceBasic newNumSequenceBasic = IncreaseHigherLetters(numSequenceBasicTemp, letter,
                                                                            position - LowestPosition);
            return patternFactory.GetPattern(newNumSequenceBasic, LowestPosition,
                                            HighestPosition + 1, Maximum + 1);
        }
        public override Permutation Delete(int position)
        {
            byte letter = NumSequenceBasic.GetLetter(position - LowestPosition);
            NumSequenceBasic numSequenceBasicTemp = base.Delete(position).NumSequenceBasic;
            NumSequenceBasic newNumSequenceBasic = DecreaseHigherLetters(numSequenceBasicTemp, letter,
                                                position - LowestPosition);
            return patternFactory.GetPattern(newNumSequenceBasic, LowestPosition, 
                                HighestPosition-1, Maximum -1);
        }

        public Permutation Insert(int position, byte letter, IEnumerable<int> positionsHigherLetters)
        {
            NumSequenceBasic numSequenceBasicTemp = base.Insert(position, letter).NumSequenceBasic;
            NumSequenceBasic newNumSequenceBasic = numSequenceBasicTemp.
                                                    Change(CorrectPositions(positionsHigherLetters),
                                                            +1);
            return patternFactory.GetPattern(newNumSequenceBasic, LowestPosition, 
                                    HighestPosition+1, Maximum+1);
        }
        
        public Permutation Delete(int position, IEnumerable<int> positionsHigherLetters)
        {
            NumSequenceBasic numSequenceBasicTemp = base.Delete(position).NumSequenceBasic;
            NumSequenceBasic newNumSequenceBasic = numSequenceBasicTemp.
                                                        Change(CorrectPositions(positionsHigherLetters),
                                                            -1);
            return patternFactory.GetPattern(newNumSequenceBasic, LowestPosition, 
                                            HighestPosition - 1, Maximum - 1);
        }

        private IEnumerable<int> CorrectPositions(IEnumerable<int> positions)
        {
            List<int> newPositions = new List<int>();
            
            foreach (var position in positions)
                newPositions.Add(position - LowestPosition);
            
            return newPositions;
        }

        private NumSequenceBasic IncreaseHigherLetters(NumSequenceBasic numSequenceBasic, byte letter,
            int avoidedPosition)
        {
            return CorrectNumSequence(numSequenceBasic, 1, FindPositionsToBeAltered(numSequenceBasic,
                                                                        letter, avoidedPosition));
        }

        private NumSequenceBasic DecreaseHigherLetters(NumSequenceBasic numSequenceBasic, byte letter,
            int avoidedPosition)
        {
            return CorrectNumSequence(numSequenceBasic, -1, FindPositionsToBeAltered(numSequenceBasic,
                                                                        letter, avoidedPosition));
        }

        private IEnumerable<int> FindPositionsToBeAltered(NumSequenceBasic numSequenceBasic, byte letter,
            int avoidedPosition)
        {
            List<int> positions = new List<int>();

            for (int i = 0; i < numSequenceBasic.Length; i++)
                if(avoidedPosition != i)
                    if (numSequenceBasic.GetLetter(i) >= letter)
                        positions.Add(i);

            return positions;
        }
        private NumSequenceBasic CorrectNumSequence(NumSequenceBasic numSequenceBasic, 
                                                    int difference, IEnumerable<int> positions)
        {
            return numSequenceBasic.Change(positions, difference);
        }
        
        public class PermutationFactory : IPatternFactory<Permutation>
        {
            private IIsPermutation isPermutation;
            public Permutation GetPattern(NumSequenceBasic numSequenceBasic, int lowestPosition, 
                                            int highestPosition, int maximum)
            {
                if (isPermutation.IsPermutation(numSequenceBasic))
                    return new Permutation(numSequenceBasic, highestPosition, maximum);

                return null;
            }

            public PermutationFactory()
            {
                isPermutation = new PermutationCheckFree();
            }
            public PermutationFactory(IIsPermutation isPermutation)
            {
                this.isPermutation = isPermutation;
            }
        }

        private Permutation(NumSequenceBasic numSequenceBasic, int highestPosition, int maximum) : base(numSequenceBasic, maximum)
        {
            this.highestPosition = highestPosition;
        }

        static Permutation()
        {
            patternFactory = new PermutationFactory();
        }
        
    }
}