using System.Collections.Generic;
using NumericalSequences;

namespace Patterns
{
    public class Permutation: PatternMax<Permutation>
    {
        private readonly int highestPosition;
        private static readonly IPatternFactoryMax<Permutation> patternFactory;
        private Permutation inversion;
        private bool inversionSet;
        
        public override int LowestPosition => 0;
        public override int HighestPosition => highestPosition;
        protected override IPatternFactoryMax<Permutation> PatternFactory => patternFactory;


        public override Permutation InsertPosition(int position)
        {
            NumSequenceBasic newNumSequenceBasic = NumSequenceBasic.InsertLetter(position - LowestPosition,
                Maximum + 1);
            return PatternFactory.GetPattern(newNumSequenceBasic, HighestPosition+1, 
                Maximum+1);
        }
        
        public override Permutation InsertLetter(int letter)
        {
            int maximum;
            bool lettersIncreased = false;
            NumSequenceBasic newNumSequenceBasic;
            
            if (Maximum >=  letter)
            {
                maximum = Maximum + 1;
                lettersIncreased = true;
            }
            else
                maximum = letter;
            
            NumSequenceBasic numSequenceBasicTemp = NumSequenceBasic.InsertLetter(NumSequenceBasic.Length, letter);
            if (lettersIncreased)
                newNumSequenceBasic = IncreaseHigherLetters(numSequenceBasicTemp, letter,
                    numSequenceBasicTemp.Length - 1);
            else
                newNumSequenceBasic = numSequenceBasicTemp;
            
            return patternFactory.GetPattern(newNumSequenceBasic, HighestPosition +1,
                                            maximum);
        }
        public override Permutation Insert(int position, int letter)
        {
            int maximum;
            bool lettersIncreased = false;
            NumSequenceBasic newNumSequenceBasic;
            
            if (Maximum >=  letter)
            {
                maximum = Maximum + 1;
                lettersIncreased = true;
            }
            else
                maximum = letter;

            
            NumSequenceBasic numSequenceBasicTemp = NumSequenceBasic.InsertLetter
                                                    (position - LowestPosition, letter);
            
            if (lettersIncreased)
                newNumSequenceBasic = IncreaseHigherLetters(numSequenceBasicTemp, letter,
                    position - LowestPosition);
            else
                newNumSequenceBasic = numSequenceBasicTemp;
                    
            return patternFactory.GetPattern(newNumSequenceBasic,
                                            HighestPosition + 1, maximum);
        }
        public override Permutation Delete(int position)
        {
            NumSequenceBasic newNumSequenceBasic;
            
            int letter = NumSequenceBasic.GetLetter(position - LowestPosition);
            NumSequenceBasic numSequenceBasicTemp = NumSequenceBasic.
                                                    DeleteLetterPosition(position - LowestPosition);
            
            if (letter == Maximum)
                newNumSequenceBasic = numSequenceBasicTemp;
            else 
                newNumSequenceBasic = DecreaseHigherLetters(numSequenceBasicTemp, letter,
                                                position - LowestPosition);
            
            return patternFactory.GetPattern(newNumSequenceBasic, 
                                HighestPosition-1, Maximum -1);
        }

        public Permutation Insert(int position, int letter, IEnumerable<int> positionsHigherLetters)
        {
            int maximum;
            bool lettersIncreased = false;
            NumSequenceBasic newNumSequenceBasic;
            
            if (Maximum >=  letter)
            {
                lettersIncreased = true;
                maximum = Maximum + 1;
            }
            else
                maximum = letter;
            
            NumSequenceBasic numSequenceBasicTemp = NumSequenceBasic.InsertLetter
                                                    (position - LowestPosition,
                                                    letter);
            
            if (lettersIncreased)
                newNumSequenceBasic = numSequenceBasicTemp.Change(CorrectPositions(positionsHigherLetters),
                    +1);
            else
                newNumSequenceBasic = numSequenceBasicTemp;
            
            
            return patternFactory.GetPattern(newNumSequenceBasic, 
                                    HighestPosition+1, maximum);
        }
        
        public Permutation Delete(int position, IEnumerable<int> positionsHigherLetters)
        {
            NumSequenceBasic numSequenceBasicTemp = DeleteInternal(position);
            
            NumSequenceBasic newNumSequenceBasic = numSequenceBasicTemp.
                                                        Change(CorrectPositions(positionsHigherLetters),
                                                            -1);
            return patternFactory.GetPattern(newNumSequenceBasic, 
                                            HighestPosition - 1, Maximum - 1);
        }

        
        public override Permutation Switch(int positionFrom, int positionTo)
        {
            NumSequenceBasic newNumSequenceBasic = SwitchInternal(positionFrom, positionTo);
            return PatternFactory.GetPattern(newNumSequenceBasic, HighestPosition, Maximum);
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
                    numSequenceBasic.SetLetterMutable(NumSequenceBasic.GetLetter(i),i);

                inversionSet = true;
                inversion = patternFactory.GetPattern(numSequenceBasic, HighestPosition, Maximum);
                
                return inversion;
            }
        }
        

        private NumSequenceBasic IncreaseHigherLetters(NumSequenceBasic numSequenceBasic, int letter,
            int avoidedPosition)
        {
            return CorrectNumSequence(numSequenceBasic, 1, FindPositionsToBeAltered(numSequenceBasic,
                                                                        letter, avoidedPosition));
        }

        private NumSequenceBasic DecreaseHigherLetters(NumSequenceBasic numSequenceBasic, int letter,
            int avoidedPosition)
        {
            return CorrectNumSequence(numSequenceBasic, -1, FindPositionsToBeAltered(numSequenceBasic,
                                                                        letter, avoidedPosition));
        }

        private IEnumerable<int> FindPositionsToBeAltered(NumSequenceBasic numSequenceBasic, int letter,
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

        public class PermutationFactory : IPatternFactoryMax<Permutation>
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

        private Permutation(NumSequenceBasic numSequenceBasic, int highestPosition, int maximum) 
                                        : base(numSequenceBasic, maximum)
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