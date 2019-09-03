using System.Collections;
using System.Collections.Generic;
using NumberOperationsInterfaces;

namespace NumberOperationsImplementations
{
    public class CtzCompute : ICtzCompute
    {
        private byte[] lookUpTable;
        private static readonly CtzCompute ctzCompute;

        private void InitializeLookUpTable()
        {
            lookUpTable = new byte[67];

            lookUpTable[0] = 64;

            uint number = 1;

            for (byte i = 1; i < 65; i++)
            {
                lookUpTable[number % 67] = (byte)(i - 1);
            }
        }

        private byte Compute(ulong word)
        {
            return lookUpTable[((((~word)+1) & word) % 67)];
        }

        private IEnumerable<int> Compute(IEnumerable<ulong> words)
        {
            int position = 0;
            int remainder;
            byte ctz;
            
            foreach (var word in words)
            {
                remainder = 64;
                while (true)
                {
                    ctz = Compute(word);

                    if (ctz == 64)
                    {
                        position += remainder;
                        break;
                    }
                    else
                    {
                        position = position + ctz + 1;
                        remainder = remainder - ctz - 1;
                        yield return position;
                    }
                }
            }
        }
        
        
        public IEnumerable<int> ComputeCtz(IEnumerable<ulong> words)
        {

            return Compute(words);
        }

        private CtzCompute()
        {
            InitializeLookUpTable();
        }

        static CtzCompute()
        {
            ctzCompute = new CtzCompute();
        }

        public class CtzTableComputeFactory: ICtzFactory
        {
            public ICtzCompute GetCtzCompute()
            {
                return ctzCompute;
            }
        }
    }
}