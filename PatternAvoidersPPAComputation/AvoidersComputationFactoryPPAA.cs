namespace PatternAvoidersPPAComputation
{
    public class AvoidersComputationFactoryPPAA : IAvoidersComputationAbstractFactory
    {
        public IAvoidersPPAComputationSequential GetComputationSequential()
        {
            return new AvoidersPPAAComputationSequential();
        }

        public IAvoidersPPAComputationTreeTraverse GetComputationTreeTraverse()
        {
            return new AvoidersPPAAComputationTreeTraverse();
        }
    }
}