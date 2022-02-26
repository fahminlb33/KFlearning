using System.Reflection;

namespace KFlearning.Core
{
    public static class KFlearningVersion
    {
        public const string Codename = "Київ / Kiev";

        public static string GetVersionString()
        {
            var version = Assembly.GetCallingAssembly().GetName().Version;
            return $"v{version.Major}.{version.Minor}.{version.Build} \"{Codename}\"";
        }
    }
}
