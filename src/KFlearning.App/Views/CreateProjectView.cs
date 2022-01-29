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
        }

        public void BindData()
        {
            // data binding
            cboTemplate.DataSource = _presenter.CboTemplateDataSource;
            cboTemplate.DisplayMember = _presenter.CboTemplateDisplayMember;

            txtLocation.DataBindings.Add(nameof(txtLocation.Text), _presenter, 
                nameof(_presenter.TxtLocationText));
        }

        protected override void OnLoad(EventArgs e)
        {
            _presenter.OnLoadHandler();
            BindData();
        }

        private void cmdCreate_Click(object sender, EventArgs e)
        {
            if (!_presenter.CmdCreateClickHandler(txtProjectName.Text, cboTemplate.SelectedItem))
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
            _presenter.UpdateProjectPath(txtProjectName.Text);
        }

        private void txtProjectName_TextChanged(object sender, EventArgs e)
        {
            _presenter.UpdateProjectPath(txtProjectName.Text);
        }
    }
}