using System.Collections.Generic;

namespace PatternNode
{
    public interface IPatternNodeBuilder<T> where T:PatternNode<T>
    {
        bool ParentSet { get; }
        bool ChildrenSet { get; }
        bool CountChildrenSet { get; }
        bool DescendantsSet { get; }
        
        T Parent { get; }
        List<T> Children { get; }
        int CountChildren { get; }
        List<T>[] Descendants { get; }
    }
}