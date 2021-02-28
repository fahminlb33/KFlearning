using System.Reflection;

namespace KFlearning.Core.Extensions
{
    public static class PathHelpers
    {
        public static string GetVersionString()
        {
            var version = Assembly.GetCallingAssembly().GetName().Version;
            return $"v{version.Major}.{version.Minor}.{version.Build} build \"{AppRes.Codename}\"";
        }

        public static string TrimLongText(this string path, int maxLength = 40)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "";
            }

            return path.Length <= maxLength ? path : path.Substring(0, maxLength) + "...";
        }
    }
}