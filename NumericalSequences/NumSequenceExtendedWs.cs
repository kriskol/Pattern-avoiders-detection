namespace NumericalSequences
{
    internal class NumSequenceExtendedWs : NumSequenceExtended
    {
        private ulong[] words;

        protected override ulong[] Words
        {
            get { return words; }
            set { words = value; }
        }
        
        public NumSequenceExtendedWs(INumSequenceExtendedWsBuilder builder) : base(builder)
        {
            words = builder.Words;
        }
    }
}