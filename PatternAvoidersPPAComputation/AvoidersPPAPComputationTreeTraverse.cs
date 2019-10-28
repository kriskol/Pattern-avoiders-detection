using System.Collections.Generic;
using PatternNode;
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
            if (node.Permutation.Length + node.Parent.DescendantsDepthFromNode == depthComputed)
                ComputeStepProcessChildren(node, result);
            else
                ComputeStepDefault(node, result);
        }
    }
}