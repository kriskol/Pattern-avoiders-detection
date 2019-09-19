namespace Patterns
{
    public interface IPermutationFactoryExternal
    {
        Permutation CreatePermutation(ulong[] words, int length, byte letterSize);
        
        Permutation CreatePermutation(ulong[] words, int length, byte letterSize, 
            IPermutationBuilderAdvancedAttributes attributes);
       
        Permutation CreatePermutation(string[] letters, byte letterSize);
        
        Permutation CreatePermutation(string[] letters, byte letterSize, 
                                        IPermutationBuilderAdvancedAttributes attributes);

    }
}