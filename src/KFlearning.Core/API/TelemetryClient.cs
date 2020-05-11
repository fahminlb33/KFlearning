// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : TelemetryClient.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace KFlearning.Core.API
{
    public interface ITelemetryClient
    {
        Task SendAppStart(DateTime dateTime, string appName, string deviceId);
        Task SendAppExit(DateTime dateTime, string appName, string deviceId);
        Task SendIdentification(string deviceId, string cpu, string ram, string os, string bitness);
    }

    public class TelemetryClient : ITelemetryClient
    {
        private const string BaseUri = "https://kflearning.kodesiana.com";
        private readonly CultureInfo _culture = new CultureInfo("en-US");
        private static HttpClient Client = new HttpClient();

        static TelemetryClient()
        {
            Client.DefaultRequestHeaders.Add("Authorization", "fahmi-kodesiana");
        }

        public async Task SendAppStart(DateTime dateTime, string appName, string deviceId)
        {
            await SendTelemetry(dateTime, appName, "started", deviceId);
        }

        public async Task SendAppExit(DateTime dateTime, string appName, string deviceId)
        {
            await SendTelemetry(dateTime, appName, "exit", deviceId);
        }

        public async Task SendIdentification(string deviceId, string cpu, string ram, string os, string bitness)
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(deviceId), "device_id");
                content.Add(new StringContent(cpu), "cpu");
                content.Add(new StringContent(ram), "ram");
                content.Add(new StringContent(os), "os");
                content.Add(new StringContent(bitness), "bitness");

                var result = await Client.PostAsync(BaseUri + "/api/identification.php", content);
                result.EnsureSuccessStatusCode();
            }
        }

        private async Task SendTelemetry(DateTime dateTime, string appName, string appEvent, string deviceId)
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(dateTime.ToUnixTime().ToString(_culture)), "timestamp");
                content.Add(new StringContent(deviceId), "device_id");
                content.Add(new StringContent(appName), "app_name");
                content.Add(new StringContent(appEvent), "event");

                var result = await Client.PostAsync(BaseUri + "/api/telemetry.php", content);
                result.EnsureSuccessStatusCode();
            }
        }
    }
}