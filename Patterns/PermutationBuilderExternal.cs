using NumericalSequences;

namespace Patterns
{
    public class PermutationBuilderExternal : PatternBuilderExternal<Permutation, IPermutationBuilderExternal>, 
                                                IPermutationBuilderExternal
    {
        public PermutationBuilderExternal() : base()
        {
            patternFactory = new Permutation.PermutationFactory();
        }
        
        public PermutationBuilderExternal(INumSequenceBasicFactoryExternal factoryNumSequence,
                                            IBasicWsBuilderFactory factoryBuilder,
                                            IPatternFactory<Permutation> patternFactory)
                                            :base(factoryNumSequence, factoryBuilder, patternFactory)
        {
            this.patternFactory = patternFactory;
        }

        public override IPermutationBuilderExternal Clone()
        {
            return new PermutationBuilderExternal();
        }
    }
}