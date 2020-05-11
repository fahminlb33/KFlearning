// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : ReleaseResponse.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KFlearning.Core.API.Model
{
    public class ReleaseResponse
    {
        [JsonProperty("html_url")] public Uri HtmlUrl { get; set; }

        [JsonProperty("tag_name")] public string TagName { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("prerelease")] public bool Prerelease { get; set; }

        [JsonProperty("draft")] public bool Draft { get; set; }

        [JsonProperty("published_at")] public DateTimeOffset PublishedAt { get; set; }

        [JsonProperty("assets")] public List<AssetResponse> Assets { get; set; }

        [JsonProperty("body")] public string Body { get; set; }
    }

    public class AssetResponse
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("browser_download_url")] public Uri BrowserDownloadUrl { get; set; }
    }
}