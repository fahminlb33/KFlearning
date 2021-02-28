using System;
using System.Linq;
using System.Windows.Forms;
using KFlearning.Control;
using KFlearning.Core.Extensions;
using KFlearning.Models;
using KFlearning.Properties;
using KFlearning.Services;

namespace KFlearning.Views
{
    public partial class StartupForm : Form
    {
        private readonly IProjectService _project = Program.Container.Resolve<IProjectService>();
        private readonly IHistoryService _history = Program.Container.Resolve<IHistoryService>();

        public StartupForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            lblVersion.Text = PathHelpers.GetVersionString();
            ReloadHistory();

        }

        #region Private Methods

        private void ReloadHistory()
        {
            lstHistory.Items.Clear();
            lstHistory.Items.AddRange(_history.GetAll().Cast<object>().ToArray());
        }

        private void OpenProject(string path)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Tidak dapat membuka project.\r\n" + ex.Message, Resources.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        private void cmdNewProject_Click(object sender, System.EventArgs e)
        {
            using (var frm = Program.Container.Resolve<CreateProjectForm>())
            {
                if (frm.ShowDialog(this) != DialogResult.OK) return;
                _project.Create(frm.Project);
                OpenProject(frm.Project.Path);
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