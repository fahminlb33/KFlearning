// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : UpdateCheckClient.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KFlearning.Core.API
{
    public interface IUpdateCheckClient
    {
        Task<UpdateDefinition> GetLatestVersion();
    }

    public class UpdateCheckClient : IUpdateCheckClient
    {
        private const string BaseUri = "https://api.github.com";

        private static HttpClient Client = new HttpClient();

        static UpdateCheckClient()
        {
            Client.DefaultRequestHeaders.Add("User-Agent", "KFlearning/1.0");
        }

        public async Task<UpdateDefinition> GetLatestVersion()
        {
            const string uri = BaseUri + "/repos/fahminlb33/KFlearning/releases/latest";
            using (var response = await Client.GetAsync(uri))
            {
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var release = JsonConvert.DeserializeObject<ReleaseResponse>(content, Helpers.SerializeSettings);
                if (release.Assets.Count == 0)
                    return null;

                var asset = release.Assets.First();
                return new UpdateDefinition
                {
                    UpdateDate = release.PublishedAt.DateTime,
                    Description = release.Body,
                    ReleaseName = release.Name,
                    TagName = release.TagName,
                    ReleaseUrl = release.HtmlUrl.ToString(),
                    DownloadUrl = asset.BrowserDownloadUrl.ToString()
                };
            }
        }
    }
}