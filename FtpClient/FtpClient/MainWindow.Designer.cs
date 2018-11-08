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
            this.ListboxMessages = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ButtonConfig = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButtonConnectFtp
            // 
            this.ButtonConnectFtp.Location = new System.Drawing.Point(12, 16);
            this.ButtonConnectFtp.Name = "ButtonConnectFtp";
            this.ButtonConnectFtp.Size = new System.Drawing.Size(114, 33);
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
            // ListboxMessages
            // 
            this.ListboxMessages.FormattingEnabled = true;
            this.ListboxMessages.Location = new System.Drawing.Point(12, 69);
            this.ListboxMessages.Name = "ListboxMessages";
            this.ListboxMessages.Size = new System.Drawing.Size(354, 212);
            this.ListboxMessages.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Received messages:";
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
            this.ButtonConfig.Location = new System.Drawing.Point(132, 16);
            this.ButtonConfig.Name = "ButtonConfig";
            this.ButtonConfig.Size = new System.Drawing.Size(114, 33);
            this.ButtonConfig.TabIndex = 7;
            this.ButtonConfig.Text = "Open Config";
            this.ButtonConfig.UseVisualStyleBackColor = true;
            this.ButtonConfig.Click += new System.EventHandler(this.ButtonConfig_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 452);
            this.Controls.Add(this.ButtonConfig);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListboxMessages);
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
        private System.Windows.Forms.ListBox ListboxMessages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ButtonConfig;
    }
}

