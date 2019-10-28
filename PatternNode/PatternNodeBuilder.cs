using System.Collections.Generic;

namespace PatternNode
{
    public abstract class PatternNodeBuilder<T>: IPatternNodeBuilderFactory<T>, IPatternNodeBuilder<T> where T: PatternNode<T>
    {
        private bool parentSet;
        private bool childrenSet;
        private bool countChildrenSet;
        private bool descendantsSet;
        private T parent;
        private List<T> children;
        private int countChildren;
        private List<T>[] descendants;
        
        
        public void SetParent(T parent)
        {
            parentSet = true;
            this.parent = parent;
        }

        public void SetChildren(List<T> children)
        {
            childrenSet = true;
            countChildrenSet = true;
            this.children = children;
            this.countChildren = children.Count;
        }

        public void SetCountChildren(int count)
        {
            countChildrenSet = true;
            this.countChildren = count;
        }

        public void SetDescendants(List<T>[] descendants)
        {
            descendantsSet = true;
            this.descendants = descendants;
        }

        public bool ParentSet => parentSet;
        public bool ChildrenSet => childrenSet;
        public bool CountChildrenSet => countChildrenSet;
        public bool DescendantsSet => descendantsSet;
        public T Parent => parentSet ? parent : null;
        public List<T> Children => childrenSet ? children : null;
        public int CountChildren => countChildrenSet ? countChildren : -1;
        public List<T>[] Descendants => descendantsSet ? descendants : null;

        public virtual void Reset()
        {
            parentSet = false;
            childrenSet = false;
            countChildrenSet = false;
            descendantsSet = false;
        }
        
        protected PatternNodeBuilder()
        {
            Reset();
        }
    }
}