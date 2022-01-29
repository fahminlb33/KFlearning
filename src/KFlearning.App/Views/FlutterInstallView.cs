using System;
using System.Windows.Forms;

namespace KFlearning.App.Views
{
    public partial class FlutterInstallView : Form, IView
    {
        private readonly FlutterInstallViewPresenter _presenter;

        public FlutterInstallView(FlutterInstallViewPresenter presenter)
        {
            _presenter = presenter;

            InitializeComponent();
            BindData();
        }

        public void BindData()
        {
            // form event and delegates
            _presenter.FormCloseAction = Close;
            _presenter.FormInvokeAction = (a, b) => Invoke(a, b);
            _presenter.InvokeRequiredAction = () => InvokeRequired;

            // data binding
            cmdInstall.DataBindings.Add(nameof(cmdInstall.Text), _presenter, nameof(_presenter.CmdInstallText));
            cmdInstall.DataBindings.Add(nameof(cmdInstall.Enabled), _presenter, nameof(_presenter.CmdInstallEnabled));
            cmdBrowse.DataBindings.Add(nameof(cmdBrowse.Enabled), _presenter, nameof(_presenter.CmdBrowseEnabled));

            txtInstallPath.DataBindings.Add(nameof(txtInstallPath.Text), _presenter,
                nameof(_presenter.TxtInstallPathText));

            prgProgress.DataBindings.Add(nameof(prgProgress.Value), _presenter, nameof(_presenter.PrgProgressValue));

            lblStatus.DataBindings.Add(nameof(lblStatus.Text), _presenter, nameof(_presenter.LblStatusText));
            lblPercent.DataBindings.Add(nameof(lblPercent.Text), _presenter, nameof(_presenter.LblPercentText));
            lblFlutterVersion.DataBindings.Add(nameof(lblFlutterVersion.Text), _presenter,
                nameof(_presenter.LblFlutterVersionText));
        }

        protected override void OnLoad(EventArgs e)
        {
            _presenter.OnLoadHandler();
        }

        private void cmdBrowse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _presenter.TxtInstallPathText = fbd.SelectedPath;
        }

        private void cmdInstall_Click(object sender, EventArgs e)
        {
            _presenter.CmdInstallClickHandler();
        }
    }
}