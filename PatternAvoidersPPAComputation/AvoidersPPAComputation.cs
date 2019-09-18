using System.Collections.Generic;
using ExtensionMaps;
using PatternNode;
using PermutationsCollections;

namespace PatternAvoidersPPAComputation
{
    public abstract class AvoidersPPAComputation
    {
        protected PermutationDictionary<ExtensionMap> ExtractExtensionMaps(List<PatternNodePPA> nodes)
        {
            PermutationDictionary<ExtensionMap> extensionMaps = new PermutationDictionary<ExtensionMap>();
            foreach (var node in nodes)
                extensionMaps.Add(node.Permutation, node.ExtensionMap);

            return extensionMaps;
        }
    }
}