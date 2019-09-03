using System.Text;
using NumericalSequences;

namespace Patterns
{
    public class PatternBasic : PatternIm<PatternBasic>
    {

        private readonly int lowestPosition;
        private readonly int highestPosition;
        private static IPatternFactory<PatternBasic> patternFactory;
        
        public override int LowestPosition => lowestPosition;
        public override int HighestPosition => highestPosition;
        protected override IPatternFactory<PatternBasic> PatternFactory => patternFactory;
        
        public PatternBasic(NumSequenceBasic numSequenceBasic, int lowestPosition,
                            int highestPosition, int maximum) : base(numSequenceBasic, maximum)
        {
            this.lowestPosition = lowestPosition;
            this.highestPosition = highestPosition;
        }

        public class PatternBasicFactory : IPatternFactory<PatternBasic>
        {
            private IIsPatternBasic isPatternBasic;
            public PatternBasic GetPattern(NumSequenceBasic numSequenceBasic, int lowestPosition, 
                                            int highestPosition, int maximum)
            {
                if(isPatternBasic.IsPatternBasic(numSequenceBasic))
                    return  new PatternBasic(numSequenceBasic, lowestPosition, highestPosition, maximum);

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