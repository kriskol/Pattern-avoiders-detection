using System.Collections.Generic;

namespace NumberOperationsInterfaces
{
    public interface IPopCountCompute
    {
        int ComputePopCount(IEnumerable<ulong> words);
    }
}