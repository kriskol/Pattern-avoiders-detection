namespace Patterns
{
    public interface IPatternBasicBuilderAdvancedAttributes : IPatternBuilderAdvancedAttributes
    {
        bool MaximalLengthSet { get; }
        int MaximalLength { get; }
    }
}