using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KFlearning.Core.API
{
    public interface ITelemetryClient
    {
        Task SendTelemetry(UserEngagementModel model);
        Task SendIdentification(DeviceIdentificationModel model);
    }

    public class TelemetryClient : ITelemetryClient
    {
        private static HttpClient Client = new HttpClient();

        static TelemetryClient()
        {
            Client.DefaultRequestHeaders.Add("X-API-Key", AppRes.ApiKey);
        }

        public async Task SendIdentification(DeviceIdentificationModel model)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"))
            {
                var result = await Client.PostAsync(AppRes.ApiBaseUri + "/telemetry/device", content);
                result.EnsureSuccessStatusCode();
            }
        }

        public async Task SendTelemetry(UserEngagementModel model)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"))
            {
                var result = await Client.PostAsync(AppRes.ApiBaseUri + "/telemetry/intent", content);
                result.EnsureSuccessStatusCode();
            }
        }
    }
}