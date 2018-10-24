namespace MailClient
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
            this.ButtonConnectPop = new System.Windows.Forms.Button();
            this.ListboxLog = new System.Windows.Forms.ListBox();
            this.ListboxMessages = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ButtonShowLog = new System.Windows.Forms.Button();
            this.ButtonConfig = new System.Windows.Forms.Button();
            this.ButtonSendMessage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButtonConnectPop
            // 
            this.ButtonConnectPop.Location = new System.Drawing.Point(12, 16);
            this.ButtonConnectPop.Name = "ButtonConnectPop";
            this.ButtonConnectPop.Size = new System.Drawing.Size(114, 33);
            this.ButtonConnectPop.TabIndex = 0;
            this.ButtonConnectPop.Text = "Start connection";
            this.ButtonConnectPop.UseVisualStyleBackColor = true;
            this.ButtonConnectPop.Click += new System.EventHandler(this.ButtonConnectPop_Click);
            // 
            // ListboxLog
            // 
            this.ListboxLog.FormattingEnabled = true;
            this.ListboxLog.Location = new System.Drawing.Point(12, 226);
            this.ListboxLog.Name = "ListboxLog";
            this.ListboxLog.Size = new System.Drawing.Size(474, 212);
            this.ListboxLog.TabIndex = 2;
            // 
            // ListboxMessages
            // 
            this.ListboxMessages.FormattingEnabled = true;
            this.ListboxMessages.Location = new System.Drawing.Point(12, 69);
            this.ListboxMessages.Name = "ListboxMessages";
            this.ListboxMessages.Size = new System.Drawing.Size(474, 134);
            this.ListboxMessages.TabIndex = 3;
            this.ListboxMessages.SelectedIndexChanged += new System.EventHandler(this.ListboxMessages_SelectedMessage);
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
            this.label2.Location = new System.Drawing.Point(12, 210);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Server log:";
            // 
            // ButtonShowLog
            // 
            this.ButtonShowLog.Location = new System.Drawing.Point(372, 16);
            this.ButtonShowLog.Name = "ButtonShowLog";
            this.ButtonShowLog.Size = new System.Drawing.Size(114, 33);
            this.ButtonShowLog.TabIndex = 6;
            this.ButtonShowLog.Text = "Show server log";
            this.ButtonShowLog.UseVisualStyleBackColor = true;
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
            // ButtonSendMessage
            // 
            this.ButtonSendMessage.Enabled = false;
            this.ButtonSendMessage.Location = new System.Drawing.Point(252, 16);
            this.ButtonSendMessage.Name = "ButtonSendMessage";
            this.ButtonSendMessage.Size = new System.Drawing.Size(114, 33);
            this.ButtonSendMessage.TabIndex = 8;
            this.ButtonSendMessage.Text = "Send message";
            this.ButtonSendMessage.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 452);
            this.Controls.Add(this.ButtonSendMessage);
            this.Controls.Add(this.ButtonConfig);
            this.Controls.Add(this.ButtonShowLog);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListboxMessages);
            this.Controls.Add(this.ListboxLog);
            this.Controls.Add(this.ButtonConnectPop);
            this.Name = "MainWindow";
            this.Text = "MailClient (Krzysztof Niedźwiecki)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonConnectPop;
        private System.Windows.Forms.ListBox ListboxLog;
        private System.Windows.Forms.ListBox ListboxMessages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ButtonShowLog;
        private System.Windows.Forms.Button ButtonConfig;
        private System.Windows.Forms.Button ButtonSendMessage;
    }
}

