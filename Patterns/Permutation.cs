using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using NumericalSequences;

namespace Patterns
{
    public class Permutation: PatternIm<Permutation>
    {
        private readonly int highestPosition;
        private static readonly IPatternFactory<Permutation> patternFactory;
        private Permutation inversion;
        private bool inversionSet;
        
        public override int LowestPosition => 0;
        public override int HighestPosition => highestPosition;
        protected override IPatternFactory<Permutation> PatternFactory => patternFactory;

        public override Permutation InsertLetter(ulong letter)
        {
            int maximum;

            if (Maximum >= (int)letter)
                maximum = Maximum + 1;
            else
                maximum = (int)letter;
            
            NumSequenceBasic numSequenceBasicTemp = base.InsertLetter(letter).NumSequenceBasic;
            NumSequenceBasic newNumSequenceBasic = IncreaseHigherLetters(numSequenceBasicTemp, letter,
                                                            numSequenceBasicTemp.Length-1);
            return patternFactory.GetPattern(newNumSequenceBasic, HighestPosition +1,
                                            maximum);
        }
        public override Permutation Insert(int position, ulong letter)
        {
            int maximum;

            if (Maximum >= (int)letter)
                maximum = Maximum + 1;
            else
                maximum = (int)letter;

            
            NumSequenceBasic numSequenceBasicTemp = base.Insert(position, letter).NumSequenceBasic;
            NumSequenceBasic newNumSequenceBasic = IncreaseHigherLetters(numSequenceBasicTemp, letter,
                                                                            position - LowestPosition);
            return patternFactory.GetPattern(newNumSequenceBasic,
                                            HighestPosition + 1, maximum);
        }
        public override Permutation Delete(int position)
        {

            byte letter = (byte)NumSequenceBasic.GetLetter(position - LowestPosition);
            NumSequenceBasic numSequenceBasicTemp = base.Delete(position).NumSequenceBasic;
            NumSequenceBasic newNumSequenceBasic = DecreaseHigherLetters(numSequenceBasicTemp, letter,
                                                position - LowestPosition);
            return patternFactory.GetPattern(newNumSequenceBasic, 
                                HighestPosition-1, Maximum -1);
        }

        public Permutation Insert(int position, byte letter, IEnumerable<int> positionsHigherLetters)
        {
            int maximum;

            if (Maximum >= (int)letter)
                maximum = Maximum + 1;
            else
                maximum = (int)letter;
            
            NumSequenceBasic numSequenceBasicTemp = base.Insert(position, letter).NumSequenceBasic;
            NumSequenceBasic newNumSequenceBasic = numSequenceBasicTemp.
                                                    Change(CorrectPositions(positionsHigherLetters),
                                                            +1);
            return patternFactory.GetPattern(newNumSequenceBasic, 
                                    HighestPosition+1, maximum);
        }
        
        public Permutation Delete(int position, IEnumerable<int> positionsHigherLetters)
        {
            NumSequenceBasic numSequenceBasicTemp = base.Delete(position).NumSequenceBasic;
            NumSequenceBasic newNumSequenceBasic = numSequenceBasicTemp.
                                                        Change(CorrectPositions(positionsHigherLetters),
                                                            -1);
            return patternFactory.GetPattern(newNumSequenceBasic, 
                                            HighestPosition - 1, Maximum - 1);
        }

        public Permutation ComputeInversion()
        {
            if (inversionSet)
                return inversion;
            else
            {
                NumSequenceBasicFactory factory = new NumSequenceBasicFactory();
                NumSequenceBasic numSequenceBasic = factory.GetNumSequenceDefault(LetterSize, Length, false);

                for (int i = LowestPosition; i <= HighestPosition; i++)
                    numSequenceBasic.SetLetterMutable((int)NumSequenceBasic.GetLetter(i),(ulong)i);

                inversionSet = true;
                inversion = patternFactory.GetPattern(numSequenceBasic, HighestPosition, Maximum);
                
                return inversion;
            }
        }
        

        private NumSequenceBasic IncreaseHigherLetters(NumSequenceBasic numSequenceBasic, ulong letter,
            int avoidedPosition)
        {
            return CorrectNumSequence(numSequenceBasic, 1, FindPositionsToBeAltered(numSequenceBasic,
                                                                        letter, avoidedPosition));
        }

        private NumSequenceBasic DecreaseHigherLetters(NumSequenceBasic numSequenceBasic, ulong letter,
            int avoidedPosition)
        {
            return CorrectNumSequence(numSequenceBasic, -1, FindPositionsToBeAltered(numSequenceBasic,
                                                                        letter, avoidedPosition));
        }

        private IEnumerable<int> FindPositionsToBeAltered(NumSequenceBasic numSequenceBasic, ulong letter,
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
            public Permutation GetPattern(NumSequenceBasic numSequenceBasic, 
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
            inversionSet = false;
        }

        static Permutation()
        {
            patternFactory = new PermutationFactory();
        }
        
    }
}