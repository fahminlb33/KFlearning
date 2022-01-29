using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using KFlearning.Annotations;
using KFlearning.App.Resources;
using KFlearning.App.Services;
using KFlearning.Core.Services;
using KFlearning.TemplateProvider;
using Microsoft.Extensions.Logging;

namespace KFlearning.App.Views
{
    public class CreateProjectViewPresenter : INotifyPropertyChanged
    {
        private readonly ILogger<CreateProjectViewPresenter> _logger;
        private readonly IProjectService _projectService;
        private readonly ITemplateService _templateService;
        private readonly IPathManager _pathManager;

        private string _basePath = string.Empty;
        private Project? _project;
        private object? _cboTemplateDataSource;
        private string? _cboTemplateDisplayMember;
        private string? _txtLocationText;

        public CreateProjectViewPresenter(ILogger<CreateProjectViewPresenter> logger, IProjectService projectService, ITemplateService templateService, IPathManager pathManager)
        {
            _logger = logger;
            _projectService = projectService;
            _templateService = templateService;
            _pathManager = pathManager;
        }

        #region Properties

        public string BasePath
        {
            get => _basePath;
            set
            {
                if (value == _basePath) return;
                _basePath = value;
                OnPropertyChanged();
            }
        }

        public Project? Project
        {
            get => _project;
            set
            {
                if (Equals(value, _project)) return;
                _project = value;
                OnPropertyChanged();
            }
        }

        public object? CboTemplateDataSource
        {
            get => _cboTemplateDataSource;
            set
            {
                if (Equals(value, _cboTemplateDataSource)) return;
                _cboTemplateDataSource = value;
                OnPropertyChanged();
            }
        }

        public string? CboTemplateDisplayMember
        {
            get => _cboTemplateDisplayMember;
            set
            {
                if (value == _cboTemplateDisplayMember) return;
                _cboTemplateDisplayMember = value;
                OnPropertyChanged();
            }
        }

        public string? TxtLocationText
        {
            get => _txtLocationText;
            set
            {
                if (value == _txtLocationText) return;
                _txtLocationText = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public void OnLoadHandler()
        {
            BasePath = _pathManager.GetPath(PathKind.ProjectRoot);
            CboTemplateDataSource = _templateService.GetTemplates();
            CboTemplateDisplayMember = nameof(ITemplateProvider.Title);
        }

        public bool CmdCreateClickHandler(string projectName, object selectedItem)
        {
            if (string.IsNullOrWhiteSpace(projectName))
            {
                MessageBox.Show(MessagesText.CreateProjectDuplicateError, MessagesText.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (Directory.Exists(TxtLocationText))
            {
                MessageBox.Show(MessagesText.CreateProjectDuplicateError, MessagesText.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (selectedItem is null)
            {
                MessageBox.Show(MessagesText.CreateProjectNullProjectTypeError, MessagesText.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            Project = new Project
            {
                Name = projectName,
                Path = _projectService.GetPathForProject(projectName, BasePath),
                Template = (ITemplateProvider)selectedItem,
                CreatedAt = DateTime.Now
            };

            return true;
        }

        public void UpdateProjectPath(string projectName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
            {
                return;
            }

            TxtLocationText = _projectService.GetPathForProject(projectName, BasePath);
        }

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
