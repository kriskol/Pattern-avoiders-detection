using System.Collections.Generic;

namespace PatternNode
{
    public interface IPatternNodeBuilderFactory<T> where T: PatternNode<T>
    {
        void SetParent(T parent);
        void SetChildren(List<T> children);
        void SetCountChildren(int count);
        void SetDescendants(List<T>[] descendants);
        void Reset();
    }
}