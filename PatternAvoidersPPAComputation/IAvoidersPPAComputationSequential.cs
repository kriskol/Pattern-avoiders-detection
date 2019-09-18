using PatternNode;
using PermutationsCollections;
using Result;

namespace PatternAvoidersPPAComputation
{
    public interface IAvoidersPPAComputationSequential
    {
        PatternNodePPA Compute(IPermutationsCollection avoidedPermutations, 
                                int maximalLengthAvoiders, int maximalDepthComputed, ResultPPA result);
    }
}