using System;
using System.Windows.Forms;
using KFlearning.App.Views.Controls;

namespace KFlearning.App.Views
{
    public partial class StartupView : Form, IView
    {
        private readonly StartupViewPresenter _presenter;

        public StartupView(StartupViewPresenter presenter)
        {
            _presenter = presenter;

            InitializeComponent();
            BindData();
        }

        public void BindData()
        {
            // data binding
            lblVersion.DataBindings.Add(nameof(lblVersion.Text), _presenter, nameof(_presenter.LblVersionText));

            _presenter.LstHistoryItems = lstHistory.Items;
            lstHistory.DataBindings.Add(nameof(lstHistory.SelectedItem), _presenter,
                nameof(_presenter.LstHistorySelectedItem));
        }

        protected override void OnLoad(EventArgs e)
        {
            _presenter.OnLoad();
        }

        private void lstHistory_DrawItem(object sender, DrawItemEventArgs e)
        {
            FlatListBox.DrawItemHandler(sender, e);
        }

        private void cmdNewProject_Click(object sender, EventArgs e)
        {
           _presenter.CmdNewProjectClickHandler();
        }

        private void cmdOpenProject_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _presenter.CmdOpenProjectClickHandler(fbd.SelectedPath);
        }

        private void cmdAbout_Click(object sender, EventArgs e)
        {
            _presenter.CmdAboutClickHandler();
        }

        private void cmdFlutterInstall_Click(object sender, EventArgs e)
        {
           _presenter.CmdFlutterInstallClickHandler();
        }

        private void cmdClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _presenter.CmdClearClickHandler();
        }

        private void lstHistory_DoubleClick(object sender, EventArgs e)
        {
            _presenter.LstHistoryDoubleClickHandler();
        }

    }
}