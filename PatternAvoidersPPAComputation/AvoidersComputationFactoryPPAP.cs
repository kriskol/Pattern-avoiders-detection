namespace PatternAvoidersPPAComputation
{
    public class AvoidersComputationFactoryPPAP : IAvoidersComputationAbstractFactory
    {
        public IAvoidersPPAComputationSequential GetComputationSequential()
        {
            return new AvoidersPPAPComputationSequential();
        }

        public IAvoidersPPAComputationTreeTraverse GetComputationTreeTraverse()
        {
            return new AvoidersPPAPComputationTreeTraverse();
        }
    }
}