using System.Collections.Generic;
using PatternNode;
using PermutationSuccessorsComputation;
using Result;

namespace PatternAvoidersPPAComputation
{
    public class AvoidersPPAAComputationTreeTraverse : AvoidersPPAComputationTreeTraverse
    {
        private IPermSuccessorsComputation permSuccessorsComputation;

        protected void ComputeStepProcessPermutations(PatternNodePPA node, ResultPPA result)
        {
            PatternNodePPA parent = node.Parent;
            List<PatternNodePPA> nodes;
            List<PatternNodePPA>[] descendants;
            parent.TryGetDescendants(out descendants);
            
            nodes = descendants[node.PositionPrecedingLetters(node.Permutation.Length - 1)];
            
            foreach (var nodeProcessed in nodes)
                result.ProcessPermutations(permSuccessorsComputation.Successors(nodeProcessed.Permutation, 
                                                    nodeProcessed.ExtensionMap, 
                                                    avoidedPermutations));
        }
        
        protected override void ComputeStep(PatternNodePPA node, ResultPPA result)
        {
            if (node.Permutation.Length + node.Parent.DescendantsDepthFromNode == depthComputed)
                ComputeStepProcessPermutations(node, result);
            else
                ComputeStepDefault(node, result);
        }

        public AvoidersPPAAComputationTreeTraverse()
        {
            permSuccessorsComputation = new PermSuccessorsComputation();
        }
        public AvoidersPPAAComputationTreeTraverse(IPermSuccessorsComputation permSuccessorsComputation)
        {
            this.permSuccessorsComputation = permSuccessorsComputation;
        }
    }
}