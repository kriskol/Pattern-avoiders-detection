namespace NumericalSequences
{
    public class NumSequenceFactory
    {
        protected ulong[] CreteDefaultWords(byte letterSize, int length, bool set)
        {

            int countWords = length*letterSize / 64;
            if (length * letterSize % 64 != 0 || length == 0)
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

            if (letterSize * length % 64 == 0 || length == 0)
                words[countWords - 1] = word;
            else
                words[countWords - 1] = word & (ulong) (((ulong) 1 << ((letterSize * length) % 64)) - (ulong) 1);

            return words;
        }
    }
}