using System.Net;

namespace KFlearning.Core.Extensions
{
    public static class NetworkHelpers
    {
        public static void EnableTls()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
        }
    }
}
