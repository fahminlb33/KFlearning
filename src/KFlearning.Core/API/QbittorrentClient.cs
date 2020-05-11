// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : QbittorrentClient.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace KFlearning.Core.API
{
    public interface IQbittorrentClient
    {
        Task AddTorrent(string path);
    }

    public class QbittorrentClient : IQbittorrentClient
    {
        private const string BaseUri = "http://localhost:8080";

        private static readonly HttpClient Client = new HttpClient();

        public async Task AddTorrent(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open))
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent("false"), "paused");
                content.Add(new StreamContent(fileStream), "torrents", "upload.torrent");

                var result = await Client.PostAsync(BaseUri + "/api/v2/torrents/add", content);
                result.EnsureSuccessStatusCode();
            }
        }
    }
}