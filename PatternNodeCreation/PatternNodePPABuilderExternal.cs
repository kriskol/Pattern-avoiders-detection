using System.Collections.Generic;
using ExtensionMaps;
using Patterns;
using PermutationContainers;
using PermutationsCollections;

namespace PatternNode
{
    public class PatternNodePPABuilderExternal : IPatternNodePPABuilderExternal
    {
        private IPatternNodePPABuilderFactory factory;
        private IPermutationContainerPPAFactory containerPpaFactory;
        
        public void SetParent(PatternNodePPA parent)
        {
            factory.SetParent(parent);
        }

        public void SetChildren(List<PatternNodePPA> children)
        {
            factory.SetChildren(children);
        }

        public void SetCountChildren(int count)
        {
            factory.SetCountChildren(count);
        }

        public void SetDescendants(List<PatternNodePPA>[] descendants)
        {
            factory.SetDescendants(descendants);
        }

        public void SetExtensionMapsDescendants(IPermutationDictionary<ExtensionMap> extensionMapsDescendants)
        {
            factory.SetExtensionMapsDescendants(extensionMapsDescendants);
        }
        
        public void Reset()
        {
            factory.Reset();
        }

        public PatternNodePPA CreateNode(Permutation permutation, PatternBasic positions, ExtensionMap extensionMap)
        {
            PermutationContainerPPA containerPpa = containerPpaFactory.
                                                    CreatePermutation(permutation, positions, extensionMap);
            factory.SetContainerPPA(containerPpa);

            IPatternNodePPABuilder builder;

            factory.TryGetBuilder(out builder);
            
            return new PatternNodePPA(builder);
        }

        public PatternNodePPABuilderExternal()
        {
            factory = new PatternNodePPABuilder();
            containerPpaFactory = new PermutationContainerPPAFactory();
        }

        public PatternNodePPABuilderExternal(IPatternNodePPABuilderFactory factory,
                                                IPermutationContainerPPAFactory containerPpaFactory)
        {
            this.factory = factory;
            this.containerPpaFactory = containerPpaFactory;
        }
    }
}