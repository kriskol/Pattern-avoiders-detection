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


        public override int CountChildren 
        {
            get {
                if (!countChildrenSet)
                {
                
                    childrenSet = true;
                    countChildren = containerPPA.ExtensionMap.PopCount();
                }

                return countChildren;
            }
        }
    

        public int PositionPrecedingLetters(int letter)
        {
            return containerPPA.PositionPreceedingLetters(letter);
        }

        public override void DisposeDescendants()
        {
            base.DisposeDescendants();
            extensionMapsDescendants = null;
            extensionMapsDescendantsSet = false;
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
            
            SetChildren(childrenNodes);

            return childrenNodes;
        }

        protected void SetChildren(List<PatternNodePPA> children)
        {
            childrenSet = true;
            this.children = children;
            countChildrenSet = true;
            countChildren = children.Count;

            foreach (var child in children)
            {
                child.Parent = this;
            }
        }

        public void SetExtensionMapsDescendants(IPermutationDictionary<ExtensionMap> extensionMaps)
        {
            extensionMapsDescendantsSet = true;
            this.extensionMapsDescendants = extensionMaps;
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