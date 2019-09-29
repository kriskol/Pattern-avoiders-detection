namespace PatternNode
{
    public class PatternNodePPABuilderFactoryCreator : IPatternNodePPABuilderFactoryCreator
    {
        public IPatternNodePPABuilderFactory GetBuilderFactory()
        {
            return new PatternNodePPABuilder();
        }
    }
}