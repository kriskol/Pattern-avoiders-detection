
namespace NumericalSequences
{
    public interface INumSequenceFactory<T> where T: NumSequenceBase<T>
    {
        T GetNumSequenceDefault(byte letterSize, int length, bool set);
        T GetNumSequence(ulong[] words, byte letterSize, int length);
        T GetNumSequence(ulong[] words, byte letterSize, int length, int suffixLength);
    }
}