namespace Mappers
{
    internal static class ToBoolean
    {
        public static bool ToBool(this string str)
        {
            return str == "True";
        }
    }
}
