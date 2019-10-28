namespace IntExtensions
{
    public static class IntExtensions
    {
        public static bool ToBool(this int b)
        {
            if (b == 0)
                return false;

            return true;
        }
    }
}