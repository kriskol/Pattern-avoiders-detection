using System.Collections.Generic;
using System.Linq;
using ExtensionMaps;
using PatternNode;
using PermutationsCollections;
using Result;
using System.Threading;
using System.Threading.Tasks;

namespace PatternAvoidersPPAComputation
{
    public abstract class AvoidersPPAComputationTreeTraverse : AvoidersPPAComputation, IAvoidersPPAComputationTreeTraverse
    {

        protected int depthComputed;
        protected IPermutationsCollection avoidedPermutations;

        protected void SetFields(int depthComputed, IPermutationsCollection avoidedPermutations)
        {
            this.depthComputed = depthComputed;
            this.avoidedPermutations = avoidedPermutations;
        }
        
        //patternNodeDispose
        //resultClonesDispose
        
        protected void InsertDescendant(List<PatternNodePPA>[] descendants, PatternNodePPA node,
                                        int index)
        {
            descendants[index].Add(node);
        }
        
        protected void InsertDescendants(List<PatternNodePPA>[] descendants, List<PatternNodePPA> nodes,
                                            int index)
        {
            descendants[index].AddRange(nodes);
        }
        
        protected void AddDescendants(List<PatternNodePPA>[] descendants, List<PatternNodePPA> nodes,
                                        int depth, int letter)
        {
            if (nodes.Count == 0)
                return;

            if (depth <= 1)
                foreach (var node in nodes)
                    InsertDescendant(descendants, node, node.PositionPreceedingLetters(letter));
            else
            {
                int index = nodes[0].PositionPreceedingLetters(letter);
                InsertDescendants(descendants, nodes, index);
            }
        }

        protected void ComputeStepDefault(PatternNodePPA node, ResultPPA result)
        {
            PatternNodePPA parent = node.Parent;
            IPermutationDictionary<ExtensionMap> extensionMaps;
            parent.TryGetExtensionMapsDescendants(out extensionMaps);
            List<PatternNodePPA>[] descendants;
            parent.TryGetDescendants(out descendants);
            List<PatternNodePPA> nodes = descendants[node.PositionPreceedingLetters( node.Permutation.Length-1)];

            IPermutationDictionary<ExtensionMap> newExtensionMaps;
            List<PatternNodePPA>[] newDescendants = new List<PatternNodePPA>[node.Permutation.Length+1];
            List<PatternNodePPA> newNodes;
            List<PatternNodePPA> allNodes = new List<PatternNodePPA>();
            
            foreach (var nodeProcessed in nodes)
            {
                newNodes = nodeProcessed.ComputeChildren(extensionMaps, avoidedPermutations);
                allNodes.AddRange(newNodes);
                AddDescendants(descendants, newNodes, parent.DescendantsDepthFromNode, 
                    node.Permutation.Length);
            }
            
            result.ProcessNodes(allNodes);
            newExtensionMaps = ExtractExtensionMaps(allNodes);
            
            node.SetDescendants(newDescendants, parent.DescendantsDepthFromNode);
            node.SetExtensionMapsDescendants(newExtensionMaps);
        }

        protected abstract void ComputeStep(PatternNodePPA node, ResultPPA result);

        protected int[] ComputeThreadsCountChildren(int countChildren, int numThreads)
        {
            int[] numThreadsChildren = new int[countChildren];
            int eachChild = numThreads / countChildren;
            int remainder = numThreads % countChildren;
            
            for (int i = 0; i < countChildren; i++)
            {
                numThreadsChildren[i] = eachChild;

                if (remainder > i)
                    numThreadsChildren[i]++;
            }

            return numThreadsChildren;
        }

        protected int[] DivideThreadsChildren(int countChildren, int numThreads)
        {
            int[] childrenPerThread = new int[numThreads];
            int eachThread = countChildren / numThreads;
            int remainder = countChildren % numThreads;

            for (int i = 0; i < childrenPerThread.Length; i++)
            {
                childrenPerThread[i] = eachThread;

                if (i > remainder)
                    childrenPerThread[i]++;
            }

            return childrenPerThread;
        }
        
