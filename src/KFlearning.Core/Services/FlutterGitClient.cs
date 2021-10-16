using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

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
            
            using var bodyReader = await response.Content.ReadAsStreamAsync();
            var tags = await JsonSerializer.DeserializeAsync<List<FlutterGitTag>>(bodyReader);

            var latest = tags?.LastOrDefault(x => !x.Ref.Contains("pre"));
            if (latest == null)
            {
                throw new KFlearningException("Tidak dapat menemukan versi Flutter! Silakan download manual.");
            }

            return GetVersionFromTag(latest.Ref);
        }

        public string GetFlutterDownloadUri(string version)
        {
            return $"https://storage.googleapis.com/flutter_infra_release/releases/stable/windows/flutter_windows_{version}-stable.zip";
        }

        private static string GetVersionFromTag(string tag)
        {
            return tag.Split('/').Last();
        }
    }
}
