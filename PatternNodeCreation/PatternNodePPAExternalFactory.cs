using System.Collections.Generic;
using ExtensionMaps;
using Patterns;
using PermutationsCollections;

namespace PatternNode
{
    public class PatternNodePPAExternalFactory : IPatternNodePPAExternalFactory
    {
        public PatternNodePPA GetPatternNodePpa(Permutation permutation, PatternBasic positions, 
                                                ExtensionMap extensionMap)
        {
            
        }

        public PatternNodePPA GetPatternNodePpa(Permutation permutation, PatternBasic positions, 
                                                ExtensionMap extensionMap, PatternNodePPA parent)
        {
            throw new System.NotImplementedException();
        }

        public PatternNodePPA GetPatternNodePpa(Permutation permutation, PatternBasic positions,
                                                ExtensionMap extensionMap, PatternNodePPA parent, 
                                                List<PatternNodePPA> children)
        {
            throw new System.NotImplementedException();
        }

        public PatternNodePPA GetPatternNodePpa(Permutation permutation, PatternBasic positions, ExtensionMap extensionMap,
                                                PatternNodePPA parernt, List<PatternNodePPA> children, 
                                                List<PatternNodePPA>[] descendants, 
            IPermutationDictionary<ExtensionMap> extensionMapsDescendants,
            int depthDescendants)
        {
            throw new System.NotImplementedException();
        }
    }
}