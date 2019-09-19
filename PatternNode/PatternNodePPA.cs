using System.Collections.Generic;
using ExtensionMaps;
using PermutationContainers;
using Patterns;
using PermutationsCollections;

namespace PatternNode
{
    public class PatternNodePPA : PatternNode<PatternNodePPA>
    {
        private static readonly IPatternNodePPABuilderFactory builderFactory;
        private PermutationContainerPPA containerPPA;
        private IPermutationDictionary<ExtensionMap> extensionMapsDescendants;
        private bool extensionMapsDescendantsSet = false;
        
        public Permutation Permutation => containerPPA.Permutation;
        public ExtensionMap ExtensionMap => containerPPA.ExtensionMap;
        
        
        public int Position(ulong letter)
        {
            int position = containerPPA.Position((int)letter);
            for(int i = 0; i < containerPPA.
        }

        public int PositionPrecedingLetters(int letter)
        {
            containerPPA.PositionPreceedingLetters(letter);
        }

        public override int DescendantsDepthFromNode
        {
            get
            {
                if (descendantsSet)
                    return descendantsLevelFromNode;
                else
                    return 0;
            }
        }

        public override void DisposeChildren()
        {
            childrenSet = false;
            children = null;
        }

        public override void DisposeDescendants()
        {
            descendantsSet =false;
            descendants = null;
        }

        protected List<PatternNodePPA> ComputeChildren(List<PermutationContainerPPA> containerPpas)
        {
            List<PatternNodePPA> nodePpas = new List<PatternNodePPA>();
            IPatternNodePPABuilder builder;
            
            foreach (var container in containerPpas)
            {
                builderFactory.Reset();
                builderFactory.SetContainerPPA(container);
                builderFactory.TryGetBuilder(out builder);
                nodePpas.Add(new PatternNodePPA(builder));
            }
            return nodePpas;
        }
        
        public List<PatternNodePPA> ComputeChildren(IPermutationDictionary<ExtensionMap> extensionMaps, 
                                                    IPermutationsCollection permutationsAvoided)
        {
            List<PermutationContainerPPA> childrenUnfolded = 
                containerPPA.GetSuccessors(extensionMaps, permutationsAvoided);
            List<PatternNodePPA> childrenNodes;

            childrenNodes =  ComputeChildren(childrenUnfolded);
            
            childrenSet = true;
            children = childrenNodes;
            countChildrenSet = true;
            countChildren = childrenNodes.Count;

            return childrenNodes;
        }

        public void SetDescendants(List<PatternNodePPA>[] descendants, int descendantsLevelFromNode)
        {
            base.descendantsLevelFromNode = descendantsLevelFromNode;
            base.descendants = descendants;
            base.descendantsSet = true;
        }

        public void SetExtensionMapsDescendants(IPermutationDictionary<ExtensionMap> extensionMaps)
        {
            extensionMapsDescendantsSet = true;
            this.extensionMapsDescendants = extensionMaps;
        }
        
        public override bool TryGetChildren(out List<PatternNodePPA> children)
        {
            if (childrenSet)
            {
                children = base.children;
                return true;
            }
            else
            {
                children = null;
                return false;
            }
        }

        public override bool TryGetDescendants(out List<PatternNodePPA>[] descendants)
        {
            if (descendantsSet)
            {
                descendants = base.descendants;
                return true;
            }
            else
            {
                descendants = null;
                return false;
            }
                
        }

        public bool TryGetExtensionMapsDescendants(out IPermutationDictionary<ExtensionMap> extensionMapsDescendants)
        {
            if (extensionMapsDescendantsSet)
            {
                extensionMapsDescendants = this.extensionMapsDescendants;
                return true;
            }
            else
            {
                extensionMapsDescendants = null;
                return false;
            }
        }
        
        public PatternNodePPA(IPatternNodePPABuilder builder):base(builder)
        {
            containerPPA = builder.ContainerPPA;
            extensionMapsDescendantsSet = builder.ExtensionMapsDescendantsSet;
            extensionMapsDescendants = builder.ExtensionMapsDescendants;
        }

        static PatternNodePPA()
        {
            builderFactory = new PatternNodePPABuilder();
        }
    }
}