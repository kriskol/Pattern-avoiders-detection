using System.Collections.Generic;
using GeneralInterfaces;
using LogicInterfaces;
using NumberOperationsInterfaces;

namespace NumericalSequences
{
    public abstract class NumSequenceExtension<T>: NumSequenceBaseIm<T>, ICtz, ILogicAnd<T>, 
                                                    IPopCount where T:NumSequenceExtension<T>
    {
        public abstract IEnumerable<int> Ctz();
        public abstract T And(T arg);
        public abstract T And(T[] args);
        public abstract int PopCount();
        public abstract int PopCount(int fromGivenPosition);
        public abstract T And(ulong[] arg);
        public abstract T And(ulong[][] args);
    }
}