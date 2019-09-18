using System;
using NumericalSequences;

namespace ExtensionMaps
{
    public class ExtensionMapNumSeqFactory : ExtensionMapFactory, IExtensionMapNumSeqFactory
    {
        private NumSequenceExtendedFactory factory;
        
        public ExtensionMapNumSeq GetExtensionMapNumSeq(NumSequenceExtended numSequenceExtended)
        {
            if (numSequenceExtended != null && numSequenceExtended.LetterSize == 1)
                return new ExtensionMapNumSeq(numSequenceExtended);
                
            if(numSequenceExtended == null)
                throw new ArgumentNullException();
            else
                throw new ArgumentException();
                
        }

        public override ExtensionMap GetExtensionMap(ulong[] words, int length)
        {
            NumSequenceExtended numSequenceExtended = factory.GetNumSequence(words, 1, length);
            return new ExtensionMapNumSeq(numSequenceExtended);
        }

        public override ExtensionMap GetExtensionMapDefault(int length, bool set)
        {
            NumSequenceExtended numSequenceExtended = factory.GetNumSequenceDefault(1, length, true);
            return new ExtensionMapNumSeq(numSequenceExtended);
        }

        public ExtensionMapNumSeqFactory()
        {
            factory = new NumSequenceExtendedFactory();
        }
    }
}