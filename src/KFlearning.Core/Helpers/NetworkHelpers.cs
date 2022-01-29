using System.Diagnostics;
using System.Net;

namespace KFlearning.Core.Helpers
{
    public static class NetworkHelpers
    {
        public static void EnableTls()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
        }

        public static void LaunchUrl(string url)
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
    }
}
