using System.Diagnostics.Tracing;

namespace NumericalSequences
{
    public interface INumSequenceFactory<T> where T: NumSequenceBaseC<T>
    {
        T GetNumSequenceDefault(byte letterSize, int length, bool set);
        T GetNumSequence(ulong[] words, byte letterSize, int length);
        T GetNumSequence(ulong[] words, byte letterSize, int length, int maximalLength);
        T GetNumSequence(ulong[] words, byte letterSize, int length, int maximalLength, int suffixLength);
    }
}