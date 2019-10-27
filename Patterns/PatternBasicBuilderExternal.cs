using GeneralInterfaces;
using NumericalSequences;

namespace Patterns
{
    public class PatternBasicBuilderExternal : PatternBuilderExternal<PatternBasic, IPatternBasicBuilderExternal>, 
                                        IPatternBasicBuilderExternal
    {
        protected IPatternFactory<PatternBasic> patternFactory;

        public void SetMaximalLength(int maximalLength)
        {
            factoryBuilder.SetMaximalLength(maximalLength, 0);
        }
                    
        public PatternBasicBuilderExternal() : base()
        {
            patternFactory = new PatternBasic.PatternBasicFactory();
        }
                                                                
        public PatternBasicBuilderExternal(INumSequenceBasicFactoryExternal factoryNumSequence,
                                            IBasicWsBuilderFactory factoryBuilder,
                                            IPatternFactory<PatternBasic> patternFactory)
                                            :base(factoryNumSequence, factoryBuilder)
        {
            this.patternFactory = patternFactory;
        }

        protected override PatternBasic ConstructPattern(NumSequenceBasic numSequenceBasic, int highestPosition, int maximum)
        {
            return patternFactory.GetPattern(numSequenceBasic, highestPosition);
        }

        public override IPatternBasicBuilderExternal Clone()
        {
            return new PatternBasicBuilderExternal();
        }
    }
}