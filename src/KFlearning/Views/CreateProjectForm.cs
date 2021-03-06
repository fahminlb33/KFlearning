using System;
using System.IO;
using System.Windows.Forms;
using KFlearning.Models;
using KFlearning.Properties;
using KFlearning.Services;
using KFlearning.TemplateProvider;

namespace KFlearning.Views
{
    public partial class CreateProjectForm : Form
    {
        private string _basePath;

        private readonly IProjectService _projectService = Program.Container.Resolve<IProjectService>();
        private readonly ITemplateService _templateService = Program.Container.Resolve<ITemplateService>();

        public Project Project { get; set; }

        public CreateProjectForm()
        {
            InitializeComponent();

            cboTemplate.DataSource = _templateService.GetTemplates();
            cboTemplate.DisplayMember = "Title";
        }

        private void cmdCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProjectName.Text))
            {
                MessageBox.Show(Resources.ProjectNameEmptyMessage, Resources.AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            if (Directory.Exists(txtLocation.Text))
            {
                MessageBox.Show(Resources.ProjectExistsMessage, Resources.AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            Project = new Project
            {
                Name = txtProjectName.Text,
                Path = _projectService.GetPathForProject(txtProjectName.Text, _basePath),
                Template = (ITemplateProvider) cboTemplate.SelectedItem,
                CreatedAt = DateTime.Now
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private void cmdBrowse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _basePath = fbd.SelectedPath;
            txtProjectName_TextChanged(null, null);
        }

        private void txtProjectName_TextChanged(object sender, EventArgs e)
        {
            txtLocation.Text = _projectService.GetPathForProject(txtProjectName.Text, _basePath);
        }
    }
}