using KFlearning.Core.Extensions;
using System.Diagnostics;
using System.Windows.Forms;

namespace KFmaintenance.Views
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            lblVersion.Text = PathHelpers.GetVersionString();
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