namespace NumericalSequences
{
    public class NumSequenceFactory
    {
        protected ulong[] CreteDefaultWords(byte letterSize, int length, bool set)
        {
            int countWords = length*letterSize / 64;
            ulong[] words = new ulong[countWords+1];
            ulong word;

            if (set)
                word = ulong.MaxValue;
            else
                word = 0;
            
            for (int j = 0; j <= countWords; j++)
            {
                words[j] = word;
            }

            return words;
        }
    }
}