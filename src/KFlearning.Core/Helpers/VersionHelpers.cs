using System.Reflection;

namespace KFlearning.Core.Helpers
{
    public static class VersionHelpers
    {
        public const string Codename = "KevinTheDuck";

        public static string GetVersionString()
        {
            var version = Assembly.GetCallingAssembly().GetName().Version;
            return $"v{version.Major}.{version.Minor}.{version.Build}";
        }
    }
}
