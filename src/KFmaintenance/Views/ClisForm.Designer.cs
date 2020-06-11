namespace KFmaintenance.Views
{
    partial class ClisForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ofdChromeDriver = new System.Windows.Forms.OpenFileDialog();
            this.flatTabControl1 = new KFlearning.Core.Forms.FlatTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkSaveCred = new System.Windows.Forms.CheckBox();
            this.chkUseHeadless = new System.Windows.Forms.CheckBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.txtClass = new System.Windows.Forms.TextBox();
            this.txtChromeDriver = new System.Windows.Forms.TextBox();
            this.cmdOpenChrome = new System.Windows.Forms.LinkLabel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtClassId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmdOpenExcel = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cmdStop = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.chkMasterStudentList = new System.Windows.Forms.CheckBox();
            this.chkLecturerStudentList = new System.Windows.Forms.CheckBox();
            this.chkFinalScore = new System.Windows.Forms.CheckBox();
            this.lblProgress = new System.Windows.Forms.Label();
            this.cmdExecute = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.pgProgress = new System.Windows.Forms.ProgressBar();
            this.ofdExcel = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.flatTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(622, 61);
            this.panel1.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Webdings", 19.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label2.Location = new System.Drawing.Point(15, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 34);
            this.label2.TabIndex = 2;
            this.label2.Text = "\'";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.label1.Location = new System.Drawing.Point(78, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "CLIS Auto-Fill";
            // 
            // ofdChromeDriver
            // 
            this.ofdChromeDriver.Filter = "chromedriver.exe|chromedriver.exe";
            this.ofdChromeDriver.Title = "Pilih chromedriver.exe";
            // 
            // flatTabControl1
            // 
            this.flatTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flatTabControl1.Controls.Add(this.tabPage1);
            this.flatTabControl1.Controls.Add(this.tabPage2);
            this.flatTabControl1.Controls.Add(this.tabPage3);
            this.flatTabControl1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.flatTabControl1.ItemSize = new System.Drawing.Size(120, 40);
            this.flatTabControl1.Location = new System.Drawing.Point(0, 77);
            this.flatTabControl1.Name = "flatTabControl1";
            this.flatTabControl1.SelectedIndex = 0;
            this.flatTabControl1.Size = new System.Drawing.Size(622, 333);
            this.flatTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.flatTabControl1.TabIndex = 25;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.tabPage1.Controls.Add(this.chkSaveCred);
            this.tabPage1.Controls.Add(this.chkUseHeadless);
            this.tabPage1.Controls.Add(this.txtYear);
            this.tabPage1.Controls.Add(this.txtClass);
            this.tabPage1.Controls.Add(this.txtChromeDriver);
            this.tabPage1.Controls.Add(this.cmdOpenChrome);
            this.tabPage1.Controls.Add(this.txtPassword);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtUsername);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.txtClassId);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Location = new System.Drawing.Point(4, 44);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(614, 285);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Akun";
            // 
            // chkSaveCred
            // 
            this.chkSaveCred.AutoSize = true;
            this.chkSaveCred.Checked = true;
            this.chkSaveCred.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveCred.Location = new System.Drawing.Point(334, 231);
            this.chkSaveCred.Name = "chkSaveCred";
            this.chkSaveCred.Size = new System.Drawing.Size(187, 24);
            this.chkSaveCred.TabIndex = 40;
            this.chkSaveCred.Text = "Simpan akun dan driver";
            this.chkSaveCred.UseVisualStyleBackColor = true;
            // 
            // chkUseHeadless
            // 
            this.chkUseHeadless.AutoSize = true;
            this.chkUseHeadless.Location = new System.Drawing.Point(334, 201);
            this.chkUseHeadless.Name = "chkUseHeadless";
            this.chkUseHeadless.Size = new System.Drawing.Size(195, 24);
            this.chkUseHeadless.TabIndex = 39;
            this.chkUseHeadless.Text = "Gunakan mode Headless";
            this.chkUseHeadless.UseVisualStyleBackColor = true;
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(39, 103);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(100, 27);
            this.txtYear.TabIndex = 26;
            // 
            // txtClass
            // 
            this.txtClass.Location = new System.Drawing.Point(39, 47);
            this.txtClass.Name = "txtClass";
            this.txtClass.Size = new System.Drawing.Size(70, 27);
            this.txtClass.TabIndex = 25;
            // 
            // txtChromeDriver
            // 
            this.txtChromeDriver.Location = new System.Drawing.Point(334, 159);
            this.txtChromeDriver.Name = "txtChromeDriver";
            this.txtChromeDriver.ReadOnly = true;
            this.txtChromeDriver.Size = new System.Drawing.Size(193, 27);
            this.txtChromeDriver.TabIndex = 37;
            // 
            // cmdOpenChrome
            // 
            this.cmdOpenChrome.AutoSize = true;
            this.cmdOpenChrome.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(160)))), ((int)(((byte)(230)))));
            this.cmdOpenChrome.Location = new System.Drawing.Point(533, 162);
            this.cmdOpenChrome.Name = "cmdOpenChrome";
            this.cmdOpenChrome.Size = new System.Drawing.Size(37, 20);
            this.cmdOpenChrome.TabIndex = 38;
            this.cmdOpenChrome.TabStop = true;
            this.cmdOpenChrome.Text = "Pilih";
            this.cmdOpenChrome.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdOpenChrome_LinkClicked);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(334, 103);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(236, 27);
            this.txtPassword.TabIndex = 20;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(330, 136);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(106, 20);
            this.label10.TabIndex = 36;
            this.label10.Text = "Chrome driver:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 20);
            this.label7.TabIndex = 24;
            this.label7.Text = "Tahun angkatan:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 20);
            this.label6.TabIndex = 23;
            this.label6.Text = "Kelas:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(330, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Username:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(334, 47);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(236, 27);
            this.txtUsername.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(330, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 20);
            this.label4.TabIndex = 17;
            this.label4.Text = "Password:";
            // 
            // txtClassId
            // 
            this.txtClassId.Location = new System.Drawing.Point(39, 159);
            this.txtClassId.Name = "txtClassId";
            this.txtClassId.Size = new System.Drawing.Size(236, 27);
            this.txtClassId.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 20);
            this.label5.TabIndex = 18;
            this.label5.Text = "ID Kelas:";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Controls.Add(this.cmdOpenExcel);
            this.tabPage2.Location = new System.Drawing.Point(4, 44);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(614, 285);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Data";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(16, 64);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(581, 206);
            this.dataGridView1.TabIndex = 20;
            // 
            // cmdOpenExcel
            // 
            this.cmdOpenExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdOpenExcel.Location = new System.Drawing.Point(18, 17);
            this.cmdOpenExcel.Name = "cmdOpenExcel";
            this.cmdOpenExcel.Size = new System.Drawing.Size(130, 31);
            this.cmdOpenExcel.TabIndex = 3;
            this.cmdOpenExcel.Text = "Buka Excel";
            this.cmdOpenExcel.UseVisualStyleBackColor = true;
            this.cmdOpenExcel.Click += new System.EventHandler(this.cmdOpenExcel_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.tabPage3.Controls.Add(this.cmdStop);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.lstLog);
            this.tabPage3.Controls.Add(this.chkMasterStudentList);
            this.tabPage3.Controls.Add(this.chkLecturerStudentList);
            this.tabPage3.Controls.Add(this.chkFinalScore);
            this.tabPage3.Controls.Add(this.lblProgress);
            this.tabPage3.Controls.Add(this.cmdExecute);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.pgProgress);
            this.tabPage3.Location = new System.Drawing.Point(4, 44);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(614, 285);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Proses";
            // 
            // cmdStop
            // 
            this.cmdStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdStop.Location = new System.Drawing.Point(134, 203);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(90, 33);
            this.cmdStop.TabIndex = 41;
            this.cmdStop.Text = "Berhenti";
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(307, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 20);
            this.label11.TabIndex = 40;
            this.label11.Text = "Log:";
            // 
            // lstLog
            // 
            this.lstLog.FormattingEnabled = true;
            this.lstLog.ItemHeight = 20;
            this.lstLog.Location = new System.Drawing.Point(311, 56);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(276, 204);
            this.lstLog.TabIndex = 39;
            // 
            // chkMasterStudentList
            // 
            this.chkMasterStudentList.AutoSize = true;
            this.chkMasterStudentList.Location = new System.Drawing.Point(29, 26);
            this.chkMasterStudentList.Name = "chkMasterStudentList";
            this.chkMasterStudentList.Size = new System.Drawing.Size(195, 24);
            this.chkMasterStudentList.TabIndex = 33;
            this.chkMasterStudentList.Text = "Input Master Student List";
            this.chkMasterStudentList.UseVisualStyleBackColor = true;
            // 
            // chkLecturerStudentList
            // 
            this.chkLecturerStudentList.AutoSize = true;
            this.chkLecturerStudentList.Location = new System.Drawing.Point(29, 56);
            this.chkLecturerStudentList.Name = "chkLecturerStudentList";
            this.chkLecturerStudentList.Size = new System.Drawing.Size(216, 24);
            this.chkLecturerStudentList.TabIndex = 34;
            this.chkLecturerStudentList.Text = "Input Lecture List of Student";
            this.chkLecturerStudentList.UseVisualStyleBackColor = true;
            // 
            // chkFinalScore
            // 
            this.chkFinalScore.AutoSize = true;
            this.chkFinalScore.Location = new System.Drawing.Point(29, 86);
            this.chkFinalScore.Name = "chkFinalScore";
            this.chkFinalScore.Size = new System.Drawing.Size(130, 24);
            this.chkFinalScore.TabIndex = 35;
            this.chkFinalScore.Text = "Input Final Test";
            this.chkFinalScore.UseVisualStyleBackColor = true;
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(85, 136);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(38, 20);
            this.lblProgress.TabIndex = 23;
            this.lblProgress.Text = "Siap";
            // 
            // cmdExecute
            // 
            this.cmdExecute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.cmdExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdExecute.Location = new System.Drawing.Point(29, 203);
            this.cmdExecute.Name = "cmdExecute";
            this.cmdExecute.Size = new System.Drawing.Size(99, 33);
            this.cmdExecute.TabIndex = 4;
            this.cmdExecute.Text = "Mulai";
            this.cmdExecute.UseVisualStyleBackColor = false;
            this.cmdExecute.Click += new System.EventHandler(this.cmdExecute_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 20);
            this.label8.TabIndex = 21;
            this.label8.Text = "Proses:";
            // 
            // pgProgress
            // 
            this.pgProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pgProgress.Location = new System.Drawing.Point(29, 159);
            this.pgProgress.Name = "pgProgress";
            this.pgProgress.Size = new System.Drawing.Size(232, 22);
            this.pgProgress.TabIndex = 22;
            // 
            // ofdExcel
            // 
            this.ofdExcel.Filter = "Excel file (xls/xlsx)|*.xls;*.xlsx";
            // 
            // ClisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(622, 410);
            this.Controls.Add(this.flatTabControl1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "ClisForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CLIS Auto-Fill";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flatTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtClassId;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button cmdExecute;
        private System.Windows.Forms.Button cmdOpenExcel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private KFlearning.Core.Forms.FlatTabControl flatTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtClass;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ProgressBar pgProgress;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.LinkLabel cmdOpenChrome;
        private System.Windows.Forms.TextBox txtChromeDriver;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkMasterStudentList;
        private System.Windows.Forms.CheckBox chkLecturerStudentList;
        private System.Windows.Forms.CheckBox chkFinalScore;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.OpenFileDialog ofdChromeDriver;
        private System.Windows.Forms.Button cmdStop;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.OpenFileDialog ofdExcel;
        private System.Windows.Forms.CheckBox chkSaveCred;
        private System.Windows.Forms.CheckBox chkUseHeadless;
    }
}