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

        public virtual void DisposeChildren()
        {
            childrenSet = false;
            children = null;
        }

        public virtual void DisposeDescendants()
        {
            descendantsSet =false;
            descendants = null;
        }

        public virtual int DescendantsDepthFromNode => descendantsSet ? descendantsLevelFromNode : -1;

        public virtual int CountChildren => countChildrenSet ? countChildren : -1;

        public virtual bool TryGetChildren(out List<T> children)
        {
            if (childrenSet)
            {
                children = this.children;
                return true;
            }
            else
            {
                children = null;
                return false;
            }
        }

        public virtual bool TryGetDescendants(out List<T>[] descendants)
        {
            if (descendantsSet)
            {
                descendants = this.descendants;
                return true;
            }
            else
            {
                descendants = null;
                return false;
            }
        }

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