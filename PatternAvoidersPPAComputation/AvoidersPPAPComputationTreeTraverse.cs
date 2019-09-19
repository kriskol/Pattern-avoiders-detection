using System.Collections.Generic;
using PatternNode;
using PermutationsCollections;
using Result;

namespace PatternAvoidersPPAComputation
{
    public class AvoidersPPAPComputationTreeTraverse : AvoidersPPAComputationTreeTraverse

    {
        protected void ComputeStepProcessChildren(PatternNodePPA node, ResultPPA result)
        {
            PatternNodePPA parent = node.Parent;
            List<PatternNodePPA>[] descendants;
            parent.TryGetDescendants(out descendants);
            List<PatternNodePPA> nodes = descendants[node.PositionPrecedingLetters(node.Permutation.Length - 1)];
            
            foreach (var nodeProcessed in nodes)
                result.TryProcessNodeChildren(nodeProcessed);
        }
        
        protected override void ComputeStep(PatternNodePPA node, ResultPPA result)
        {
            if (node.Parent.Permutation.Length + node.Parent.DescendantsDepthFromNode == depthComputed - 1)
                ComputeStepProcessChildren(node, result);
            else
                ComputeStepDefault(node, result);
        }
    }
}