using System;
using System.Collections.Generic;
using BoolExtension;
using ByteExtensions;
using NumericalSequences;

namespace ExtensionMaps
{
    public class ExtensionMapNumSeq: ExtensionMap
    {
        private static readonly IExtensionMapNumSeqFactory extensionMapFactory;
        private readonly NumSequenceExtended numSequenceExtended;
        
        public override ExtensionMap Set(int position, bool letter)
        {
           return extensionMapFactory.GetExtensionMapNumSeq(numSequenceExtended.SetLetter(position, letter.ToByte()));
        }

        public override bool Get(int position)
        {
            return numSequenceExtended.GetLetter(position).ToBool();
        }

        public override ExtensionMap Insert(int position, bool letter)
        {
           return extensionMapFactory.GetExtensionMapNumSeq(numSequenceExtended.InsertLetter(position, letter.ToByte()));
        }

        public override ExtensionMap Delete(int position)
        {
            return extensionMapFactory.GetExtensionMapNumSeq(numSequenceExtended.DeleteLetterPosition(position));
        }

        public override ulong[] Words
        {
            get
            {
                return numSequenceExtended.Words;
            }
        }
        public override int Length
        {
            get { return numSequenceExtended.Length; }
        }
        
        public override ExtensionMap And(ExtensionMap arg)
        {
            return extensionMapFactory.GetExtensionMapNumSeq(numSequenceExtended.And(arg.Words));
        }

        public override ExtensionMap And(ExtensionMap[] args)
        {
            ulong[][] words = new ulong[args.Length][];

            for (int i = 0; i < args.Length; i++)
                words[i] = args[i].Words;

            return extensionMapFactory.GetExtensionMapNumSeq(numSequenceExtended.And(words));
        }

        public override IEnumerable<int> Ctz()
        {
            return numSequenceExtended.Ctz();
        }

        public override int PopCount()
        {
            return numSequenceExtended.PopCount();
        }

        public override int PopCount(int fromGivenPosition)
        {
            return numSequenceExtended.PopCount(fromGivenPosition);
        }

        public override ExtensionMap Clone()
        {
            return extensionMapFactory.GetExtensionMapNumSeq(numSequenceExtended.Clone());
        }

        public override string ToString()
        {
            return numSequenceExtended.ToString();
        }

        public override int GetHashCode()
        {
            return numSequenceExtended.GetHashCode();
        }

        public class ExtensionMapNumSeqFactory : IExtensionMapNumSeqFactory
        {
            public ExtensionMapNumSeq GetExtensionMapNumSeq(NumSequenceExtended numSequenceExtended)
            {
                if (numSequenceExtended != null && numSequenceExtended.LetterSize == 1)
                    return new ExtensionMapNumSeq(numSequenceExtended);
                
                if(numSequenceExtended == null)
                    throw new ArgumentNullException();
                else
                    throw new ArgumentException();
                
            }
        }

        private ExtensionMapNumSeq(NumSequenceExtended numSequenceExtended)
        {
            this.numSequenceExtended = numSequenceExtended;
        }

        static ExtensionMapNumSeq()
        {
            extensionMapFactory = new ExtensionMapNumSeqFactory();
        }
    }
}