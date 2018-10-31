namespace MailClient
{
    partial class SmtpWindow
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
            this.ListboxLog = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ButtonSendMessage = new System.Windows.Forms.Button();
            this.TextBoxRecipients = new System.Windows.Forms.TextBox();
            this.TextBoxSubject = new System.Windows.Forms.TextBox();
            this.TextBoxMessage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ListboxLog
            // 
            this.ListboxLog.FormattingEnabled = true;
            this.ListboxLog.Location = new System.Drawing.Point(12, 304);
            this.ListboxLog.Name = "ListboxLog";
            this.ListboxLog.Size = new System.Drawing.Size(354, 134);
            this.ListboxLog.TabIndex = 2;
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
            // ButtonSendMessage
            // 
            this.ButtonSendMessage.Location = new System.Drawing.Point(252, 16);
            this.ButtonSendMessage.Name = "ButtonSendMessage";
            this.ButtonSendMessage.Size = new System.Drawing.Size(114, 33);
            this.ButtonSendMessage.TabIndex = 8;
            this.ButtonSendMessage.Text = "Send message";
            this.ButtonSendMessage.UseVisualStyleBackColor = true;
            this.ButtonSendMessage.Click += new System.EventHandler(this.ButtonSendMessage_Click);
            // 
            // TextBoxRecipients
            // 
            this.TextBoxRecipients.Location = new System.Drawing.Point(76, 55);
            this.TextBoxRecipients.Name = "TextBoxRecipients";
            this.TextBoxRecipients.Size = new System.Drawing.Size(290, 20);
            this.TextBoxRecipients.TabIndex = 9;
            // 
            // TextBoxSubject
            // 
            this.TextBoxSubject.Location = new System.Drawing.Point(76, 81);
            this.TextBoxSubject.Name = "TextBoxSubject";
            this.TextBoxSubject.Size = new System.Drawing.Size(290, 20);
            this.TextBoxSubject.TabIndex = 10;
            this.TextBoxSubject.Text = "PS LAB ZIMA 2018 45575";
            // 
            // TextBoxMessage
            // 
            this.TextBoxMessage.Location = new System.Drawing.Point(12, 127);
            this.TextBoxMessage.Multiline = true;
            this.TextBoxMessage.Name = "TextBoxMessage";
            this.TextBoxMessage.Size = new System.Drawing.Size(354, 158);
            this.TextBoxMessage.TabIndex = 11;
            this.TextBoxMessage.Text = "Krzysztof Niedzwiecki";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "To:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Subject:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Message:";
            // 
            // SmtpWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 452);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextBoxMessage);
            this.Controls.Add(this.TextBoxSubject);
            this.Controls.Add(this.TextBoxRecipients);
            this.Controls.Add(this.ButtonSendMessage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ListboxLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SmtpWindow";
            this.Text = "MailClient (Krzysztof Niedźwiecki)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox ListboxLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ButtonSendMessage;
        private System.Windows.Forms.TextBox TextBoxRecipients;
        private System.Windows.Forms.TextBox TextBoxSubject;
        private System.Windows.Forms.TextBox TextBoxMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

