using System.Collections.Generic;
using System.Reflection;

namespace PatternNode
{
    public abstract class PatternNode<T> where T: PatternNode<T>
    {
        protected bool parentSet;
        protected bool childrenSet;
        protected bool descendantsSet;
        protected T parent;
        protected List<T> children;
        protected List<T>[] descendants;
        protected bool countChildrenSet;
        protected int countChildren;
        protected int descendantsLevelFromNode;
        
        public T Parent
        {
            get
            {
                if (parentSet)
                    return parent;
                else
                    return null;
            }
        }

        public abstract void DisposeChildren();
        public abstract void DisposeDescendants();
        
        public abstract int DescendantsDepthFromNode { get; }
        public abstract int CountChildren { get; }
        public abstract bool TryGetChildren(out List<T> children);
        public abstract bool TryGetDescendants(out List<T>[] descendants);

        protected PatternNode(IPatternNodeBuilder<T> builder)
        {
            parentSet = builder.ParentSet;
            childrenSet = builder.ChildrenSet;
            descendantsSet = builder.DescendantsSet;
            countChildrenSet = builder.CountChildrenSet;
            
            parent = builder.Parent;
            children = builder.Children;
            descendants = builder.Descendants;
            countChildren = builder.CountChildren;
        }
    }
}