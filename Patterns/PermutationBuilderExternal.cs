using NumericalSequences;

namespace Patterns
{
    public class PermutationBuilderExternal : PatternBuilderExternal<Permutation, IPermutationBuilderExternal>, 
                                                IPermutationBuilderExternal
    {
        protected IPatternFactoryMax<Permutation> patternFactory;

        public PermutationBuilderExternal()
        {
            patternFactory = new Permutation.PermutationFactory();
        }
        
        public PermutationBuilderExternal(INumSequenceBasicFactoryExternal factoryNumSequence,
                                            IBasicWsBuilderFactory factoryBuilder,
                                            IPatternFactoryMax<Permutation> patternFactory)
                                            :base(factoryNumSequence, factoryBuilder)
        {
            this.patternFactory = patternFactory;
        }

        protected override Permutation ConstructPattern(NumSequenceBasic numSequenceBasic, int highestPosition, int maximum)
        {
            return patternFactory.GetPattern(numSequenceBasic, highestPosition, maximum);
        }

        public override IPermutationBuilderExternal Clone()
        {
            return new PermutationBuilderExternal();
        }
    }
}