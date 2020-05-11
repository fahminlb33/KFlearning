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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KFlearning.Core;
using KFlearning.Core.API;
using KFlearning.Core.Services;
using KFmaintenance.Properties;
using KFmaintenance.Services;

namespace KFmaintenance.Views
{
    public partial class StartupForm : Form
    {
        private readonly ISystemInfoService _infoService = Program.Container.Resolve<ISystemInfoService>();
        private readonly IRemoteService _remoteService = Program.Container.Resolve<IRemoteService>();
        private readonly ISystemTweaker _systemTweaker = Program.Container.Resolve<ISystemTweaker>();
        private readonly IPathManager _pathManager = Program.Container.Resolve<IPathManager>();
        private bool _exit, _promptAuth;

        public StartupForm()
        {
            InitializeComponent();
        }

        #region Form Events

        protected override void OnLoad(EventArgs e)
        {
            _promptAuth = true;

            // run server
            _remoteService.Run();

            // app version
            lblVersion.Text = Helpers.GetVersionString();

            // remote shutdown
            chkRemoteShutdown.Checked = Settings.Default.RemoteShutdown;

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
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing || _exit)
            {
                tray.Visible = false;
                return;
            }

            e.Cancel = true;
            _promptAuth = true;

            Hide();
        }

        protected override void OnActivated(EventArgs e)
        {
            if (!_promptAuth) return;
            using (var frm = Program.Container.Resolve<AuthForm>())
            {
                if (frm.ShowDialog() != DialogResult.OK)
                {
                    _promptAuth = true;
                    Hide();
                }
                else
                {
                    _promptAuth = false;
                    if (_exit) Close();
                }
            }
        } 

        #endregion

        #region Buttons

        private void chkWallpaper_CheckedChanged(object sender, EventArgs e)
        {
            pnWallpaper.Enabled = chkWallpaper.Checked;
        }

        private void cmdBrowseWallpaper_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofd.Filter = Resources.FileImageFilter;
            if (ofd.ShowDialog() != DialogResult.OK) return;
            lblFileName.Text = ofd.FileName;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                Settings.Default.Password = txtPassword.Text;
                Settings.Default.RemoteShutdown = chkRemoteShutdown.Checked;
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

        private void cmdSend_Click(object sender, EventArgs e)
        {
            if (cmdSend.Tag.ToString() == "SEND")
            {
                ofd.Filter = Resources.FileTorrentFilter;
                if (ofd.ShowDialog() != DialogResult.OK) return;

                _remoteService.StartSendFile(ofd.FileName);
                cmdSend.Text = Resources.ServerStartText;
                cmdSend.Tag = "STOP";
            }
            else
            {
                cmdSend.Text = Resources.ServerStopText;
                _remoteService.StopSendFile();
            }
        }

        private void cmdBroadcastShutdown_Click(object sender, EventArgs e)
        {
            _remoteService.SendShutdown();
        }

        private void chkRemoteShutdown_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.RemoteShutdown = chkRemoteShutdown.Checked;
        }

        #endregion

        #region Context Menu

        private void mnuExit_Click(object sender, EventArgs e)
        {
            _exit = true;
            Show();
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