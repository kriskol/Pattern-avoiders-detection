using System.Collections.Generic;
using ExtensionMaps;
using GeneralInterfaces;
using Patterns;
using PermutationsCollections;

namespace ExtensionMapsComputations
{
    /*
    public class ExMapComputationUnsorted : IExMapComputationUnsorted
    {
        private static ExtensionMapFactoryNum extensionMapFactoryNum;
        
        public ExtensionMap Compute(IPermutationDictionary<ExtensionMap> permExMaps, Permutation permutation,
                                    IPosition positions, int lettersConsidered)
        {
            ExtensionMap extensionMapDefault = extensionMapFactoryNum.GetExtensionMapDefault(permutation.Length+1);

            if (lettersConsidered <= 1)
                return extensionMapDefault;
            
            ExtensionMap[] extensionMaps = new ExtensionMap[lettersConsidered-1];

            int positionDeleted = positions.GetPosition(permutation.Length - 2);
            int positionMaximum = positions.GetPosition(permutation.Length - 1);
            Permutation newPermutation = permutation.Delete(positionDeleted,
                                                            new int[] {positionMaximum});
            permExMaps.TryGetValue(newPermutation, out ExtensionMap extensionMap);
            extensionMap = extensionMap.Insert(positionDeleted, extensionMap.Get(positionDeleted));
            extensionMaps[0] = extensionMap;

            int positionTo = positionDeleted;
            int positionFrom;
            for (int i = 1; i < lettersConsidered-1; i++)
            {
                positionFrom = positions.GetPosition(permutation.Length - i - 2);
                newPermutation = newPermutation.Switch(positionFrom, positionTo);
                permExMaps.TryGetValue(newPermutation, out extensionMap);
                extensionMap.Insert(positionFrom, extensionMap.Get(positionFrom));
                extensionMaps[i] = extensionMap;
                positionTo = positionFrom;
            }

            return extensionMapDefault.And(extensionMaps);
        }

        static ExMapComputationUnsorted()
        {
            extensionMapFactoryNum = new ExtensionMapFactoryNum();
        }
    }
    */
    
    public class ExMapComputationUnsorted : IExMapComputationUnsorted
    {
        private static ExtensionMapFactory _extensionMapFactory;
        
        public ExtensionMap Compute(IPermutationDictionary<ExtensionMap> permExMaps, Permutation permutation,
            IPosition positions, int lettersConsidered)
        {
            ExtensionMap extensionMapDefault = _extensionMapFactory.GetExtensionMapDefault(permutation.Length+1);

            if (lettersConsidered <= 0)
                return extensionMapDefault;
            
            ExtensionMap[] extensionMaps = new ExtensionMap[lettersConsidered];

            int positionTo = positions.GetPosition(permutation.Length - 1);
            Permutation newPermutation = permutation.Delete(positionTo, new int[] {});
            permExMaps.TryGetValue(newPermutation, out ExtensionMap extensionMap);
            extensionMap = extensionMap.Insert(positionTo, extensionMap.Get(positionTo));
            extensionMaps[0] = extensionMap;
            
            int positionFrom;
            bool positionDiminished = false;
            for (int i = 0; i < lettersConsidered-1; i++)
            {
                positionFrom = positions.GetPosition(permutation.Length - i - 2);
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
            _extensionMapFactory = new ExtensionMapFactory();
        }
    }
}