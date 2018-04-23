using System.Diagnostics;

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

        public static string GetFileVersionString(string file = null)
        {
            return GetFileVersion(file).ToString();
        }

        public static FileVersionInfo GetFileVersion(string file = null)
        {
            if (Extension.IsNullOrWhiteSpace(file)) file = System.Reflection.Assembly.GetEntryAssembly().Location;

            return FileVersionInfo.GetVersionInfo(file);
        }
    }
}
