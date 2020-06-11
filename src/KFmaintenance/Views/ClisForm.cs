using ExcelDataReader;
using KFlearning.Core.CLIS;
using KFmaintenance.Properties;
using KFmaintenance.Services;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace KFmaintenance.Views
{
    public partial class ClisForm : Form
    {
        private readonly IClisService _service = Program.Container.Resolve<IClisService>();
        private readonly BindingList<ClisRecord> _records = new BindingList<ClisRecord>();
        public ClisForm()
        {
            InitializeComponent();

            dataGridView1.ApplyStyle();
            dataGridView1.DataSource = _records;
            dataGridView1.Columns[1].Width = 300;

            _service.ProgressChanged += Service_ProgressChanged;
            _service.ProcessFinished += Service_ProcessFinished;
        }


        #region Private Methods

        private void UpdateMessage(ClisProgressEventArgs e, bool executeEnable, bool stopEnable)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ClisProgressEventArgs, bool, bool>(UpdateMessage), e, executeEnable, stopEnable);
                return;
            }

            pgProgress.Value = e.ProgressPercentage;
            lblProgress.Text = e.Step;
            lstLog.Items.Add(e.Message);
            cmdExecute.Enabled = executeEnable;
            cmdStop.Enabled = stopEnable;
        }

        protected override void OnLoad(EventArgs e)
        {
            var settings = Settings.Default;
            chkSaveCred.Checked = settings.ClisSaveCred;
            if (settings.ClisSaveCred)
            {
                chkUseHeadless.Checked = settings.ClisUseHeadless;
                txtUsername.Text = settings.ClisUsername;
                txtPassword.Text = settings.ClisPassword;
                txtChromeDriver.Text = settings.WebdriverPath;
            }

            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            var settings = Settings.Default;
            settings.ClisUseHeadless = chkUseHeadless.Checked;
            settings.ClisUsername = txtUsername.Text;
            settings.ClisPassword = txtPassword.Text;
            settings.WebdriverPath = txtChromeDriver.Text;

            settings.Save();
            base.OnFormClosing(e);
        }

        #endregion

        #region Service Event Handler

        private void Service_ProcessFinished(object sender, ClisProgressEventArgs e)
        {
            UpdateMessage(e, true, false);
        }

        private void Service_ProgressChanged(object sender, ClisProgressEventArgs e)
        {
            UpdateMessage(e, false, true);
        }

        #endregion

        #region Buttons

        private void cmdOpenChrome_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ofdChromeDriver.ShowDialog() != DialogResult.OK) return;
            txtChromeDriver.Text = ofdChromeDriver.FileName;
        }

        private void cmdOpenExcel_Click(object sender, EventArgs e)
        {
            if (ofdExcel.ShowDialog() != DialogResult.OK) return;

            try
            {
                using (var stream = File.Open(ofdExcel.FileName, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        _records.Clear();

                        reader.Read(); // read header
                        while (reader.Read())
                        {
                            _records.Add(new ClisRecord
                            {
                                Npm = reader.GetString(0),
                                Name = reader.GetString(1),
                                Score = reader.GetDouble(2)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cmdExecute_Click(object sender, EventArgs e)
        {
            if (_records.Count == 0)
            {
                MessageBox.Show(Resources.ClisNoDataMessage, Resources.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!File.Exists(txtChromeDriver.Text))
            {
                MessageBox.Show(Resources.ClisDriverNotFoundMessage, Resources.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var metadata = new ClisMetadata
            {
                Username = txtUsername.Text,
                Password = txtPassword.Text,
                Class = txtClass.Text,
                Year = txtYear.Text,
                ClassId = txtClassId.Text,
                ContinueOnError = true,
                Records = _records.ToList()
            };

            _service.WebDriverPath = txtChromeDriver.Text;
            _service.ProcessMasterStudent = chkMasterStudentList.Checked;
            _service.ProcessLecturerStudent = chkLecturerStudentList.Checked;
            _service.ProcessFinalScore = chkFinalScore.Checked;
            _service.UseHeadless = chkUseHeadless.Checked;

            cmdExecute.Enabled = false;
            cmdStop.Enabled = true;

            _service.Start(metadata);
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            cmdStop.Enabled = false;
            _service.Stop();
        }

        #endregion
    }
}
