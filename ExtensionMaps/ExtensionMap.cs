using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using GeneralInterfaces;
using LogicInterfaces;
using NumberOperationsInterfaces;

namespace ExtensionMaps
{
    
    public abstract class ExtensionMap: ILogicAnd<ExtensionMap>, ICloneable<ExtensionMap>, 
                                        ICtz, IPopCount, IEquatable<ExtensionMap>
    {
        public abstract ExtensionMap Set(int position, bool letter);
        public abstract bool Get(int position);
        public abstract ExtensionMap Insert(int position, bool letter);
        public abstract ExtensionMap Delete(int position);
        public abstract ulong[] Words { get; }
        public abstract int Length { get; }
        
        public abstract ExtensionMap And(ExtensionMap arg);
        public abstract ExtensionMap And(ExtensionMap[] args);
        public abstract IEnumerable<int> Ctz();
        public abstract int PopCount();
        public abstract int PopCount(int fromGivenPosition);

        public abstract ExtensionMap Clone();

        public abstract override string ToString();
        public abstract override int GetHashCode();
        public override bool Equals(object obj)
        {
            return (Equals(obj as ExtensionMap));
        }
        public bool Equals(ExtensionMap other)
        {
            if (other == null)
                return false;
            return (other.Length == Length && Words.SequenceEqual(other.Words));
        }
    }
}