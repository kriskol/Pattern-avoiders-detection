using PermutationsCollections;

namespace PatternNode
{
    public interface IPatternNodePPAFactory
    {
        PatternNodePPA CreateDefaultPatternNodePpa(byte letterSize, IPermutationsCollection avoidedPermutations);

        PatternNodePPA CreatePatternNodePpa(IPatternNodePPABuilderFactory nodePpaBuilderFactory);
    }
}