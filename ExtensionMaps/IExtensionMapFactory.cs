namespace ExtensionMaps
{
    public interface IExtensionMapFactory
    {
        ExtensionMap GetExtensionMap(ulong[] words, int length, int maxLength);
        ExtensionMap GetExtensionMapDefault(int length, int maxLength);
    }
}