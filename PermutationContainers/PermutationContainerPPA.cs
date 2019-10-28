using System;
using System.Collections.Generic;
using  ExtensionMaps;
using ExtensionMapsComputations;
using Patterns;
using PermutationsCollections;

namespace PermutationContainers
{
    public class PermutationContainerPPA : PermutationContainerPositions<PatternBasic>
    {
        protected ExtensionMap extensionMap;
        protected int lengthLongestPermutationAvoided;
        protected int minimumLettersConsidered;
        protected int countSuccessors;
        protected bool countSuccessorsComputed;
        protected readonly IMinimumLettersChecked minimumLettersBeChecked;
        protected readonly IExMapComputationUnsorted exMapComputationUnsorted;
        public ExtensionMap ExtensionMap => extensionMap;

        public override int CountSuccessors
        {
            get
            {
                if (countSuccessorsComputed)
                    return countSuccessors;
                else
                {
                    countSuccessorsComputed = true;
                    countSuccessors = extensionMap.PopCount();
                    return countSuccessors;
                }
            }
        }

        protected PatternBasic CorrectPositionsTop(PatternBasic positions)
        {
            int length = positions.Length;
            int positionCorrect = positions.Get(positions.LowestPosition + length-1);
            List<int> positionsBeCorrected = new List<int>();

            int numPositionsCorrected = Math.Min(lengthLongestPermutationAvoided, length);
            
            for(int i = 0; i < numPositionsCorrected-1; i++)
                if(positions.Get(positions.LowestPosition + length-i-2) >= positionCorrect)
                    positionsBeCorrected.Add(positions.LowestPosition + length-i-2);
            
            return positions.ChangePositive(positionsBeCorrected, 1);
        }
        
        protected void CorrectExtensionMap(Permutation newPermutation, ExtensionMap extensionMap, 
                                            IPermutationsCollection avoidedPermutations)
        {
            for(int i = 0; i < extensionMap.Length; i++)
                if(extensionMap.Get(i))
                    if(avoidedPermutations.Contains(newPermutation.InsertPosition(i)))
                        extensionMap.SetMutable(i, false);
        }
        
        protected List<PermutationContainerPPA> ComputeSuccessors(IPermutationDictionary<ExtensionMap> collection,
                                                                        IPermutationsCollection avoidedPermutations,
                                                                        bool checkAvoidedPermutations)
        {
            Permutation newPermutation;
            PatternBasic newPermutationPositions;
            ExtensionMap newExtensionMap;
            int newMinimumLettersConsidered;
            List<PermutationContainerPPA> containers = new List<PermutationContainerPPA>();
            PermutationContainerPPA permutationContainerPPA;
            
            int maximumLettersConsidered = Math.Min(lengthLongestPermutationAvoided, Permutation.Length + 1);
            
            foreach (var position in extensionMap.Ctz())
            {
                newPermutation = Permutation.InsertPosition(position);
                
                if(checkAvoidedPermutations && avoidedPermutations.Contains(newPermutation))
                    continue;
                newPermutationPositions = PermutationPositions.InsertLetter(position);
                newPermutationPositions = CorrectPositionsTop(newPermutationPositions);
                
                newMinimumLettersConsidered = minimumLettersBeChecked.
                    GetMinimumLettersConsidered(collection, newPermutation,
                        newPermutationPositions,maximumLettersConsidered);
                
                newExtensionMap = exMapComputationUnsorted.Compute(collection, newPermutation,
                    newPermutationPositions, newMinimumLettersConsidered);
                
                if(checkAvoidedPermutations)
                    CorrectExtensionMap(newPermutation, newExtensionMap, avoidedPermutations);
                

                permutationContainerPPA = new PermutationContainerPPA(newPermutation, newPermutationPositions,
                    minimumLettersBeChecked, exMapComputationUnsorted,
                    newExtensionMap, lengthLongestPermutationAvoided,
                    newMinimumLettersConsidered);
                
                containers.Add(permutationContainerPPA);
            }

            return containers;
        }

        public List<PermutationContainerPPA> GetSuccessors(IPermutationDictionary<ExtensionMap> collection,
                                                                IPermutationsCollection avoidedPermutations)
        {
            if (Permutation.Length <= lengthLongestPermutationAvoided)
                return ComputeSuccessors(collection, avoidedPermutations, true);
            else
                return ComputeSuccessors(collection, avoidedPermutations, false);
        }
        
        public PermutationContainerPPA(Permutation permutation, PatternBasic permutationPositions,
            IMinimumLettersChecked minimumLettersBeChecked,
            IExMapComputationUnsorted exMapComputationUnsorted,
            ExtensionMap extensionMap,
            int lengthLongestPermutationAvoided, int minimumLettersConsidered)
            : this(permutation, permutationPositions,
                minimumLettersBeChecked, exMapComputationUnsorted,
                extensionMap, lengthLongestPermutationAvoided)
        {
            this.minimumLettersConsidered = minimumLettersConsidered;
        }
        
        public PermutationContainerPPA(Permutation permutation, PatternBasic permutationPositions,
                                        IMinimumLettersChecked minimumLettersBeChecked, 
                                        IExMapComputationUnsorted exMapComputationUnsorted,
                                        ExtensionMap extensionMap, int lengthLongestPermutationAvoided) 
                                        :base(permutation, permutationPositions)
        {
            minimumLettersConsidered = Math.Min(permutation.Length, lengthLongestPermutationAvoided);
            countSuccessorsComputed = false;
            this.minimumLettersBeChecked = minimumLettersBeChecked;
            this.exMapComputationUnsorted = exMapComputationUnsorted;
            this.extensionMap = extensionMap;
            this.lengthLongestPermutationAvoided = lengthLongestPermutationAvoided;
        }
        
        
    }
}