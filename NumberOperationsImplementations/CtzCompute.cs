﻿using System.Collections.Generic;
using NumberOperationsInterfaces;

namespace NumberOperationsImplementations
{
    public class CtzComputeTable : ICtzCompute
    {
        private int[] lookUpTable;
        private static readonly CtzComputeTable ctzComputeTable;
        
        private void InitializeLookUpTable()
        {
            lookUpTable = new int[67];

            lookUpTable[0] = 64;

            ulong number = 1;

            for (int i = 1; i < 65; i++)
            {
                lookUpTable[number % 67] = i - 1;
                number <<= 1;
            }
        }

        private int Compute(ulong word)
        {
            return lookUpTable[((((~word)+1) & word) % 67)];
        }

        private IEnumerable<int> Compute(IEnumerable<ulong> words)
        {
            ulong processedWord = 0;
            int position = -1;
            int remainder;
            int ctz;

            foreach (var word in words)
            {
                processedWord = word;
                remainder = 64;
                while (true)
                {
                    ctz = Compute(processedWord);

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

                    if (ctz + 1 == 64)
                        processedWord = 0;
                    else 
                        processedWord =  (processedWord >> (ctz + 1));
                }
            }
        }
        
        
        public IEnumerable<int> ComputeCtz(IEnumerable<ulong> words)
        {

            return Compute(words);
        }

        private CtzComputeTable()
        {
            InitializeLookUpTable();
        }

        static CtzComputeTable()
        {
            ctzComputeTable = new CtzComputeTable();
        }

        public static CtzComputeTable GetCtzComputeTable()
        {
            return ctzComputeTable;
        }

        public class CtzTableComputeFactory: ICtzFactory
        {
            public static CtzComputeTable GetCtzComputeTable()
            {
                return ctzComputeTable;
            }
            public ICtzCompute GetCtzCompute()
            {
                return ctzComputeTable;
            }
        }
    }
}