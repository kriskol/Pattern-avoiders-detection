namespace Patterns
{
    public interface IPermutationFactoryExternal
    {
        Permutation CreatePermutation(ulong[] words, int length, int letterSize);
        Permutation CreatePermutation(string[] letters, int letterSize);
    }
}