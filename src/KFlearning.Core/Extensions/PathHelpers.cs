using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace KFlearning.Core.Extensions
{
    public static class PathHelpers
    {
        private static readonly char[] InvalidFileNameChars = Path.GetInvalidFileNameChars();

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

        public static string StripInvalidPathName(string path)
        {
            return InvalidFileNameChars.Aggregate(path, (current, x) => current.Replace(x, '_'));
        }

        public static string GetFullPathToEnv(string fileName)
        {
            if (File.Exists(fileName))
            {
                return Path.GetFullPath(fileName);
            }

            var values = Environment.GetEnvironmentVariable("PATH");
            return values?.Split(Path.PathSeparator).Select(path => Path.Combine(path, fileName)).FirstOrDefault(File.Exists);
        }
    }
}