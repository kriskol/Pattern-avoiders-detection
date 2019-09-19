using System;
using System.Collections.Generic;
using ExtensionMaps;
using PatternNode;
using Patterns;
using PermutationContainers;
using PermutationsCollections;
using Result;

namespace PatternAvoidersPPAComputation
{
    public abstract class AvoidersPPAComputationSequential : AvoidersPPAComputation, IAvoidersPPAComputationSequential
    {
        protected virtual PatternNodePPA GetStartingNode(IPermutationsCollection avoidedPermutations, int n)
        {
            IPatternNodePPABuilderFactory nodePpaBuilderFactory = new PatternNodePPABuilder();
            IPatternNodePPABuilder nodePpaBuilder;
            IPermutationContainerPPAFactory containerPpaFactory = 
                                                new PermutationContainerPPAFactory(avoidedPermutations);
            IPermutationBuilderExternal permutationBuilderExternal = new PermutationBuilderExternal();
            IPatternBasicBuilderExternal patternBasicBuilderExternal = new PatternBasicBuilderExternal();
            IExtensionMapFactory extensionMapFactory = new ExtensionMapNumSeqFactory();
            Permutation permutation;
            PatternBasic patternBasic;
            ExtensionMap extensionMap;
            PermutationContainerPPA permutationContainer;
            
            int maxValue = Math.Max(n, avoidedPermutations.LengthLongestPermutation);
            byte letterSize = (byte)(int)(Math.Ceiling(Math.Log(maxValue,2))) - 1);
            
            permutationBuilderExternal.SetSuffixLength(avoidedPermutations.LengthLongestPermutation);
            permutation = permutationBuilderExternal.CreatePattern(new ulong[] {0}, letterSize, 0);
            
            patternBasicBuilderExternal.SetMaximalLength(avoidedPermutations.LengthLongestPermutation);
            patternBasic = patternBasicBuilderExternal.CreatePattern(new ulong[] {0}, letterSize, 0);
            
            
            if(avoidedPermutations.Contains(permutation.Insert(0,0)))
                extensionMap = extensionMapFactory.GetExtensionMapDefault(1, true);
            else
                extensionMap = extensionMapFactory.GetExtensionMapDefault(1, false);

            permutationContainer = containerPpaFactory.CreatePermutation(permutation, patternBasic, extensionMap);
            
            nodePpaBuilderFactory.SetContainerPPA(permutationContainer);
            nodePpaBuilderFactory.TryGetBuilder(out nodePpaBuilder);

            return new PatternNodePPA(nodePpaBuilder);
        }

        protected List<PatternNodePPA> Compute(PatternNodePPA node, IPermutationsCollection avoidedPermutations,
                                                int depthComputed, ResultPPA result)
        {
            List<PatternNodePPA> nodes = new List<PatternNodePPA>();
            List<PatternNodePPA> newNodes = new List<PatternNodePPA>();
            IPermutationDictionary<ExtensionMap>  extensionMaps = new PermutationDictionary<ExtensionMap>();

            nodes.Add(node);
            
            for (int i = 0; i < depthComputed; i++)
            {
                extensionMaps = ExtractExtensionMaps(nodes);

                foreach (var nodeProcessed in nodes)
                    newNodes.AddRange(nodeProcessed.ComputeChildren(extensionMaps, avoidedPermutations));

                result.ProcessNodes(newNodes);
                
                nodes = newNodes;
                newNodes = new List<PatternNodePPA>();
            }

            return nodes;
        }

        protected abstract PatternNodePPA ComputeMaximalDepth(PatternNodePPA node, 
                                                                    IPermutationsCollection avoidedPermutations, 
                                                                    int depthComputed, ResultPPA result);

        protected virtual PatternNodePPA ComputeNotMaximalDepth(PatternNodePPA node,
                                                                IPermutationsCollection avoidedPermutations,
                                                                int depthComputed, ResultPPA result)
        {
            List<PatternNodePPA> nodes = Compute(node, avoidedPermutations, depthComputed, result);
            node.SetDescendants(new List<PatternNodePPA>[] {nodes}, depthComputed);
            node.SetExtensionMapsDescendants(ExtractExtensionMaps(nodes));
            
            return node;
        }

        public virtual PatternNodePPA Compute(PatternNodePPA node, IPermutationsCollection avoidedPermutations,
            int maximalLengthAvoiders, int maximalDepthComputed, ResultPPA result)
        {
            int depth = Math.Min(maximalLengthAvoiders, maximalDepthComputed);

            if (depth == maximalDepthComputed)
                return ComputeMaximalDepth(node, avoidedPermutations, depth, result);
            else
                return ComputeNotMaximalDepth(node, avoidedPermutations, depth, result);
        }

    }
}