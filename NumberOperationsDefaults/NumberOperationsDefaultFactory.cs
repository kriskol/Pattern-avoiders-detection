using NumberOperationsInterfaces;

namespace NumberOperationsImplementations
{
    public static class NumberOperationsDefaultFactory
    {
        public static ICtzCompute GetCtzCompute()
        {
            return CtzComputeTable.CtzTableComputeFactory.GetCtzComputeTable();
        }
        public static IPopCountCompute GetPopCountCompute()
        {
            return PopCountComputeTable.PopCountTableComputeFactory.GetPopCountComputeTable();
        }
    }
}