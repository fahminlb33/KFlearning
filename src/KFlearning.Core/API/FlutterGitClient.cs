using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KFlearning.Core.API
{
    public interface IFlutterGitClient
    {
        string GetFlutterDownloadUri(string version);
        Task<string> GetLatestFlutterVersion();
    }

    public class FlutterGitClient : IFlutterGitClient
    {
        public const string DefaultFlutterVersion = "1.22.6";

        private static readonly HttpClient Client;

        static FlutterGitClient()
        {
            var version = Assembly.GetCallingAssembly().GetName().Version;

            Client = new HttpClient();
            Client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            Client.DefaultRequestHeaders.Add("User-Agent", $"{version.Major}.{version.Minor}.{version.Build}");
        }

        public async Task<string> GetLatestFlutterVersion()
        {
            var response = await Client.GetAsync("https://api.github.com/repos/flutter/flutter/git/refs/tags");
            response.EnsureSuccessStatusCode();

            var serializer = new JsonSerializer();
            using (var bodyReader = new StreamReader(await response.Content.ReadAsStreamAsync(), Encoding.UTF8))
            using (var jsonReader = new JsonTextReader(bodyReader))
            {
                var tags = serializer.Deserialize<List<FlutterGitTag>>(jsonReader);
                var latest = tags?.LastOrDefault(x => !x.Ref.Contains("pre"));
                if (latest == null)
                {
                    throw new KFlearningException("Tidak dapat menemukan versi Flutter! Silakan download manual.");
                }

                return GetVersionFromTag(latest.Ref);
            }
        }

        public string GetFlutterDownloadUri(string version)
        {
            return $"https://storage.googleapis.com/flutter_infra/releases/stable/windows/flutter_windows_{version}-stable.zip";
        }

        private string GetVersionFromTag(string tag)
        {
            return tag.Split('/').Last();
        }

        public class FlutterGitTag
        {
            [JsonProperty("ref")]
            public string Ref { get; set; }

            [JsonProperty("node_id")]
            public string NodeId { get; set; }

            [JsonProperty("url")]
            public Uri Url { get; set; }
        }
    }
}
