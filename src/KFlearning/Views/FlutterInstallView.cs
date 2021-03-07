using System;
using System.Windows.Forms;
using KFlearning.Services;

namespace KFlearning.Views
{
    public partial class FlutterInstallView : Form
    {
        private readonly IFlutterInstallService _installService = Program.Container.Resolve<IFlutterInstallService>();

        public FlutterInstallView()
        {
            InitializeComponent();
            _installService.InstallReady += InstallService_InstallReady;
            _installService.ProgressChanged += InstallService_ProgressChanged;
            _installService.InstallFinished += InstallService_InstallFinished;
        }

        #region FlutterInstallService Event Handlers

        private void InstallService_InstallReady(object sender, FlutterInstallReadyEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, FlutterInstallReadyEventArgs>(InstallService_InstallReady), sender, e);
            }
            else
            {
                cmdInstall.Enabled = e.Ready;
                cmdBrowse.Enabled = e.Ready;

                lblStatus.Text = e.Ready ? "Siap." : e.ErrorMessage;
                lblFlutterVersion.Text = e.Ready ? _installService.FlutterVersion : "Tidak diketahui.";
                txtInstallPath.Text = _installService.InstallPath;
            }
        }

        private void InstallService_ProgressChanged(object sender, FlutterInstallProgressEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, FlutterInstallProgressEventArgs>(InstallService_ProgressChanged), sender, e);
            }
            else
            {
                prgProgress.Value = e.ProgressPercentage;
                lblStatus.Text = e.Status;
                lblPercent.Text = $"{e.ProgressPercentage}%";
            }
        }

        private void InstallService_InstallFinished(object sender, FlutterInstallFinishedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, FlutterInstallFinishedEventArgs>(InstallService_InstallFinished), sender, e);
            }
            else
            {
                cmdBrowse.Enabled = true;
                cmdInstall.Enabled = true;
                cmdInstall.Text = "Tutup";

                lblStatus.Text = e.ErrorMessage;
                prgProgress.Value = 0;
                lblPercent.Text = "0%";
            }
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            _installService.PreparationStep();
        }

        private void cmdBrowse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            txtInstallPath.Text = fbd.SelectedPath;
        }

        private void cmdInstall_Click(object sender, EventArgs e)
        {
            switch (cmdInstall.Text)
            {
                case "Install Flutter":
                    cmdBrowse.Enabled = false;
                    cmdInstall.Text = "Batal";

                    _installService.InstallPath = txtInstallPath.Text;
                    _installService.Install();
                    break;
                case "Tutup":
                    Close();
                    break;
                default:
                    cmdInstall.Enabled = false;
                    cmdInstall.Text = "Install Flutter";
                    _installService.Cancel();
                    break;
            }
        }
    }
}