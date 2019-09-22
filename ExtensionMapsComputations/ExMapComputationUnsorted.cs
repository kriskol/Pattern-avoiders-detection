using System.Collections.Generic;
using ExtensionMaps;
using GeneralInterfaces;
using Patterns;
using PermutationsCollections;

namespace ExtensionMapsComputations
{
    public class ExMapComputationUnsorted : IExMapComputationUnsorted
    {
        private static IExtensionMapFactory extensionMapFactory;
        
        public ExtensionMap Compute(IPermutationDictionary<ExtensionMap> permExMaps, Permutation permutation,
            IPosition positions, int lettersConsidered)
        {
            ExtensionMap extensionMapDefault = extensionMapFactory.
                                            GetExtensionMapDefault(permutation.Length+1, true);

            if (lettersConsidered <= 0)
                return extensionMapDefault;
            
            ExtensionMap[] extensionMaps = new ExtensionMap[lettersConsidered];

            int positionTo = positions.GetPosition(positions.LowestPosition +  permutation.Length - 1);
            Permutation newPermutation = permutation.Delete(positionTo, new int[] {});
            permExMaps.TryGetValue(newPermutation, out ExtensionMap extensionMap);
            extensionMap = extensionMap.Insert(positionTo, extensionMap.Get(positionTo));
            extensionMaps[0] = extensionMap;
            
            int positionFrom;
            bool positionDiminished = false;
            for (int i = 0; i < lettersConsidered-1; i++)
            {
                positionFrom = positions.GetPosition(positions.LowestPosition + permutation.Length - i - 2);
                if (positionFrom >= positionTo)
                {
                    positionFrom--;
                    positionDiminished = true;
                }
                newPermutation = newPermutation.Switch(positionFrom, positionTo);
                if (positionDiminished)
                {
                    positionFrom++;
                }
                permExMaps.TryGetValue(newPermutation, out extensionMap);
                extensionMap = extensionMap.Insert(positionFrom, extensionMap.Get(positionFrom));
                extensionMaps[i+1] = extensionMap;
                positionTo = positionFrom;
                positionDiminished = false;
            }

            return extensionMapDefault.And(extensionMaps);
        }

        static ExMapComputationUnsorted()
        {
            extensionMapFactory = new ExtensionMapNumSeqFactory();
        }
    }
}