using System;
using System.Collections.Generic;
using NumberOperationsInterfaces;

namespace NumberOperationsImplementations
{
    public class PopCountCompute : IPopCountCompute
    {
        private byte[] lookUpTable;
        private static readonly PopCountCompute popCountCompute;
        
        private byte Compute(ulong word)
        {
            return (byte)(lookUpTable[word & 0xFFFF] + lookUpTable[(word >> 16) & 0xFFFF] + lookUpTable[(word >> 32) & 0xFFFF]
                   + lookUpTable[(word >> 48) & 0xFFFF]);
        }

        public int ComputePopCount(IEnumerable<ulong> words)
        {
            byte count = 0;

            foreach (var word in words)
            {
                count += Compute(word);
            }

            return count;
        }

        private void FillLookUpTable()
        {
            lookUpTable = new byte[65536];

            for (int i = 0; i < 65536; i++)
            {
                int number = i;
                byte count = 0;

                for (count = 0; number > 0; count++)
                {
                    number &= number - 1;
                }

                lookUpTable[i] = count;
            }
        }

        private PopCountCompute()
        {
            FillLookUpTable();
        }

        static PopCountCompute()
        {
            popCountCompute = new PopCountCompute();
        }

        public class PopCountTableComputeFactory : IPopCountFactory
        {
            public IPopCountCompute GetPopCountCompute()
            {
                return popCountCompute;
            }
        }
    }
}