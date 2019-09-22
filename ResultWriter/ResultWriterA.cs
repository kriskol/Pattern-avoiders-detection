using System;
using System.Collections.Generic;
using System.Linq;
using Patterns;
using Result;

namespace ResultWriter
{
    public class ResultWriter : IResultWriter
    {
        private void OutputStart()
        {
            Console.WriteLine("## OUTPUT START ##");
        }

        private void OutputEnd()
        {
            Console.WriteLine("## OUTPUT END ##");
        }

        private void CountPermutationsStart()
        {
            Console.WriteLine("== COUNT PERMUTATIONS == length : count ==");
        }

        private void CountPermutationsEnd()
        {
            Console.WriteLine("== COUNT PERMUTATIONS ==");
        }
        
        private void CountPermutations(int length, int count)
        {
            Console.WriteLine(length+" : "+count);
        }

        private void PermutationsListStart()
        {
            Console.WriteLine("== PERMUTATION LIST START ==");
        }

        private void PermutationListEnd()
        {
            Console.WriteLine(" PERMUTATION LIST END");
        }

        private void PermutationListLength(int length)
        {
            Console.WriteLine("-- PERMUTATIONS LENGTH : "+length);
        }

        private void PermutationListCountGivenLength(int count)
        {
            Console.WriteLine("++ Count : "+ count);
        }
        
        private void PermutationWrite(Permutation permutation)
        {
            Console.WriteLine(permutation);
        }

        public void Visit(ResultPPAA result)
        {
            Dictionary<int, List<Permutation>> storedData = result.StoredData;
            List<Permutation> permutations;
            int maximum = storedData.Keys.Max();
            
            OutputStart();
            PermutationsListStart();
            
            for (int i = 1; i <= maximum; i++)
                if (storedData.ContainsKey(i))
                {
                    permutations = storedData[i];
                    PermutationListLength(i);
                    PermutationListCountGivenLength(permutations.Count);
                    foreach (var permutation in permutations)
                        PermutationWrite(permutation);
                }
                else
                {
                    PermutationListLength(i);
                    PermutationListCountGivenLength(0);
                }

            PermutationListEnd();
            OutputEnd();
        }

        public void Visit(ResultPPAP result)
        {
            Dictionary<int, int> storedData = result.StoredData;
            int maximum = storedData.Keys.Max();
            
            OutputStart();
            CountPermutationsStart();

            for (int i = 1; i <= maximum; i++)
                if (storedData.ContainsKey(i))
                    CountPermutations(i, storedData[i]);
                else
                    CountPermutations(i,0);
            
            CountPermutationsEnd();
            OutputEnd();
        }
    }
}