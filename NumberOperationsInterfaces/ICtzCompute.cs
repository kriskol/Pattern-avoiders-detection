using System.Collections.Generic;

namespace NumberOperationsInterfaces
{
    public interface ICtzCompute
    {
        IEnumerable<int> ComputeCtz(IEnumerable<ulong> words);
    } 
}