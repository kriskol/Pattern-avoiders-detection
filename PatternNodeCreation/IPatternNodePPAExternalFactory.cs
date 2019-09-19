using System.Collections.Generic;
using ExtensionMaps;
using Patterns;
using PermutationContainers;
using PermutationsCollections;

namespace PatternNode
{
    public interface IPatternNodePPAExternalFactory
    {
        PatternNodePPA GetPatternNodePpa(Permutation permutation, PatternBasic positions, 
                                            ExtensionMap extensionMap);
        PatternNodePPA GetPatternNodePpa(Permutation permutation, PatternBasic positions, 
                                            ExtensionMap extensionMap, PatternNodePPA parent);
        PatternNodePPA GetPatternNodePpa(Permutation permutation, PatternBasic positions, 
                                            ExtensionMap extensionMap, PatternNodePPA parent,
                                            List<PatternNodePPA> children);
        PatternNodePPA GetPatternNodePpa(Permutation permutation, PatternBasic positions,
                                            ExtensionMap extensionMap, PatternNodePPA parent,
                                            List<PatternNodePPA> children,
                                            List<PatternNodePPA>[] descendants,
                                            IPermutationDictionary<ExtensionMap> extensionMapsDescendants,
                                            int depthDescendants);
    }
}