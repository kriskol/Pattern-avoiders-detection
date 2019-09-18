using System.Collections.Generic;
using ArrayExtensions;
using NumberOperationsInterfaces;

namespace NumericalSequences 
{
    public abstract class NumSequenceExtensionIm<T>:NumSequenceExtension<T> where T:NumSequenceExtensionIm<T>
    {

        protected abstract ICtzCompute CtzCompute { get; }
        protected abstract IPopCountCompute PopCountCompute { get; }
        public override IEnumerable<int> Ctz()
        {
            return CtzCompute.ComputeCtz(Words);
        }

        public override int PopCount()
        {
            return PopCountCompute.ComputePopCount(Words);
        }

        public override int PopCount(int fromGivenPosition)
        {
            ConvertPosition(fromGivenPosition, out int index, out byte positionWord, out int offset);
            ulong[] newArray = Words.Slice(index, Words.Length);
            newArray[0] = newArray[0] >> (offset + positionWord * LetterSize);
            return PopCountCompute.ComputePopCount(newArray);
        }

        public override T And(T arg)
        {
            if (arg == null)
                return (T) this;
                
            return And(arg.Words);
        }
        public override T And(T[] args)
        {
            if (args.Length == 0)
                return (T) this;

            ulong[][] words = new ulong[args.Length][];
            for (int i = 0; i < args.Length; i++)
            {
                words[i] = args[i].Words;
            }

            return And(words);
        }
        public override T And(ulong[] arg)
        {
            ulong[] newWords = Words;
            for (int i = 0; i < arg.Length; i++)
                newWords[i] &= arg[i];

            return CreateNumSequenceThisProp(newWords);
        }
        public override T And(ulong[][] args)
        {
            ulong[] newWords = Words;

            for (int i = 0; i < newWords.Length; i++)
                foreach (var arg in args)
                    newWords[i] &= arg[i];

            return CreateNumSequenceThisProp(newWords);
        }
        
        protected NumSequenceExtensionIm(INumSequenceBaseBuilder builder) : base(builder)
        {
        }
        
    }
}