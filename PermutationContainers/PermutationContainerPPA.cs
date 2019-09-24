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

        protected void CorrectPositionsTop(PatternBasic positions)
        {
            int length = positions.Length;
            ulong positionCorrect = positions.Get(positions.LowestPosition + length-1);
            List<int> positionsBeCorrected = new List<int>();

            int numPositionsCorrected = Math.Min(lengthLongestPermutationAvoided, length);
            
            for(int i = 0; i < numPositionsCorrected-1; i++)
                if(positions.Get(positions.LowestPosition + length-i-2) >= positionCorrect)
                    positionsBeCorrected.Add(positions.LowestPosition + length-i-2);
            
            positions.Change(positionsBeCorrected, 1);
        }

        protected List<PermutationContainerPPA> ComputeSuccessorsDuplicate
        (IPermutationDictionary<ExtensionMap> collection,
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
                    break;
                newPermutationPositions = PermutationPositions.InsertLetter((ulong)position);
                CorrectPositionsTop(newPermutationPositions);
                newExtensionMap = exMapComputationUnsorted.Compute(collection, newPermutation,
                    newPermutationPositions, minimumLettersConsidered);
                newMinimumLettersConsidered = minimumLettersBeChecked.
                    GetMinimumLettersConsidered(collection, newPermutation,
                        newPermutationPositions,maximumLettersConsidered);

                permutationContainerPPA = new PermutationContainerPPA(newPermutation, newPermutationPositions,
                    minimumLettersBeChecked, exMapComputationUnsorted,
                    newExtensionMap, lengthLongestPermutationAvoided,
                    newMinimumLettersConsidered);
                
                containers.Add(permutationContainerPPA);
            }

            return containers;
        }

        protected void CorrectExtensionMap(Permutation newPermutation, ExtensionMap extensionMap, 
                                            IPermutationsCollection avoidedPermutations)
        {
            for(int i = 0; i < extensionMap.Length; i++)
                if(extensionMap.Get(i) == true)
                    if(avoidedPermutations.Contains(newPermutation.InsertLetter(((ulong)i))))
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
                    break;
                newPermutationPositions = PermutationPositions.InsertLetter((ulong)position);
                CorrectPositionsTop(newPermutationPositions);
                
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