using System.Diagnostics;

namespace KFmaintenance.Services
{
    internal static class WindowsHelpers
    {
        public static void Shutdown()
        {
            Process.Start("shutdown -s -t 0");
        }
    }
}
