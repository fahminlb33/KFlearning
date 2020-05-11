namespace KFmaintenance.Views
{
    partial class AboutForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdKodesiana = new System.Windows.Forms.LinkLabel();
            this.cmdInstagram = new System.Windows.Forms.LinkLabel();
            this.cmdGitHub = new System.Windows.Forms.LinkLabel();
            this.lblVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::KFmaintenance.Properties.Resources.KFmaintenance_logo48;
            this.pictureBox1.Location = new System.Drawing.Point(17, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(101, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "KFmaintenance";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(85, 269);
            this.panel1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "E-learning terpadu by LABKOM.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(106, 219);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 30);
            this.label3.TabIndex = 4;
            this.label3.Text = "Hak Cipta (C) Fahmi Noor Fiqri 2020.\r\nDilisensikan di bawah lisensi MIT.";
            // 
            // cmdKodesiana
            // 
            this.cmdKodesiana.AutoSize = true;
            this.cmdKodesiana.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(160)))), ((int)(((byte)(230)))));
            this.cmdKodesiana.Location = new System.Drawing.Point(106, 186);
            this.cmdKodesiana.Name = "cmdKodesiana";
            this.cmdKodesiana.Size = new System.Drawing.Size(88, 15);
            this.cmdKodesiana.TabIndex = 5;
            this.cmdKodesiana.TabStop = true;
            this.cmdKodesiana.Text = "Kodesiana.com";
            this.cmdKodesiana.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdKodesiana_LinkClicked);
            // 
            // cmdInstagram
            // 
            this.cmdInstagram.AutoSize = true;
            this.cmdInstagram.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(160)))), ((int)(((byte)(230)))));
            this.cmdInstagram.Location = new System.Drawing.Point(200, 186);
            this.cmdInstagram.Name = "cmdInstagram";
            this.cmdInstagram.Size = new System.Drawing.Size(60, 15);
            this.cmdInstagram.TabIndex = 6;
            this.cmdInstagram.TabStop = true;
            this.cmdInstagram.Text = "Instagram";
            this.cmdInstagram.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdInstagram_LinkClicked);
            // 
            // cmdGitHub
            // 
            this.cmdGitHub.AutoSize = true;
            this.cmdGitHub.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(160)))), ((int)(((byte)(230)))));
            this.cmdGitHub.Location = new System.Drawing.Point(266, 186);
            this.cmdGitHub.Name = "cmdGitHub";
            this.cmdGitHub.Size = new System.Drawing.Size(45, 15);
            this.cmdGitHub.TabIndex = 7;
            this.cmdGitHub.TabStop = true;
            this.cmdGitHub.Text = "GitHub";
            this.cmdGitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdGitHub_LinkClicked);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(106, 92);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(54, 15);
            this.lblVersion.TabIndex = 8;
            this.lblVersion.Text = "VERSION";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(353, 269);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.cmdGitHub);
            this.Controls.Add(this.cmdInstagram);
            this.Controls.Add(this.cmdKodesiana);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tentang KFlearning";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel cmdKodesiana;
        private System.Windows.Forms.LinkLabel cmdInstagram;
        private System.Windows.Forms.LinkLabel cmdGitHub;
        private System.Windows.Forms.Label lblVersion;
    }
}