using System;
using System.Text.Json.Serialization;

namespace KFlearning.Core.API
{
    public class FlutterGitTag
    {
        [JsonPropertyName("ref")]
        public string Ref { get; set; }

        [JsonPropertyName("node_id")]
        public string NodeId { get; set; }

        [JsonPropertyName("url")]
        public Uri Url { get; set; }
    }
}
