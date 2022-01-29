
namespace KFlearning.App.Views
{
    partial class FlutterInstallView
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
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInstallPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFlutterVersion = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.prgProgress = new System.Windows.Forms.ProgressBar();
            this.cmdBrowse = new System.Windows.Forms.LinkLabel();
            this.lblPercent = new System.Windows.Forms.Label();
            this.cmdInstall = new System.Windows.Forms.Button();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(387, 48);
            this.panel1.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Wingdings", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(17, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "Þ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(59, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(167, 28);
            this.label5.TabIndex = 1;
            this.label5.Text = "Install Flutter SDK";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "Lokasi install:";
            // 
            // txtInstallPath
            // 
            this.txtInstallPath.Location = new System.Drawing.Point(132, 94);
            this.txtInstallPath.Name = "txtInstallPath";
            this.txtInstallPath.ReadOnly = true;
            this.txtInstallPath.Size = new System.Drawing.Size(168, 23);
            this.txtInstallPath.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 18;
            this.label2.Text = "Versi Flutter:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 15);
            this.label4.TabIndex = 19;
            this.label4.Text = "Proses instalasi:";
            // 
            // lblFlutterVersion
            // 
            this.lblFlutterVersion.AutoSize = true;
            this.lblFlutterVersion.Location = new System.Drawing.Point(129, 72);
            this.lblFlutterVersion.Name = "lblFlutterVersion";
            this.lblFlutterVersion.Size = new System.Drawing.Size(16, 15);
            this.lblFlutterVersion.TabIndex = 20;
            this.lblFlutterVersion.Text = "...";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(38, 187);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(139, 15);
            this.lblStatus.TabIndex = 21;
            this.lblStatus.Text = "Menunggu versi Flutter...";
            // 
            // prgProgress
            // 
            this.prgProgress.Location = new System.Drawing.Point(41, 160);
            this.prgProgress.Name = "prgProgress";
            this.prgProgress.Size = new System.Drawing.Size(304, 18);
            this.prgProgress.TabIndex = 22;
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.AutoSize = true;
            this.cmdBrowse.Enabled = false;
            this.cmdBrowse.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(160)))), ((int)(((byte)(230)))));
            this.cmdBrowse.Location = new System.Drawing.Point(306, 97);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(39, 15);
            this.cmdBrowse.TabIndex = 23;
            this.cmdBrowse.TabStop = true;
            this.cmdBrowse.Text = "Pilih...";
            this.cmdBrowse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdBrowse_LinkClicked);
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.Location = new System.Drawing.Point(310, 187);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(23, 15);
            this.lblPercent.TabIndex = 24;
            this.lblPercent.Text = "0%";
            // 
            // cmdInstall
            // 
            this.cmdInstall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.cmdInstall.Enabled = false;
            this.cmdInstall.FlatAppearance.BorderSize = 0;
            this.cmdInstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdInstall.Location = new System.Drawing.Point(234, 227);
            this.cmdInstall.Name = "cmdInstall";
            this.cmdInstall.Size = new System.Drawing.Size(111, 32);
            this.cmdInstall.TabIndex = 25;
            this.cmdInstall.Text = "Install Flutter";
            this.cmdInstall.UseVisualStyleBackColor = false;
            this.cmdInstall.Click += new System.EventHandler(this.cmdInstall_Click);
            // 
            // FlutterInstallView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(387, 285);
            this.Controls.Add(this.cmdInstall);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.cmdBrowse);
            this.Controls.Add(this.prgProgress);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblFlutterVersion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtInstallPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FlutterInstallView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Install Flutter SDK";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInstallPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblFlutterVersion;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar prgProgress;
        private System.Windows.Forms.LinkLabel cmdBrowse;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.Button cmdInstall;
        private System.Windows.Forms.FolderBrowserDialog fbd;
    }
}