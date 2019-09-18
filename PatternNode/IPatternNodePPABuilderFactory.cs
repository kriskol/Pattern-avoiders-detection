using ExtensionMaps;
using PermutationContainers;
using PermutationsCollections;

namespace PatternNode
{
    public interface IPatternNodePPABuilderFactory : IPatternNodeBuilderFactory<PatternNodePPA>
    {
        void SetContainerPPA(PermutationContainerPPA containerPpa);
        void SetExtensionMapsDescendants(IPermutationDictionary<ExtensionMap> extensionMapsDescendants);
        
        bool TryGetBuilder(out IPatternNodePPABuilder builder);
    }
}