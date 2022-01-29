using System;
using System.Diagnostics;
using System.Windows.Forms;
using KFlearning.App.Resources;
using KFlearning.Core.Helpers;

namespace KFlearning.App.Views
{
    public partial class AboutView : Form
    {
        public AboutView()
        {
            InitializeComponent();

            lblVersion.Text = VersionHelpers.GetVersionString();
            lblCopyright.Text = string.Format(MessagesText.CopyrightText, DateTime.Now.Year);
        }

        private void cmdInstagram_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NetworkHelpers.LaunchUrl(MessagesText.LinksLinkedin);
        }

        private void cmdKodesiana_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NetworkHelpers.LaunchUrl(MessagesText.LinksHomepage);
        }

        private void cmdGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NetworkHelpers.LaunchUrl(MessagesText.LinksGithub);
        }
    }
}