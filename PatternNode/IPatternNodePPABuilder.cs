using ExtensionMaps;
using PermutationContainers;
using PermutationsCollections;

namespace PatternNode
{
    public interface IPatternNodePPABuilder : IPatternNodeBuilder<PatternNodePPA>
    {
        bool ExtensionMapsDescendantsSet { get; }

        PermutationContainerPPA ContainerPPA { get; }
        IPermutationDictionary<ExtensionMap> ExtensionMapsDescendants { get; } 
    }
}