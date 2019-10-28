using System.Collections.Generic;
using NumericalSequences;

namespace Patterns
{
    public class PatternBasic : PatternIm<PatternBasic>
    {

        private readonly int highestPosition;
        private static readonly IPatternFactory<PatternBasic> patternFactory;
        
        public override int LowestPosition => highestPosition - NumSequenceBasic.Length + 1;
        public override int HighestPosition => highestPosition;
        protected IPatternFactory<PatternBasic> PatternFactory => patternFactory;

        public PatternBasic ChangePositive(IEnumerable<int> positions, int difference)
        {
            NumSequenceBasic newNumSequenceBasic;
            IEnumerable<int> correctedPositions = CorrectPositions(positions);
            newNumSequenceBasic = NumSequenceBasic.Change(correctedPositions, difference);

            return patternFactory.GetPattern(newNumSequenceBasic, highestPosition);
        }
        
        public override PatternBasic InsertLetter(int letter)
        {
            NumSequenceBasic newNumSequenceBasic = NumSequenceBasic.InsertLetter(NumSequenceBasic.Length, letter);
            return PatternFactory.GetPattern(newNumSequenceBasic, HighestPosition+1);
        }

        public override PatternBasic Insert(int position, int letter)
        {
            NumSequenceBasic newNumSequenceBasic = NumSequenceBasic.InsertLetter(position - LowestPosition, letter);
            return PatternFactory.GetPattern(newNumSequenceBasic, 
                HighestPosition + 1);
        }
        
        public override PatternBasic Switch(int positionFrom, int positionTo)
        {
            NumSequenceBasic newNumSequenceBasic = SwitchInternal(positionFrom, positionTo);
            return PatternFactory.GetPattern(newNumSequenceBasic, HighestPosition);
        }
        
        public override PatternBasic Delete(int position)
        {
            NumSequenceBasic newNumSequenceBasic = DeleteInternal(position);
            return PatternFactory.GetPattern(newNumSequenceBasic, HighestPosition-1);
        }
        
        public override PatternBasic  Clone()
        {
            return PatternFactory.GetPattern(NumSequenceBasic.Clone(), HighestPosition);
        }
        
        public PatternBasic(NumSequenceBasic numSequenceBasic,
                            int highestPosition) : base(numSequenceBasic)
        {
            this.highestPosition = highestPosition;
        }

        public class PatternBasicFactory : IPatternFactory<PatternBasic>
        {
            private IIsPatternBasic isPatternBasic;
            public PatternBasic GetPattern(NumSequenceBasic numSequenceBasic, 
                                            int highestPosition)
            {
                if(isPatternBasic.IsPatternBasic(numSequenceBasic))
                    return  new PatternBasic(numSequenceBasic, highestPosition);

                return null;
            }

            public PatternBasicFactory()
            {
                isPatternBasic = new PatternBasicCheckFree();
            }

            public PatternBasicFactory(IIsPatternBasic isPatternBasic)
            {
                this.isPatternBasic = isPatternBasic;
            }
        }

        static PatternBasic()
        {
            patternFactory = new PatternBasicFactory();
        }
    }
}