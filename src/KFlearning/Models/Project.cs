using KFlearning.Core.Extensions;
using KFlearning.TemplateProvider;
using Newtonsoft.Json;
using System;

namespace KFlearning.Models
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
            return $"{Name} ({PathHelpers.TrimLongText(Path)})";
        }
    }
}