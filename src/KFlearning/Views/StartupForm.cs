// SOLUTION : KFlearning
// PROJECT  : KFlearning
// FILENAME : StartupForm.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using KFlearning.Core;
using KFlearning.Core.API;
using KFlearning.Core.Forms;
using KFlearning.Properties;
using KFlearning.Services;
using Helpers = KFlearning.Core.Helpers;

namespace KFlearning.Views
{
    public partial class StartupForm : Form
    {
        private readonly IProjectService _project = Program.Container.Resolve<IProjectService>();
        private readonly IHistoryService _history = Program.Container.Resolve<IHistoryService>();
        private readonly ITelemetryService _telemetry = Program.Container.Resolve<ITelemetryService>();
        private readonly IUpdateCheckClient _updateCheck = Program.Container.Resolve<IUpdateCheckClient>();

        public StartupForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            lblVersion.Text = Helpers.GetVersionString();
            ReloadHistory();
            Task.Run(() => _telemetry.Load());
            Task.Run(() => CheckUpdate());
        }

        #region Private Methods

        private async void CheckUpdate()
        {
            try
            {
                var result = await _updateCheck.GetLatestVersion();
                if (!VersionHelpers.IsNewerVersion(result)) return;

                var selection = MessageBox.Show(string.Format(Resources.UpdateAvailableMessage, result.TagName, result.ReleaseName),
                    Resources.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (selection == DialogResult.Yes)
                {
                    Process.Start(result.DownloadUrl);
                }
            }
            catch (Exception)
            {
                // ignore
            }
        }

        private void ReloadHistory()
        {
            lstHistory.Items.Clear();
            lstHistory.Items.AddRange(_history.GetAll().Cast<object>().ToArray());
        }

        private void OpenProject(string path)
        {
            if (!_project.IsExists(path))
            {
                MessageBox.Show(Resources.InvalidProjectMessage, Resources.AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            var project = _project.Load(path);
            _history.Add(project);
            _project.Launch(project);
            ReloadHistory();
        }

        #endregion

        private void cmdNewProject_Click(object sender, System.EventArgs e)
        {
            using (var frm = Program.Container.Resolve<CreateProjectForm>())
            {
                if (frm.ShowDialog(this) != DialogResult.OK) return;
                _project.Create(frm.Project);
                _history.Add(frm.Project);
                _project.Launch(frm.Project);
                ReloadHistory();
            }
        }

        private void cmdOpenProject_Click(object sender, System.EventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.OK) return;
            OpenProject(fbd.SelectedPath);
        }

        private void cmdAbout_Click(object sender, System.EventArgs e)
        {
            using (var frm = Program.Container.Resolve<AboutForm>())
                frm.ShowDialog(this);
        }

        private void cmdClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lstHistory.Items.Clear();
            _history.Clear();
        }

        private void lstHistory_DrawItem(object sender, DrawItemEventArgs e)
        {
            FlatListBox.DrawItemHandler(sender, e);
        }

        private void lstHistory_DoubleClick(object sender, System.EventArgs e)
        {
            if (lstHistory.SelectedItem == null) return;
            var item = (Project)lstHistory.SelectedItem;
            OpenProject(item.Path);
        }
    }
}