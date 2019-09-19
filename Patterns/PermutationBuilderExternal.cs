using NumericalSequences;

namespace Patterns
{
    public class PermutationBuilderExternal : PatternBuilderExternal<Permutation>, IPermutationBuilderExternal
    {
        public PermutationBuilderExternal(INumSequenceBasicFactoryExternal factoryNumSequence,
                                            IBasicWsBuilderFactory factoryBuilder,
                                            IPatternFactory<Permutation> patternFactory)
                                            :base(factoryNumSequence, factoryBuilder)
        {
            this.patternFactory = patternFactory;
        }
    }
}