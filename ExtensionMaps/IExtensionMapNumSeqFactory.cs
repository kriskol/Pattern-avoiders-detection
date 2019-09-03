using NumericalSequences;

namespace ExtensionMaps
{
    public interface IExtensionMapNumSeqFactory
    {
        ExtensionMapNumSeq GetExtensionMapNumSeq(NumSequenceExtended numSequenceExtended);
    }
}