using System;
using System.Diagnostics;
using System.Windows.Forms;
using KFlearning.Core.Extensions;

namespace KFlearning.Views
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            lblVersion.Text = PathHelpers.GetVersionString();
            lblCopyright.Text = $"Hak Cipta (C) Fahmi Noor Fiqri {DateTime.Now.Year}.\r\nDilisensikan di bawah lisensi MIT.";
        }

        private void cmdInstagram_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.instagram.com/fahminoorfiqri");
        }

        private void cmdKodesiana_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://kodesiana.com");
        }

        private void cmdGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/fahminlb33");
        }
    }
}