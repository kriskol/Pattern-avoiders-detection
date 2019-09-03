namespace BoolExtension
{
    public static class BoolExtensions
    {
        public static byte ToByte(this bool b)
        {
            if (b) return 1;
            
            return 0;
        }
    }
}