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

        public void Change(IEnumerable<int> positions, int difference)
        {
            NumSequenceBasic.ChangeMutable(positions, difference);
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