        protected void ComputeParallel(PatternNodePPA node, ResultPPA result, int numThreads)
        {
            ComputeStep(node, result);
            ComputeParallelHandler(node, result, numThreads);
        }
        
        protected void ComputeParallelSufficientThreads(PatternNodePPA node,
                                                ResultPPA result, int numThreads)
        {
            int[] numThreadsChildren = ComputeThreadsCountChildren(node.CountChildren, numThreads);
            Thread[] threads = new Thread[node.CountChildren-1];
            List<PatternNodePPA> children;
            node.TryGetChildren(out children);
            
            for (int i = 0; i < threads.Length; i++)
                threads[i] = new Thread(()=>ComputeParallel(children[i], 
                                         result.GetClone(), numThreadsChildren[i]));
            foreach (var thread in threads)
                thread.Start();
            
            ComputeParallel(children[children.Count-1],
                            result.GetClone(), numThreadsChildren[children.Count-1]);

            foreach (var thread in threads)
                thread.Join();
            
            result.FetchDataAllClones();
            result.DisposeClones();
        }

        protected void ComputeParallelUnsufficientThreadsChildrenProcessing
            (List<PatternNodePPA> children, int startIndex, int length, 
            ResultPPA result)
        {
            for (int i = startIndex; i < startIndex + length; i++)
            {
                ComputeStep(children[i], result);
                ComputeNonParallel(children[i], result);
            }
        }
        
        protected void ComputeParallelUnSufficientThreads(PatternNodePPA node,
                                                ResultPPA result, int numThreads)
        {
            int[] childrenPerThread = DivideThreadsChildren(node.CountChildren, numThreads);
            Thread[] threads = new Thread[childrenPerThread.Length-1];
            List<PatternNodePPA> children;
            node.TryGetChildren(out children);

            int startIndex = 0;

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() => ComputeParallelUnsufficientThreadsChildrenProcessing(children,
                    startIndex, childrenPerThread[i], result.GetClone()));

                startIndex += childrenPerThread[i];
            }

            foreach (var thread in threads)
                thread.Start();
            
            ComputeParallelUnsufficientThreadsChildrenProcessing(children, 
                startIndex, childrenPerThread[childrenPerThread.Length-1], 
                result.GetClone());

            foreach (var thread in threads)
                thread.Join();
            
            result.FetchDataAllClones();
            result.DisposeClones();
        }
        
        protected void ComputeParallelHandler(PatternNodePPA node, ResultPPA result, int numThreads)
        {
            if (depthComputed > node.Permutation.Length + node.DescendantsDepthFromNode)
            {

                if (numThreads == 1)
                    ComputeNonParallel(node, result);

                if (numThreads < node.CountChildren)
                    ComputeParallelUnSufficientThreads(node,  result, numThreads);
                else
                    ComputeParallelSufficientThreads(node, result, numThreads);
                
                node.DisposeChildren();
                node.DisposeDescendants();
            }
        }

        protected void ComputeNonParallel(PatternNodePPA node, ResultPPA result)
        {
            if (depthComputed > node.Permutation.Length + node.DescendantsDepthFromNode)
            {
                List<PatternNodePPA> children;
                node.TryGetChildren(out children);

                foreach (var child in children)
                {
                    ComputeStep(child, result);
                    ComputeNonParallel(child, result);
                }
                
                node.DisposeChildren();
                node.DisposeDescendants();
            }
        }

        
        public ResultPPA Compute(PatternNodePPA node, IPermutationsCollection avoidedPermutations,
            int maximalDepthComputed, ResultPPA result, int numThreads)
        {
            SetFields(maximalDepthComputed, avoidedPermutations);
            ComputeParallelHandler(node, result, numThreads);

            return result;
        }


    }
}