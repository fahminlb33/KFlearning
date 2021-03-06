using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using KFlearning.Core.Remoting;
using KFmaintenance.Properties;

namespace KFmaintenance.Views
{
    public partial class FileServerForm : Form
    {
        private readonly IKFServer _server = Program.Container.Resolve<IKFServer>();

        public FileServerForm()
        {
            InitializeComponent();
        }

        private void cmdOpenAddress_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!cmdOpenAddress.Text.StartsWith("http"))
            {
                return;
            }

            Process.Start(cmdOpenAddress.Text);
        }

        private void cmdOpenFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            txtSource.Text = fbd.SelectedPath;
        }

        private void cmdExecute_Click(object sender, EventArgs e)
        {
            if (_server.IsRunning)
            {
                _server.Stop();
            }
            else
            {
                if (string.IsNullOrEmpty(txtSource.Text))
                {
                    MessageBox.Show(Resources.FolderNotSelectedMessage, Resources.AppName,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                _server.Start(txtSource.Text);
            }

            UpdateUIState();
        }

        private void UpdateUIState()
        {
            var serverActive = _server.IsRunning;

            cmdSelectFolder.Enabled = !serverActive;
            cmdExecute.Text = serverActive ? "Matikan" : "Aktifkan";
            cmdExecute.BackColor = serverActive
                ? Color.FromArgb(168, 35, 35)
                : Color.FromArgb(35, 168, 109);

            if (serverActive)
            {
                var info = _server.GetInfo();

                lblIP.Text = info.IP;
                lblPort.Text = info.Port.ToString();
                cmdOpenAddress.Text = info.Link;
            }
            else
            {
                lblIP.Text = Resources.Dots;
                lblPort.Text = Resources.Dots;
                cmdOpenAddress.Text = Resources.Dots;
            }
        }

    }
}
