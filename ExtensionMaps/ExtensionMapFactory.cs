namespace ExtensionMaps
{
    public abstract class ExtensionMapFactory : IExtensionMapFactory
    {
        public abstract ExtensionMap GetExtensionMap(ulong[] words, int length);
        
        public abstract ExtensionMap GetExtensionMapDefault(int length, bool set);

    }
}