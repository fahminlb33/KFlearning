using System;
using System.Diagnostics;
using System.Windows.Forms;
using KFlearning.App.Resources;
using KFlearning.Core.Extensions;

namespace KFlearning.App.Views
{
    public partial class AboutView : Form
    {
        public AboutView()
        {
            InitializeComponent();

            lblVersion.Text = PathHelpers.GetVersionString();
            lblCopyright.Text = string.Format(MessagesText.CopyrightText, DateTime.Now.Year);
        }

        private void cmdInstagram_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(MessagesText.LinksLinkedin);
        }

        private void cmdKodesiana_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(MessagesText.LinksHomepage);
        }

        private void cmdGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(MessagesText.LinksGithub);
        }
    }
}