using System.Collections.Generic;

namespace NumericalSequences
{
    internal class NumSequenceBasicWs : NumSequenceBasic
    {
        private ulong[] words;

        protected override ulong[] Words
        {
            get { return words;}
            set { words = value; }
        }
        
        public NumSequenceBasicWs(INumSequenceBasicWsBuilder builder) : base(builder)
        {
            words = builder.Words;
        }
    }
}