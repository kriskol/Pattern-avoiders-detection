using NumericalSequences;

namespace Patterns
{
    public class PatternBasicBuilderExternal : PatternBuilderExternal<PatternBasic>, IPatternBasicBuilderExternal
    {
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
                                            :base(factoryNumSequence, factoryBuilder, patternFactory)
        {
        }
    }
}