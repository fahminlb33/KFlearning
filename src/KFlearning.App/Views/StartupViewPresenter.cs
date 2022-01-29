using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using KFlearning.Annotations;
using KFlearning.App.Resources;
using KFlearning.App.Services;
using KFlearning.Core.Helpers;
using KFlearning.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KFlearning.App.Views
{
    public class StartupViewPresenter : INotifyPropertyChanged
    {
        private readonly ILogger<StartupViewPresenter> _logger;
        private readonly IPathManager _pathManager;
        private readonly IProjectService _project;
        private readonly IHistoryService _history;

        private string lblVersionText = string.Empty;
        private object? lstHistorySelectedItem;
        private ListBox.ObjectCollection lstHistoryItems;

        public StartupViewPresenter(ILogger<StartupViewPresenter> logger, IPathManager pathManager, IProjectService project, IHistoryService history)
        {
            _logger = logger;
            _pathManager = pathManager;
            _project = project;
            _history = history;
        }

        #region Properties

        public string LblVersionText
        {
            get => lblVersionText;
            set
            {
                if (value == lblVersionText) return;
                lblVersionText = value;
                OnPropertyChanged();
            }
        }

        public object? LstHistorySelectedItem
        {
            get => lstHistorySelectedItem;
            set
            {
                if (value == lstHistorySelectedItem) return;
                lstHistorySelectedItem = value;
                OnPropertyChanged();
            }
        }

        public ListBox.ObjectCollection LstHistoryItems
        {
            get => lstHistoryItems;
            set
            {
                if (value == lstHistoryItems) return;
                lstHistoryItems = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public void OnLoad()
        {
            LblVersionText = VersionHelpers.GetVersionString();
            ReloadHistory();
        }

        public void CmdOpenProjectClickHandler(string path)
        {
            OpenOrCreateProject(_project.Load(path), false);
        }

        public void CmdNewProjectClickHandler()
        {
            using var frm = Program.Container.GetRequiredService<CreateProjectView>();
            if (frm.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            OpenOrCreateProject(frm.Project, true);
        }

        public void CmdAboutClickHandler()
        {
            using var frm = Program.Container.GetRequiredService<AboutView>();
            frm.ShowDialog();
        }

        public void CmdFlutterInstallClickHandler()
        {
            if (_pathManager.IsFlutterInstalled)
            {
                MessageBox.Show(MessagesText.FlutterAlreadyInstalledError, MessagesText.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var frm = Program.Container.GetRequiredService<FlutterInstallView>();
            frm.ShowDialog();
        }

        public void LstHistoryDoubleClickHandler()
        {
            if (LstHistorySelectedItem == null)
            {
                return;
            }

            OpenOrCreateProject((Project)LstHistorySelectedItem, false);
        }

        public void CmdClearClickHandler()
        {
            LstHistoryItems.Clear();
            _history.Clear();
        }

        private void ReloadHistory()
        {
            var items = _history.GetAll().Cast<object>().ToArray();

            LstHistoryItems.Clear();
            LstHistoryItems.AddRange(items);
        }

        private void OpenOrCreateProject(Project? project, bool create)
        {
            try
            {
                // check if the project is null
                if (project == null)
                {
                    MessageBox.Show(MessagesText.HomeProjectOpenCreateError, MessagesText.AppName,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // create the project
                if (create)
                {
                    _project.Create(project);
                }

                // check if the project is exists
                if (!_project.IsExists(project.Path))
                {
                    MessageBox.Show(MessagesText.HomeProjectNotFoundError, MessagesText.AppName,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // launch the project
                _project.Launch(project);

                // add to history
                _history.Add(project);
                ReloadHistory();
            }
            catch (Exception ex)
            {
                _logger.LogError("Can't open project", ex);
                MessageBox.Show(MessagesText.HomeProjectOpenCreateError, MessagesText.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
