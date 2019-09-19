namespace Patterns
{
    public interface IPatternBasicFactoryExternal
    {
        PatternBasic CreatePatternBasic(ulong[] words, int length, byte letterSize);
        PatternBasic CreatePatternBasic(string[] letters, byte letterSize);
        PatternBasic CreatePatternBasic(ulong[] words, int length, byte letterSize, 
                                    IPatternBasicBuilderAdvancedAttributes attributes);
        PatternBasic CreatePatternBasic(string[] letters, byte letterSize, 
                                    IPatternBasicBuilderAdvancedAttributes attributes);
    }
}