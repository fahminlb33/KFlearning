// SOLUTION : KFlearning
// PROJECT  : KFmaintenance
// FILENAME : StartupForm.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System;
using System.IO;
using System.Windows.Forms;
using KFlearning.Core;
using KFlearning.Core.Services;
using KFmaintenance.Properties;
using KFmaintenance.Services;

namespace KFmaintenance.Views
{
    public partial class StartupForm : Form
    {
        private readonly ISystemInfoService _infoService = Program.Container.Resolve<ISystemInfoService>();
        private readonly IRemoteShutdownServer _remoteService = Program.Container.Resolve<IRemoteShutdownServer>();
        private readonly ISystemTweaker _systemTweaker = Program.Container.Resolve<ISystemTweaker>();
        private readonly IProcessManager _processManager = Program.Container.Resolve<IProcessManager>();
        private readonly IPathManager _pathManager = Program.Container.Resolve<IPathManager>();
        private readonly IFormService _formService = Program.Container.Resolve<IFormService>();
        private DateTime _lastShutdownRequest;
        private bool _exit;

        public StartupForm()
        {
            InitializeComponent();

            _lastShutdownRequest = DateTime.Now;
        }

        #region Form Events

        protected override void OnLoad(EventArgs e)
        {
            // app version
            lblVersion.Text = Helpers.GetVersionString();

            // listen remote shutdowns
            _remoteService.ShutdownRequested += RemoteService_ShutdownRequested;
            _remoteService.Listen();

            // fill diagnostics
            _infoService.Query();
            lblOS.Text = _infoService.OS;
            lblOSVersion.Text = _infoService.OSVersion;
            lblOSArch.Text = _infoService.Architecture;
            lblRam.Text = _infoService.RAM;
            lblCpu.Text = _infoService.CPU;

            // system registry
            _systemTweaker.Query();
            chkWriteProtect.Checked = _systemTweaker.LockUsbCopying;
            chkRegistry.Checked = _systemTweaker.LockRegistryEditor;
            chkTaskManager.Checked = _systemTweaker.LockTaskManager;
            chkControlPanel.Checked = _systemTweaker.LockControlPanel;
            chkDesktop.Checked = _systemTweaker.LockDesktop;
            chkWallpaper.Checked = _systemTweaker.LockWallpaper;
            pnWallpaper.Enabled = _systemTweaker.LockWallpaper;
            if (_systemTweaker.WallpaperPath == null)
            {
                rdWDefault.Checked = true;
                chkWallpaper.Checked = false;
            }
            else
            {
                rdWCustom.Checked = true;
                lblFileName.Text = _systemTweaker.WallpaperPath;
            }

            // app settings
            txtCluster.Text = Settings.Default.Cluster;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing || _exit)
            {
                tray.Visible = false;
                return;
            }

            e.Cancel = true;
            Hide();
        }

        #endregion

        #region Event Handlers

        private void RemoteService_ShutdownRequested(object sender, ShutdownRequestedEventArgs e)
        {
            if ((DateTime.Now - _lastShutdownRequest).TotalSeconds < 30) return;

            _exit = true;
            WindowsHelpers.Shutdown();
            Application.Exit();
        }

        #endregion

        #region Buttons

        private void chkWallpaper_CheckedChanged(object sender, EventArgs e)
        {
            pnWallpaper.Enabled = chkWallpaper.Checked;
        }

