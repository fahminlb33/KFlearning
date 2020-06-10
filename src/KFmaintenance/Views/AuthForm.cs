// SOLUTION : KFlearning
// PROJECT  : KFmaintenance
// FILENAME : AuthForm.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using KFlearning.Core;
using KFmaintenance.Properties;
using System.Windows.Forms;

namespace KFmaintenance.Views
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        private void cmdSave_Click(object sender, System.EventArgs e)
        {
            DialogResult = Helpers.CompareHash(txtCode.Text, Settings.Default.Password) 
                ? DialogResult.OK 
                : DialogResult.Cancel;
            Close();
        }
    }
}