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
        protected virtual PatternNodePPA GetStartingNode(IPermutationsCollection avoidedPermutations, 
                                                                int maximalLengthAvoiders)
        {
            IPatternNodePPAFactory factory = new PatternNodePPAFactory();
            
            int maxValue = Math.Max(maximalLengthAvoiders, avoidedPermutations.LengthLongestPermutation);
            byte letterSize = (byte)((int)(Math.Ceiling(Math.Log(maxValue,2))) - 1);
            
            

            return factory.CreateDefaultPatternNodePpa(letterSize, avoidedPermutations);
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

        public virtual PatternNodePPA Compute(IPermutationsCollection avoidedPermutations,
            int maximalLengthAvoiders, int maximalDepthComputed, ResultPPA result)
        {
            PatternNodePPA node = GetStartingNode(avoidedPermutations, maximalLengthAvoiders);
            
            int depth = Math.Min(maximalLengthAvoiders, maximalDepthComputed);

            if (depth == maximalDepthComputed)
                return ComputeMaximalDepth(node, avoidedPermutations, depth, result);
            else
                return ComputeNotMaximalDepth(node, avoidedPermutations, depth, result);
        }
        
    }
}