        private void cmdBrowseWallpaper_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ofd.ShowDialog() != DialogResult.OK) return;
            lblFileName.Text = ofd.FileName;
        }

        private void cmdSaveSettings_Click(object sender, EventArgs e)
        {
            using (var frm = Program.Container.Resolve<AuthForm>())
            {
                var result = frm.ShowDialog();
                if (result == DialogResult.Abort)
                {
                    MessageBox.Show(Resources.PasswordInvalidMessage, Resources.AppName, MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }

                if (result != DialogResult.OK) return;
            }

            var settings = Settings.Default;
            settings.Cluster = txtCluster.Text;
            if (txtPassword.TextLength > 0)
            {
                settings.Password = Helpers.HashPassword(txtPassword.Text);
            }

            settings.Save();
            MessageBox.Show(Resources.SettingsFailedMessage, Resources.AppName, MessageBoxButtons.OK, 
                MessageBoxIcon.Information);
        }

        private void cmdSaveRegistry_Click(object sender, EventArgs e)
        {
            if (!_processManager.IsProcessElevated())
            {
                MessageBox.Show(Resources.NotElevatedMessage, Resources.AppName, MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                return;
            }

            using (var frm = Program.Container.Resolve<AuthForm>())
            {
                var result = frm.ShowDialog();
                if (result == DialogResult.Abort)
                {
                    MessageBox.Show(Resources.PasswordInvalidMessage, Resources.AppName, MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }

                if (result != DialogResult.OK) return;
            }

            try
            {
                Settings.Default.Password = txtPassword.Text;
                Settings.Default.Save();

                _systemTweaker.LockUsbCopying = chkWriteProtect.Checked;
                _systemTweaker.LockRegistryEditor = chkRegistry.Checked;
                _systemTweaker.LockTaskManager = chkTaskManager.Checked;
                _systemTweaker.LockControlPanel = chkControlPanel.Checked;
                _systemTweaker.LockDesktop = chkDesktop.Checked;
                _systemTweaker.LockWallpaper = chkWallpaper.Checked;

                if (rdWDefault.Checked)
                {
                    _systemTweaker.WallpaperPath = null;
                }
                else
                {
                    var storeWallpaperPath = _pathManager.GetPath(PathKind.WallpaperPath);
                    var newWallpaperPath = lblFileName.Text;

                    if (storeWallpaperPath == newWallpaperPath) return;
                    if (File.Exists(storeWallpaperPath)) File.Delete(storeWallpaperPath);

                    File.Copy(newWallpaperPath, storeWallpaperPath);
                    _systemTweaker.WallpaperPath = storeWallpaperPath;
                }

                MessageBox.Show(Resources.SettingsSavedMessage, Resources.AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Resources.SettingsFailedMessage, ex.Message), Resources.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cmdFileServer_Click(object sender, EventArgs e)
        {
            using (var frm = Program.Container.Resolve<AuthForm>())
            {
                var result = frm.ShowDialog();
                if (result == DialogResult.Abort)
                {
                    MessageBox.Show(Resources.PasswordInvalidMessage, Resources.AppName, MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }

                if (result != DialogResult.OK) return;
            }

            _formService.ShowFileServer();
        }

        private void cmdRemoteShutdown_Click(object sender, EventArgs e)
        {
            using (var frm = Program.Container.Resolve<AuthForm>())
            {
                var result = frm.ShowDialog();
                if (result == DialogResult.Abort)
                {
                    MessageBox.Show(Resources.PasswordInvalidMessage, Resources.AppName, MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }

                if (result != DialogResult.OK) return;
            }

            var result2 = MessageBox.Show(Resources.RemoteShutdownConfirmMessage, Resources.AppName,
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result2 != DialogResult.OK) return;

            _lastShutdownRequest = DateTime.Now;
            _remoteService.SendShutdown(Settings.Default.Cluster);
        }

        private void cmdCLIS_Click(object sender, EventArgs e)
        {
            using (var frm = Program.Container.Resolve<AuthForm>())
            {
                var result = frm.ShowDialog();
                if (result == DialogResult.Abort)
                {
                    MessageBox.Show(Resources.PasswordInvalidMessage, Resources.AppName, MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }

                if (result != DialogResult.OK) return;
            }

            _formService.ShowClis();
        }

        #endregion

        #region Context Menu

        private void mnuExit_Click(object sender, EventArgs e)
        {
            using (var frm = Program.Container.Resolve<AuthForm>())
            {
                if (frm.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show(Resources.PasswordInvalidMessage, Resources.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    _exit = true;
                    Application.Exit();
                }
            }
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void tray_DoubleClick(object sender, EventArgs e)
        {
            Show();
        }

        #endregion

    }
}