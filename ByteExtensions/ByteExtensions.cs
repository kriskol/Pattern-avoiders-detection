using System.Text;

namespace ByteExtensions
{
    public static class ByteExtensions
    {
        public static bool ToBool(this byte b)
        {
            if (b == 0)
                return false;

            return true;
        }
    }
}