// SOLUTION : KFlearning
// PROJECT  : KFlearning
// FILENAME : ProjectService.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

#region

using System.IO;
using KFlearning.Core.Services;
using Newtonsoft.Json;

#endregion

namespace KFlearning.Services
{
    public interface IProjectService
    {
        Project Load(string path);
        void Create(Project project);
        void Launch(Project project);
        bool IsExists(Project project);
        bool IsExists(string path);
        string GetPathForProject(string title, string basePath = null);
    }

    public class ProjectService : IProjectService
    {
        private const string MetadataFileName = "kf-learning.json";

        private readonly IVisualStudioCodeService _visualStudioCodeService;
        private readonly ITemplateService _template;
        private readonly IPathManager _path;

        public ProjectService(IVisualStudioCodeService visualStudioCodeService, IPathManager path,
            ITemplateService template)
        {
            _visualStudioCodeService = visualStudioCodeService;
            _path = path;
            _template = template;
        }

        public Project Load(string path)
        {
            var content = File.ReadAllText(Path.Combine(path, MetadataFileName));
            var metadata = JsonConvert.DeserializeObject<Project>(content);

            // synchronize saved path
            metadata.Path = path;
            Save(metadata);

            return metadata;
        }

        public void Create(Project project)
        {
            if (IsExists(project))
            {
                Directory.Delete(project.Path, true);
            }

            Directory.CreateDirectory(project.Path);
            _template.Extract(project.Template, project.Path);
            Save(project);
        }

        public void Launch(Project project)
        {
            _visualStudioCodeService.OpenFolder(project.Path);
        }

        public bool IsExists(Project project)
        {
            return IsExists(project.Path);
        }

        public bool IsExists(string path)
        {
            return File.Exists(Path.Combine(path, MetadataFileName));
        }

        public string GetPathForProject(string title, string basePath = null)
        {
            return Path.Combine(basePath ?? _path.GetPath(PathKind.DefaultProjectRoot),
                _path.StripInvalidPathName(title));
        }

        private void Save(Project project)
        {
            var savePath = Path.Combine(project.Path, MetadataFileName);
            File.WriteAllText(savePath, JsonConvert.SerializeObject(project));
        }
    }
}