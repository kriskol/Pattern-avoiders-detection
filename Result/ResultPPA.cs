using System.Collections.Generic;
using PatternNode;
using Patterns;
using PermutationsCollections;

namespace Result
{
    public abstract class ResultPPA : ResultPatterns
    {
        public abstract void ProcessNode(PatternNodePPA nodePpa);
        public virtual void ProcessNodes(IEnumerable<PatternNodePPA> nodePpas)
        {
            foreach (var node in nodePpas)
                ProcessNode(node);
        }
        public abstract bool TryProcessNodeChildren(PatternNodePPA nodePpa);
        
        public abstract ResultPPA GetClone();
    }
}