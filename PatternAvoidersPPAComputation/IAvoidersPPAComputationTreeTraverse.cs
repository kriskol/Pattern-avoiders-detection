using PatternNode;
using PermutationsCollections;
using Result;

namespace PatternAvoidersPPAComputation
{
    public interface IAvoidersPPAComputationTreeTraverse
    {
        ResultPPA Compute(PatternNodePPA node, IPermutationsCollection avoidedPermutations,
                            int maximalDepthComputed, ResultPPA result, int numThreads, int descendantsDepth);
    }
}