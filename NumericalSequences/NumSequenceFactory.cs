namespace NumericalSequences
{
    public class NumSequenceFactory
    {
        protected ulong[] CreteDefaultWords(byte letterSize, int length, bool set)
        {
            int countWords = length*letterSize / 64;
            if (length * letterSize % 64 != 0)
                countWords++;
            
            ulong[] words = new ulong[countWords];
            ulong word;

            if (set)
                word = ulong.MaxValue;
            else
                word = 0;
            
            for (int j = 0; j < countWords-1; j++)
            {
                words[j] = word;
            }

            words[countWords - 1] = word % ((ulong)1 << ((letterSize * length) % 64));

            return words;
        }
    }
}