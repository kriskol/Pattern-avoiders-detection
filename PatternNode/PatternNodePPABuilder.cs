using ExtensionMaps;
using PermutationContainers;
using PermutationsCollections;

namespace PatternNode
{
    public class PatternNodePPABuilder : PatternNodeBuilder<PatternNodePPA>, IPatternNodePPABuilder, IPatternNodePPABuilderFactory
    {
        private bool containerPpaSet;
        private PermutationContainerPPA containerPpa;
        private bool extensionMapsDescendantsSet;
        private IPermutationDictionary<ExtensionMap> extensionMapsDescendants;

        public bool ExtensionMapsDescendantsSet => extensionMapsDescendantsSet;
        public PermutationContainerPPA ContainerPPA => containerPpa;
        public IPermutationDictionary<ExtensionMap> ExtensionMapsDescendants => extensionMapsDescendants;


        public PatternNodePPABuilder(PermutationContainerPPA containerPpa)
        {
            Reset();
            this.containerPpa = containerPpa;
        }

        public PatternNodePPABuilder()
        {
            Reset();
        }

        public void SetContainerPPA(PermutationContainerPPA containerPpa)
        {
            containerPpaSet = true;
            this.containerPpa = containerPpa;
        }

        public void SetExtensionMapsDescendants(IPermutationDictionary<ExtensionMap> extensionMapsDescendants)
        {
            extensionMapsDescendantsSet = true;
            this.extensionMapsDescendants = extensionMapsDescendants;
        }

        public bool TryGetBuilder(out IPatternNodePPABuilder builder)
        {
            if (containerPpaSet)
            {
                builder = this;
                return true;
            }
            else
            {
                builder = null;
                return false;
            }
        }

        public override void Reset()
        {
            base.Reset();
            containerPpaSet = false;
        }
    }
}