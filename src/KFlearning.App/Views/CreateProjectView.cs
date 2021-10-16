using System;
using System.Windows.Forms;
using KFlearning.App.Services;

namespace KFlearning.App.Views
{
    public partial class CreateProjectView : Form, IView
    {
        private readonly CreateProjectViewPresenter _presenter;

        public Project? Project => _presenter.Project;

        public CreateProjectView(CreateProjectViewPresenter presenter)
        {
            _presenter = presenter;

            InitializeComponent();
            BindData();
        }

        public void BindData()
        {
            // data binding
            cboTemplate.DataBindings.Add(nameof(cboTemplate.DataSource), _presenter,
                nameof(_presenter.CboTemplateDataSource));
            cboTemplate.DataBindings.Add(nameof(cboTemplate.DisplayMember), _presenter,
                nameof(_presenter.CboTemplateDisplayMember));
            cboTemplate.DataBindings.Add(nameof(cboTemplate.SelectedItem), _presenter,
                nameof(_presenter.CboTemplateSelectedItem));

            txtProjectName.DataBindings.Add(nameof(txtProjectName.Text), _presenter,
                nameof(_presenter.TxtProjectNameText));

            txtLocation.DataBindings.Add(nameof(txtLocation.Text), _presenter, 
                nameof(_presenter.TxtLocationText));
        }

        protected override void OnLoad(EventArgs e)
        {
            _presenter.OnLoadHandler();
        }

        private void cmdCreate_Click(object sender, EventArgs e)
        {
            if (!_presenter.CmdCreateClickHandler())
            {
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void cmdBrowse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _presenter.BasePath = fbd.SelectedPath;
            _presenter.UpdateProjectPath();
        }

        private void txtProjectName_TextChanged(object sender, EventArgs e)
        {
            _presenter.UpdateProjectPath();
        }
    }
}