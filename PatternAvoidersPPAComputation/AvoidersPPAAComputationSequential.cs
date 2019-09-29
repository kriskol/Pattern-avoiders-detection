using System.Collections.Generic;
using PatternNode;
using PermutationsCollections;
using PermutationSuccessorsComputation;
using Result;

namespace PatternAvoidersPPAComputation
{
    public class AvoidersPPAAComputationSequential : AvoidersPPAComputationSequential
    {
        private IPermSuccessorsComputation successorsComputation;
        
        protected override PatternNodePPA ComputeMaximalDepth(PatternNodePPA node, 
                                                            IPermutationsCollection avoidedPermutations, 
                                                            int depthComputed, ResultPPA result)
        {
            List<PatternNodePPA> nodes = Compute(node, avoidedPermutations, depthComputed-1, result);
            
            foreach (var nodeProcessed in nodes)
                result.ProcessPermutations(
                    successorsComputation.Successors(nodeProcessed.Permutation, 
                                                        node.ExtensionMap, avoidedPermutations));
            

            return node;
        }
        
        public AvoidersPPAAComputationSequential()
        {
            successorsComputation = new PermSuccessorsComputation();
        }

        public AvoidersPPAAComputationSequential(IPermSuccessorsComputation successorsComputation)
        {
            this.successorsComputation = successorsComputation;
        }

    }
}