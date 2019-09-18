using PermutationsCollections;
using Result;

namespace PatternAvoidersPPAComputation
{
    public interface IAvoidersPPAComputationHandler
    {
        ResultPPA Compute(IPermutationsCollection avoidedPatterns, int maximalLengthAvoiders, 
                            ResultPPA result, int numThreads);
    }
}