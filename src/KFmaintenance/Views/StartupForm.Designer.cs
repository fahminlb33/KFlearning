namespace KFmaintenance.Views
{
    partial class StartupForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartupForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tmrServer = new System.Windows.Forms.Timer(this.components);
            this.tray = new System.Windows.Forms.NotifyIcon(this.components);
            this.ctxTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.flatTabControl1 = new KFlearning.Core.Forms.FlatTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblOSArch = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblOSVersion = new System.Windows.Forms.Label();
            this.lblCpu = new System.Windows.Forms.Label();
            this.lblRam = new System.Windows.Forms.Label();
            this.lblOS = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cmdBroadcastShutdown = new System.Windows.Forms.Button();
            this.cmdSend = new System.Windows.Forms.Button();
            this.chkRemoteShutdown = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkWallpaper = new System.Windows.Forms.CheckBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.chkDesktop = new System.Windows.Forms.CheckBox();
            this.chkWriteProtect = new System.Windows.Forms.CheckBox();
            this.chkControlPanel = new System.Windows.Forms.CheckBox();
            this.chkRegistry = new System.Windows.Forms.CheckBox();
            this.chkTaskManager = new System.Windows.Forms.CheckBox();
            this.pnWallpaper = new System.Windows.Forms.Panel();
            this.rdWDefault = new System.Windows.Forms.RadioButton();
            this.lblFileName = new System.Windows.Forms.Label();
            this.rdWCustom = new System.Windows.Forms.RadioButton();
            this.cmdBrowseWallpaper = new System.Windows.Forms.LinkLabel();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.ctxTray.SuspendLayout();
            this.flatTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.pnWallpaper.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblVersion);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(140, 316);
            this.panel1.TabIndex = 13;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::KFmaintenance.Properties.Resources.KFmaintenance_logo48;
            this.pictureBox1.Location = new System.Drawing.Point(46, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(10, 249);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "KFmaintenance";
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblVersion.Location = new System.Drawing.Point(11, 270);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(120, 40);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "VERSION";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.label3.Location = new System.Drawing.Point(152, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(211, 37);
            this.label3.TabIndex = 14;
            this.label3.Text = "Selamat datang!";
            // 
            // tmrServer
            // 
            this.tmrServer.Interval = 1000;
            // 
            // tray
            // 
            this.tray.ContextMenuStrip = this.ctxTray;
            this.tray.Icon = ((System.Drawing.Icon)(resources.GetObject("tray.Icon")));
            this.tray.Visible = true;
            this.tray.DoubleClick += new System.EventHandler(this.tray_DoubleClick);
            // 
            // ctxTray
            // 
            this.ctxTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpen,
            this.toolStripSeparator1,
            this.mnuExit});
            this.ctxTray.Name = "ctxTray";
            this.ctxTray.Size = new System.Drawing.Size(192, 54);
            // 
            // mnuOpen
            // 
            this.mnuOpen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.Size = new System.Drawing.Size(191, 22);
            this.mnuOpen.Text = "Buka KFmaintenance";
            this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(188, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(191, 22);
            this.mnuExit.Text = "Keluar";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // flatTabControl1
            // 
            this.flatTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flatTabControl1.Controls.Add(this.tabPage1);
            this.flatTabControl1.Controls.Add(this.tabPage2);
            this.flatTabControl1.Controls.Add(this.tabPage3);
            this.flatTabControl1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.flatTabControl1.ItemSize = new System.Drawing.Size(120, 40);
            this.flatTabControl1.Location = new System.Drawing.Point(139, 61);
            this.flatTabControl1.Name = "flatTabControl1";
            this.flatTabControl1.SelectedIndex = 0;
            this.flatTabControl1.Size = new System.Drawing.Size(469, 255);
            this.flatTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.flatTabControl1.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.tabPage1.Controls.Add(this.lblOSArch);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.lblOSVersion);
            this.tabPage1.Controls.Add(this.lblCpu);
            this.tabPage1.Controls.Add(this.lblRam);
            this.tabPage1.Controls.Add(this.lblOS);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 44);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(461, 207);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Diagnosis";
            // 
            // lblOSArch
            // 
            this.lblOSArch.AutoSize = true;
            this.lblOSArch.Location = new System.Drawing.Point(175, 81);
            this.lblOSArch.Name = "lblOSArch";
            this.lblOSArch.Size = new System.Drawing.Size(18, 19);
            this.lblOSArch.TabIndex = 10;
            this.lblOSArch.Text = "...";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(58, 81);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 19);
            this.label12.TabIndex = 9;
            this.label12.Text = "Architecture";
            // 
            // lblOSVersion
            // 
            this.lblOSVersion.AutoSize = true;
            this.lblOSVersion.Location = new System.Drawing.Point(175, 62);
            this.lblOSVersion.Name = "lblOSVersion";
            this.lblOSVersion.Size = new System.Drawing.Size(18, 19);
            this.lblOSVersion.TabIndex = 8;
            this.lblOSVersion.Text = "...";
            // 
            // lblCpu
            // 
            this.lblCpu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCpu.Location = new System.Drawing.Point(175, 132);
            this.lblCpu.Name = "lblCpu";
            this.lblCpu.Size = new System.Drawing.Size(222, 42);
            this.lblCpu.TabIndex = 7;
            this.lblCpu.Text = "...";
            // 
            // lblRam
            // 
            this.lblRam.AutoSize = true;
            this.lblRam.Location = new System.Drawing.Point(175, 113);
            this.lblRam.Name = "lblRam";
            this.lblRam.Size = new System.Drawing.Size(18, 19);
            this.lblRam.TabIndex = 6;
            this.lblRam.Text = "...";
            // 
            // lblOS
            // 
            this.lblOS.AutoSize = true;
            this.lblOS.Location = new System.Drawing.Point(175, 43);
            this.lblOS.Name = "lblOS";
            this.lblOS.Size = new System.Drawing.Size(18, 19);
            this.lblOS.TabIndex = 5;
            this.lblOS.Text = "...";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(58, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 19);
            this.label6.TabIndex = 3;
            this.label6.Text = "CPU";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(58, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 19);
            this.label5.TabIndex = 2;
            this.label5.Text = "Version";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(58, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 19);
            this.label4.TabIndex = 1;
            this.label4.Text = "RAM";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "OS";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.cmdBroadcastShutdown);
            this.tabPage2.Controls.Add(this.cmdSend);
            this.tabPage2.Controls.Add(this.chkRemoteShutdown);
            this.tabPage2.Location = new System.Drawing.Point(4, 44);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(461, 207);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Jaringan";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(196, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 19);
            this.label7.TabIndex = 6;
            this.label7.Text = "Tidak mengirim.";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(307, 138);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(124, 19);
            this.label14.TabIndex = 5;
            this.label14.Text = "Remote shutdown:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(134, 41);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 19);
            this.label13.TabIndex = 4;
            this.label13.Text = "Torrent:";
            // 
            // cmdBroadcastShutdown
            // 
            this.cmdBroadcastShutdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdBroadcastShutdown.Location = new System.Drawing.Point(311, 160);
            this.cmdBroadcastShutdown.Name = "cmdBroadcastShutdown";
            this.cmdBroadcastShutdown.Size = new System.Drawing.Size(130, 29);
            this.cmdBroadcastShutdown.TabIndex = 2;
            this.cmdBroadcastShutdown.Text = "Broadcast";
            this.cmdBroadcastShutdown.UseVisualStyleBackColor = true;
            this.cmdBroadcastShutdown.Click += new System.EventHandler(this.cmdBroadcastShutdown_Click);
            // 
            // cmdSend
            // 
            this.cmdSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSend.Location = new System.Drawing.Point(138, 63);
            this.cmdSend.Name = "cmdSend";
            this.cmdSend.Size = new System.Drawing.Size(164, 29);
            this.cmdSend.TabIndex = 1;
            this.cmdSend.Tag = "SEND";
            this.cmdSend.Text = "Kirim Torrent";
            this.cmdSend.UseVisualStyleBackColor = true;
            this.cmdSend.Click += new System.EventHandler(this.cmdSend_Click);
            // 
            // chkRemoteShutdown
            // 
            this.chkRemoteShutdown.AutoSize = true;
            this.chkRemoteShutdown.Checked = true;
            this.chkRemoteShutdown.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRemoteShutdown.Location = new System.Drawing.Point(29, 164);
            this.chkRemoteShutdown.Name = "chkRemoteShutdown";
            this.chkRemoteShutdown.Size = new System.Drawing.Size(191, 23);
            this.chkRemoteShutdown.TabIndex = 0;
            this.chkRemoteShutdown.Text = "Aktifkan remote shutdown";
            this.chkRemoteShutdown.UseVisualStyleBackColor = true;
            this.chkRemoteShutdown.CheckedChanged += new System.EventHandler(this.chkRemoteShutdown_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.tabPage3.Controls.Add(this.txtPassword);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.chkWallpaper);
            this.tabPage3.Controls.Add(this.cmdSave);
            this.tabPage3.Controls.Add(this.chkDesktop);
            this.tabPage3.Controls.Add(this.chkWriteProtect);
            this.tabPage3.Controls.Add(this.chkControlPanel);
            this.tabPage3.Controls.Add(this.chkRegistry);
            this.tabPage3.Controls.Add(this.chkTaskManager);
            this.tabPage3.Controls.Add(this.pnWallpaper);
            this.tabPage3.Location = new System.Drawing.Point(4, 44);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(461, 207);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Sistem";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(90, 168);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(161, 25);
            this.txtPassword.TabIndex = 41;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 170);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 19);
            this.label8.TabIndex = 40;
            this.label8.Text = "Password:";
            // 
            // chkWallpaper
            // 
            this.chkWallpaper.AutoSize = true;
            this.chkWallpaper.Location = new System.Drawing.Point(215, 36);
            this.chkWallpaper.Name = "chkWallpaper";
            this.chkWallpaper.Size = new System.Drawing.Size(123, 23);
            this.chkWallpaper.TabIndex = 38;
            this.chkWallpaper.Text = "Kunci wallpaper";
            this.chkWallpaper.UseVisualStyleBackColor = true;
            this.chkWallpaper.CheckedChanged += new System.EventHandler(this.chkWallpaper_CheckedChanged);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.cmdSave.FlatAppearance.BorderSize = 0;
            this.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSave.Location = new System.Drawing.Point(348, 165);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(98, 29);
            this.cmdSave.TabIndex = 32;
            this.cmdSave.Text = "Simpan";
            this.cmdSave.UseVisualStyleBackColor = false;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // chkDesktop
            // 
            this.chkDesktop.AutoSize = true;
            this.chkDesktop.Location = new System.Drawing.Point(215, 11);
            this.chkDesktop.Name = "chkDesktop";
            this.chkDesktop.Size = new System.Drawing.Size(114, 23);
            this.chkDesktop.TabIndex = 37;
            this.chkDesktop.Text = "Kunci desktop";
            this.chkDesktop.UseVisualStyleBackColor = true;
            // 
            // chkWriteProtect
            // 
            this.chkWriteProtect.AutoSize = true;
            this.chkWriteProtect.Location = new System.Drawing.Point(16, 11);
            this.chkWriteProtect.Name = "chkWriteProtect";
            this.chkWriteProtect.Size = new System.Drawing.Size(141, 23);
            this.chkWriteProtect.TabIndex = 33;
            this.chkWriteProtect.Text = "Kunci copy ke USB";
            this.chkWriteProtect.UseVisualStyleBackColor = true;
            // 
            // chkControlPanel
            // 
            this.chkControlPanel.AutoSize = true;
            this.chkControlPanel.Location = new System.Drawing.Point(16, 85);
            this.chkControlPanel.Name = "chkControlPanel";
            this.chkControlPanel.Size = new System.Drawing.Size(148, 23);
            this.chkControlPanel.TabIndex = 36;
            this.chkControlPanel.Text = "Kunci Control Panel";
            this.chkControlPanel.UseVisualStyleBackColor = true;
            // 
            // chkRegistry
            // 
            this.chkRegistry.AutoSize = true;
            this.chkRegistry.Location = new System.Drawing.Point(16, 36);
            this.chkRegistry.Name = "chkRegistry";
            this.chkRegistry.Size = new System.Drawing.Size(154, 23);
            this.chkRegistry.TabIndex = 34;
            this.chkRegistry.Text = "Kunci Registry Editor";
            this.chkRegistry.UseVisualStyleBackColor = true;
            // 
            // chkTaskManager
            // 
            this.chkTaskManager.AutoSize = true;
            this.chkTaskManager.Location = new System.Drawing.Point(16, 61);
            this.chkTaskManager.Name = "chkTaskManager";
            this.chkTaskManager.Size = new System.Drawing.Size(149, 23);
            this.chkTaskManager.TabIndex = 35;
            this.chkTaskManager.Text = "Kunci Task Manager";
            this.chkTaskManager.UseVisualStyleBackColor = true;
            // 
            // pnWallpaper
            // 
            this.pnWallpaper.Controls.Add(this.rdWDefault);
            this.pnWallpaper.Controls.Add(this.lblFileName);
            this.pnWallpaper.Controls.Add(this.rdWCustom);
            this.pnWallpaper.Controls.Add(this.cmdBrowseWallpaper);
            this.pnWallpaper.Enabled = false;
            this.pnWallpaper.Location = new System.Drawing.Point(230, 48);
            this.pnWallpaper.Name = "pnWallpaper";
            this.pnWallpaper.Size = new System.Drawing.Size(221, 92);
            this.pnWallpaper.TabIndex = 39;
            // 
            // rdWDefault
            // 
            this.rdWDefault.AutoSize = true;
            this.rdWDefault.Location = new System.Drawing.Point(0, 14);
            this.rdWDefault.Name = "rdWDefault";
            this.rdWDefault.Size = new System.Drawing.Size(190, 23);
            this.rdWDefault.TabIndex = 28;
            this.rdWDefault.Text = "Gunakan wallpaper default";
            this.rdWDefault.UseVisualStyleBackColor = true;
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(22, 63);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(18, 19);
            this.lblFileName.TabIndex = 27;
            this.lblFileName.Text = "...";
            // 
            // rdWCustom
            // 
            this.rdWCustom.AutoSize = true;
            this.rdWCustom.Location = new System.Drawing.Point(0, 39);
            this.rdWCustom.Name = "rdWCustom";
            this.rdWCustom.Size = new System.Drawing.Size(165, 23);
            this.rdWCustom.TabIndex = 24;
            this.rdWCustom.Text = "Gunakan wallpaper ini:";
            this.rdWCustom.UseVisualStyleBackColor = true;
            // 
            // cmdBrowseWallpaper
            // 
            this.cmdBrowseWallpaper.AutoSize = true;
            this.cmdBrowseWallpaper.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(181)))), ((int)(((byte)(198)))));
            this.cmdBrowseWallpaper.Location = new System.Drawing.Point(166, 41);
            this.cmdBrowseWallpaper.Name = "cmdBrowseWallpaper";
            this.cmdBrowseWallpaper.Size = new System.Drawing.Size(34, 19);
            this.cmdBrowseWallpaper.TabIndex = 26;
            this.cmdBrowseWallpaper.TabStop = true;
            this.cmdBrowseWallpaper.Text = "Pilih";
            this.cmdBrowseWallpaper.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdBrowseWallpaper_LinkClicked);
            // 
            // StartupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(608, 316);
            this.Controls.Add(this.flatTabControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StartupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KFmaintenance";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ctxTray.ResumeLayout(false);
            this.flatTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.pnWallpaper.ResumeLayout(false);
            this.pnWallpaper.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label3;
        private KFlearning.Core.Forms.FlatTabControl flatTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox chkWallpaper;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.CheckBox chkDesktop;
        private System.Windows.Forms.CheckBox chkWriteProtect;
        private System.Windows.Forms.CheckBox chkControlPanel;
        private System.Windows.Forms.CheckBox chkRegistry;
        private System.Windows.Forms.CheckBox chkTaskManager;
        private System.Windows.Forms.Panel pnWallpaper;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.LinkLabel cmdBrowseWallpaper;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkRemoteShutdown;
        private System.Windows.Forms.Label lblOSVersion;
        private System.Windows.Forms.Label lblCpu;
        private System.Windows.Forms.Label lblRam;
        private System.Windows.Forms.Label lblOS;
        private System.Windows.Forms.Button cmdBroadcastShutdown;
        private System.Windows.Forms.Button cmdSend;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblOSArch;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Timer tmrServer;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NotifyIcon tray;
        private System.Windows.Forms.ContextMenuStrip ctxTray;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.RadioButton rdWDefault;
        private System.Windows.Forms.RadioButton rdWCustom;
    }
}

