using System;
using System.Collections.Generic;
using System.Text;
using NumericalSequences;

namespace Patterns
{
    public class PatternBasic : PatternIm<PatternBasic>
    {

        private readonly int highestPosition;
        private static readonly IPatternFactory<PatternBasic> patternFactory;
        
        public override int LowestPosition => highestPosition - NumSequenceBasic.Length + 1;
        public override int HighestPosition => highestPosition;
        protected override IPatternFactory<PatternBasic> PatternFactory => patternFactory;

        public PatternBasic Change(IEnumerable<int> positions, int difference)
        {
            int maximum;
            NumSequenceBasic newNumSequenceBasic;
            IEnumerable<int> correctedPositions = CorrectPositions(positions);
            newNumSequenceBasic = NumSequenceBasic.Change(correctedPositions, difference);
            maximum = Math.Max(Maximum, (int)FindMaximum(correctedPositions));

            return patternFactory.GetPattern(newNumSequenceBasic, highestPosition, maximum);
        }
        
        
        
        public PatternBasic(NumSequenceBasic numSequenceBasic,
                            int highestPosition, int maximum) : base(numSequenceBasic, maximum)
        {
            this.highestPosition = highestPosition;
        }

        public class PatternBasicFactory : IPatternFactory<PatternBasic>
        {
            private IIsPatternBasic isPatternBasic;
            public PatternBasic GetPattern(NumSequenceBasic numSequenceBasic, 
                                            int highestPosition, int maximum)
            {
                if(isPatternBasic.IsPatternBasic(numSequenceBasic))
                    return  new PatternBasic(numSequenceBasic, highestPosition, maximum);

                return null;
            }

            public PatternBasicFactory()
            {
                isPatternBasic = new PatternBasicCheckFree();;
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