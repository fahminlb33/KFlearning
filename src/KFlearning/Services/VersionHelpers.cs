using System;
using System.Linq;
using System.Reflection;
using KFlearning.Core.API;

namespace KFlearning.Services
{
    internal static class VersionHelpers
    {
        public static bool IsNewerVersion(UpdateDefinition definition)
        {
            if (definition == null) return false;

            var versionArray = definition.TagName.Split('.').Select(x => Convert.ToInt32(x)).ToArray();
            var currentVersion = Assembly.GetEntryAssembly().GetName().Version;
            return currentVersion.Major > versionArray[0] && currentVersion.Minor > versionArray[1];
        }
    }
}
