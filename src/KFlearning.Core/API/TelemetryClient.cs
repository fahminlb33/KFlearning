// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : TelemetryClient.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KFlearning.Core.API
{
    public interface ITelemetryClient
    {
        Task SendAppStart(string appName, string deviceId);
        Task SendAppExit(string appName, string deviceId);
        Task SendIdentification(string deviceId, string cpu, double ram, string os, string bitness);
    }

    public class TelemetryClient : ITelemetryClient
    {
        private const string BaseUri = "https://kflearning.kodesiana.com";
        private static HttpClient Client = new HttpClient();

        static TelemetryClient()
        {
            Client.DefaultRequestHeaders.Add("X-API-Key", ApplicationConstants.ApiKey);
        }

        public async Task SendAppStart(string appName, string deviceId)
        {
            await SendTelemetry(appName, "started", deviceId);
        }

        public async Task SendAppExit(string appName, string deviceId)
        {
            await SendTelemetry(appName, "exit", deviceId);
        }

        public async Task SendIdentification(string deviceId, string cpu, double ram, string os, string bitness)
        {
            var body = new
            {
                deviceId,
                cpu,
                os,
                ram,
                bitness
            };

            using (var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"))
            {
                var result = await Client.PostAsync(BaseUri + "/telemetry/device", content);
                result.EnsureSuccessStatusCode();
            }
        }

        private async Task SendTelemetry(string appName, string appEvent, string deviceId)
        {
            var body = new
            {
                deviceId,
                appName,
                intent = appEvent
            };

            using (var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"))
            {
                var result = await Client.PostAsync(BaseUri + "/telemetry/intent", content);
                result.EnsureSuccessStatusCode();
            }
        }
    }
}