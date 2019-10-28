using System.Collections.Generic;

namespace PatternNode
{
    public abstract class PatternNode<T> where T: PatternNode<T>
    {
        private bool parentSet;
        protected bool childrenSet;
        protected bool descendantsSet;
        private T parent;
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
            protected set { parent = value;
                parentSet = true;
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
        
        public void SetDescendants(List<T>[] descendants, int descendantsLevelFromNode)
        {
            this.descendantsLevelFromNode = descendantsLevelFromNode;
            this.descendants = descendants;
            descendantsSet = true;
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