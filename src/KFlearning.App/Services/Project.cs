using System;
using System.Text.Json.Serialization;
using KFlearning.Core.Helpers;
using KFlearning.TemplateProvider;

namespace KFlearning.App.Services
{
    public class Project
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastOpenAt { get; set; }

        [JsonIgnore]
        public ITemplateProvider Template { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Path.TrimLongText()})";
        }
    }
}