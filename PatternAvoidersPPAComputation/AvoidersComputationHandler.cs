using PatternNode;
using PermutationsCollections;
using Result;

namespace PatternAvoidersPPAComputation
{
    public class AvoidersComputationHandler : IAvoidersPPAComputationHandler
    {
        private IAvoidersPPAComputationSequential computationSequential;
        private IAvoidersPPAComputationTreeTraverse computationTreeTraverse;
        
        public ResultPPA Compute(IPermutationsCollection avoidedPatterns, int maximalLengthAvoiders, 
                                    ResultPPA result, int numThreads)
        {
            PatternNodePPA node = computationSequential.Compute(avoidedPatterns, maximalLengthAvoiders,
                                                avoidedPatterns.LengthLongestPermutation - 1,
                                                                    result);
            return computationTreeTraverse.Compute(node, avoidedPatterns, 
                                    maximalLengthAvoiders, result, 
                                    numThreads, avoidedPatterns.LengthLongestPermutation-1);
        }

        public AvoidersComputationHandler(IAvoidersComputationAbstractFactory factory)
        {
            this.computationSequential = factory.GetComputationSequential();
            this.computationTreeTraverse = factory.GetComputationTreeTraverse();
        }
    }
}