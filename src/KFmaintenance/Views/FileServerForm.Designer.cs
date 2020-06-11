namespace KFmaintenance.Views
{
    partial class FileServerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileServerForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmdSelectFolder = new System.Windows.Forms.LinkLabel();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.cmdOpenAddress = new System.Windows.Forms.LinkLabel();
            this.cmdExecute = new System.Windows.Forms.Button();
            this.lblIP = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(454, 61);
            this.panel1.TabIndex = 16;
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
            this.label2.Text = "Ó";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.label1.Location = new System.Drawing.Point(78, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "File Server";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "Direktori server:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "IP:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(222, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 20);
            this.label5.TabIndex = 19;
            this.label5.Text = "Port:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(53, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 20);
            this.label6.TabIndex = 20;
            this.label6.Text = "Akses:";
            // 
            // cmdSelectFolder
            // 
            this.cmdSelectFolder.AutoSize = true;
            this.cmdSelectFolder.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(160)))), ((int)(((byte)(230)))));
            this.cmdSelectFolder.Location = new System.Drawing.Point(370, 113);
            this.cmdSelectFolder.Name = "cmdSelectFolder";
            this.cmdSelectFolder.Size = new System.Drawing.Size(37, 20);
            this.cmdSelectFolder.TabIndex = 21;
            this.cmdSelectFolder.TabStop = true;
            this.cmdSelectFolder.Text = "Pilih";
            this.cmdSelectFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdOpenFolder_LinkClicked);
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(50, 110);
            this.txtSource.Name = "txtSource";
            this.txtSource.ReadOnly = true;
            this.txtSource.Size = new System.Drawing.Size(314, 27);
            this.txtSource.TabIndex = 22;
            // 
            // cmdOpenAddress
            // 
            this.cmdOpenAddress.AutoSize = true;
            this.cmdOpenAddress.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(160)))), ((int)(((byte)(230)))));
            this.cmdOpenAddress.Location = new System.Drawing.Point(108, 186);
            this.cmdOpenAddress.Name = "cmdOpenAddress";
            this.cmdOpenAddress.Size = new System.Drawing.Size(18, 20);
            this.cmdOpenAddress.TabIndex = 23;
            this.cmdOpenAddress.TabStop = true;
            this.cmdOpenAddress.Text = "...";
            this.cmdOpenAddress.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdOpenAddress_LinkClicked);
            // 
            // cmdExecute
            // 
            this.cmdExecute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.cmdExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdExecute.Location = new System.Drawing.Point(321, 238);
            this.cmdExecute.Name = "cmdExecute";
            this.cmdExecute.Size = new System.Drawing.Size(99, 31);
            this.cmdExecute.TabIndex = 24;
            this.cmdExecute.Text = "Aktifkan";
            this.cmdExecute.UseVisualStyleBackColor = false;
            this.cmdExecute.Click += new System.EventHandler(this.cmdExecute_Click);
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(108, 161);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(18, 20);
            this.lblIP.TabIndex = 25;
            this.lblIP.Text = "...";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(266, 161);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(18, 20);
            this.lblPort.TabIndex = 26;
            this.lblPort.Text = "...";
            // 
            // fbd
            // 
            this.fbd.Description = "Pilih folder untuk dibagikan.";
            // 
            // FileServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(454, 297);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.cmdExecute);
            this.Controls.Add(this.cmdOpenAddress);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.cmdSelectFolder);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "FileServerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Server";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel cmdSelectFolder;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.LinkLabel cmdOpenAddress;
        private System.Windows.Forms.Button cmdExecute;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.FolderBrowserDialog fbd;
    }
}