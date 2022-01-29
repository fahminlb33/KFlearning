using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace KFlearning.Core.Helpers
{
    public static class PathHelpers
    {
        private static readonly char[] InvalidFileNameChars = Path.GetInvalidFileNameChars();

        public static string JsonExtension = ".json";

        public static string StripInvalidPathName(string path)
        {
            return InvalidFileNameChars.Aggregate(path, (current, x) => current.Replace(x, '_'));
        }

        public static string GetLogPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        public static string GetFullPathToEnv(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    return Path.GetFullPath(fileName);
                }

                var values = Environment.GetEnvironmentVariable("PATH");
                return values?.Split(Path.PathSeparator).Select(path => Path.Combine(path, fileName)).FirstOrDefault(File.Exists);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}