using System.Collections.Generic;
using PatternNode;
using PermutationsCollections;
using Result;

namespace PatternAvoidersPPAComputation
{
    public class AvoidersPPAPComputationSequential : AvoidersPPAComputationSequential

    {
        protected override PatternNodePPA ComputeMaximalDepth(PatternNodePPA node, 
                                                            IPermutationsCollection avoidedPermutations, 
                                                            int depthComputed, ResultPPA result)
        {
            List<PatternNodePPA> nodes = Compute(node, avoidedPermutations,depthComputed-1, result);

            foreach (var nodeProcessed in nodes)
                result.TryProcessNodeChildren(nodeProcessed);

            return node;
        }
    }
}