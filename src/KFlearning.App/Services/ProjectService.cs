﻿using System;
using System.IO;
using KFlearning.Core.Helpers;
using KFlearning.Core.Services;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace KFlearning.App.Services
{
    public interface IProjectService
    {
        Project Load(string path);
        void Create(Project project);
        void Launch(Project project);
        bool IsExists(Project project);
        bool IsExists(string path);
        string GetPathForProject(string title, string? basePath = null);
    }

    public class ProjectService : IProjectService
    {
        private const string MetadataFileName = "kf-learning.json";

        private readonly IVisualStudioCodeService _visualStudioCodeService;
        private readonly ITemplateService _template;
        private readonly IPathManager _path;
        private readonly ILogger<ProjectService> _logger;

        public ProjectService(IVisualStudioCodeService visualStudioCodeService, IPathManager path,
            ITemplateService template, ILogger<ProjectService> logger)
        {
            _visualStudioCodeService = visualStudioCodeService;
            _path = path;
            _template = template;
            _logger = logger;
        }

        public Project Load(string path)
        {
            using var projectStream = File.OpenRead(Path.Combine(path, MetadataFileName));
            var metadata = JsonSerializer.Deserialize<Project>(projectStream)!;

            metadata.Path = path;
            metadata.LastOpenAt = DateTime.Now;

            _logger.LogDebug("Loading project from {0}", path);
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
            _template.Scaffold(project.Template, project.Path);
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

        public string GetPathForProject(string title, string? basePath = null)
        {
            return Path.Combine(basePath ?? _path.GetPath(PathKind.ProjectRoot),
                PathHelpers.StripInvalidPathName(title));
        }

        private void Save(Project project)
        {
            using var writeStream = File.OpenWrite(Path.Combine(project.Path, MetadataFileName));
            JsonSerializer.Serialize(writeStream, project);
        }
    }
}