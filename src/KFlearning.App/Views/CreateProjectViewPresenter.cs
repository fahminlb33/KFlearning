using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using KFlearning.Annotations;
using KFlearning.App.Resources;
using KFlearning.App.Services;
using KFlearning.TemplateProvider;
using Microsoft.Extensions.Logging;

namespace KFlearning.App.Views
{
    public class CreateProjectViewPresenter : INotifyPropertyChanged
    {
        private readonly ILogger<CreateProjectViewPresenter> _logger;
        private readonly IProjectService _projectService;
        private readonly ITemplateService _templateService;

        private string _basePath = string.Empty;
        private Project? _project;
        private object? _cboTemplateDataSource;
        private string? _txtLocationText;
        private string? _txtProjectNameText;
        private object? _cboTemplateSelectedItem;
        private string? _cboTemplateDisplayMember;

        public CreateProjectViewPresenter(ILogger<CreateProjectViewPresenter> logger, IProjectService projectService, ITemplateService templateService)
        {
            _logger = logger;
            _projectService = projectService;
            _templateService = templateService;
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

        public object? CboTemplateSelectedItem
        {
            get => _cboTemplateSelectedItem;
            set
            {
                if (Equals(value, _cboTemplateSelectedItem)) return;
                _cboTemplateSelectedItem = value;
                OnPropertyChanged();
            }
        }

        public string? TxtProjectNameText
        {
            get => _txtProjectNameText;
            set
            {
                if (value == _txtProjectNameText) return;
                _txtProjectNameText = value;
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
            CboTemplateDataSource = _templateService.GetTemplates();
            CboTemplateDisplayMember = nameof(ITemplateProvider.Title);
        }

        public bool CmdCreateClickHandler()
        {
            if (string.IsNullOrWhiteSpace(TxtProjectNameText))
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

            if (CboTemplateSelectedItem is null)
            {
                MessageBox.Show(MessagesText.CreateProjectNullProjectTypeError, MessagesText.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            Project = new Project
            {
                Name = TxtProjectNameText,
                Path = _projectService.GetPathForProject(TxtProjectNameText, BasePath),
                Template = (ITemplateProvider) CboTemplateSelectedItem,
                CreatedAt = DateTime.Now
            };

            return true;
        }

        public void UpdateProjectPath()
        {
            if (string.IsNullOrWhiteSpace(TxtProjectNameText))
            {
                return;
            }

            TxtLocationText = _projectService.GetPathForProject(TxtProjectNameText, BasePath);
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
