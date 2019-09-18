namespace PatternAvoidersPPAComputation
{
    public interface IAvoidersComputationAbstractFactory
    {
        IAvoidersPPAComputationSequential GetComputationSequential();
        IAvoidersPPAComputationTreeTraverse GetComputationTreeTraverse();
    }
}