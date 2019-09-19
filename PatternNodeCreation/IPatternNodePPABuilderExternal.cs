using ExtensionMaps;
using Patterns;
using PermutationsCollections;

namespace PatternNode
{
    public interface IPatternNodePPABuilderExternal : IPatternNodeBuilderFactory<PatternNodePPA>
    {
        void SetExtensionMapsDescendants(IPermutationDictionary<ExtensionMap> extensionMapsDescendants);

        PatternNodePPA CreateNode(Permutation permutation, PatternBasic positions, ExtensionMap extensionMap);
    }
}