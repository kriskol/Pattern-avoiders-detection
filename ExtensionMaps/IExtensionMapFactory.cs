namespace ExtensionMaps
{
    public interface IExtensionMapFactory
    {
        ExtensionMap GetExtensionMap(ulong[] words, int length);
        ExtensionMap GetExtensionMapDefault(int length, bool set);
    }
}