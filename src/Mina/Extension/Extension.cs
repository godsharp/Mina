namespace GodSharp.Mina
{
    internal static class Extension
    {
        public static bool IsNullOrWhiteSpace(string value)
        {
#if NET20 || NET35
            return string.IsNullOrEmpty(value) || value == "";
#else
            return string.IsNullOrWhiteSpace(value);
#endif
        }
    }
}
