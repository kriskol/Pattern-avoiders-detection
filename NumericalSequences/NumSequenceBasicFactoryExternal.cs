namespace NumericalSequences
{
    public class NumSequenceBasicFactoryExternal : INumSequenceBasicFactoryExternal
    {
        public NumSequenceBasic CreateNumSequenceBasic(INumSequenceBasicWsBuilder builder)
        {
            return new NumSequenceBasicWs(builder);
        }
    }
}