namespace FtpClient
{
    partial class MainWindow
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
            this.ButtonConnectFtp = new System.Windows.Forms.Button();
            this.ListboxLog = new System.Windows.Forms.ListBox();
            this.ListboxDirContents = new System.Windows.Forms.ListBox();
            this.LabelCurrentPath = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ButtonConfig = new System.Windows.Forms.Button();
            this.ButtonGoUp = new System.Windows.Forms.Button();
            this.ButtonScanRecursive = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButtonConnectFtp
            // 
            this.ButtonConnectFtp.Location = new System.Drawing.Point(12, 16);
            this.ButtonConnectFtp.Name = "ButtonConnectFtp";
            this.ButtonConnectFtp.Size = new System.Drawing.Size(84, 33);
            this.ButtonConnectFtp.TabIndex = 0;
            this.ButtonConnectFtp.Text = "Start client";
            this.ButtonConnectFtp.UseVisualStyleBackColor = true;
            this.ButtonConnectFtp.Click += new System.EventHandler(this.ButtonConnectFtp_Click);
            // 
            // ListboxLog
            // 
            this.ListboxLog.FormattingEnabled = true;
            this.ListboxLog.Location = new System.Drawing.Point(12, 304);
            this.ListboxLog.Name = "ListboxLog";
            this.ListboxLog.Size = new System.Drawing.Size(354, 134);
            this.ListboxLog.TabIndex = 2;
            // 
            // ListboxDirContents
            // 
            this.ListboxDirContents.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ListboxDirContents.FormattingEnabled = true;
            this.ListboxDirContents.Location = new System.Drawing.Point(12, 69);
            this.ListboxDirContents.Name = "ListboxDirContents";
            this.ListboxDirContents.Size = new System.Drawing.Size(354, 212);
            this.ListboxDirContents.TabIndex = 3;
            this.ListboxDirContents.SelectedIndexChanged += new System.EventHandler(this.ListboxDirContents_SelectedIndexChanged);
            // 
            // LabelCurrentPath
            // 
            this.LabelCurrentPath.AutoSize = true;
            this.LabelCurrentPath.Location = new System.Drawing.Point(12, 53);
            this.LabelCurrentPath.Name = "LabelCurrentPath";
            this.LabelCurrentPath.Size = new System.Drawing.Size(85, 13);
            this.LabelCurrentPath.TabIndex = 4;
            this.LabelCurrentPath.Text = "Server contents:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 288);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Server log:";
            // 
            // ButtonConfig
            // 
            this.ButtonConfig.Location = new System.Drawing.Point(102, 16);
            this.ButtonConfig.Name = "ButtonConfig";
            this.ButtonConfig.Size = new System.Drawing.Size(84, 33);
            this.ButtonConfig.TabIndex = 7;
            this.ButtonConfig.Text = "Open Config";
            this.ButtonConfig.UseVisualStyleBackColor = true;
            this.ButtonConfig.Click += new System.EventHandler(this.ButtonConfig_Click);
            // 
            // ButtonGoUp
            // 
            this.ButtonGoUp.Enabled = false;
            this.ButtonGoUp.Location = new System.Drawing.Point(217, 16);
            this.ButtonGoUp.Name = "ButtonGoUp";
            this.ButtonGoUp.Size = new System.Drawing.Size(47, 33);
            this.ButtonGoUp.TabIndex = 8;
            this.ButtonGoUp.Text = "Go Up";
            this.ButtonGoUp.UseVisualStyleBackColor = true;
            this.ButtonGoUp.Click += new System.EventHandler(this.ButtonGoUp_Click);
            // 
            // ButtonScanRecursive
            // 
            this.ButtonScanRecursive.Enabled = false;
            this.ButtonScanRecursive.Location = new System.Drawing.Point(270, 16);
            this.ButtonScanRecursive.Name = "ButtonScanRecursive";
            this.ButtonScanRecursive.Size = new System.Drawing.Size(96, 33);
            this.ButtonScanRecursive.TabIndex = 9;
            this.ButtonScanRecursive.Text = "Recursive scan";
            this.ButtonScanRecursive.UseVisualStyleBackColor = true;
            this.ButtonScanRecursive.Click += new System.EventHandler(this.ButtonScanRecursive_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 452);
            this.Controls.Add(this.ButtonScanRecursive);
            this.Controls.Add(this.ButtonGoUp);
            this.Controls.Add(this.ButtonConfig);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LabelCurrentPath);
            this.Controls.Add(this.ListboxDirContents);
            this.Controls.Add(this.ListboxLog);
            this.Controls.Add(this.ButtonConnectFtp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainWindow";
            this.Text = "FtpClient (Krzysztof Niedźwiecki)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonConnectFtp;
        private System.Windows.Forms.ListBox ListboxLog;
        private System.Windows.Forms.ListBox ListboxDirContents;
        private System.Windows.Forms.Label LabelCurrentPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ButtonConfig;
        private System.Windows.Forms.Button ButtonGoUp;
        private System.Windows.Forms.Button ButtonScanRecursive;
    }
}

