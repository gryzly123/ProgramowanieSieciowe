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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
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
            this.ButtonSendMessage.Enabled = false;
            this.ButtonSendMessage.Location = new System.Drawing.Point(252, 16);
            this.ButtonSendMessage.Name = "ButtonSendMessage";
            this.ButtonSendMessage.Size = new System.Drawing.Size(114, 33);
            this.ButtonSendMessage.TabIndex = 8;
            this.ButtonSendMessage.Text = "Send message";
            this.ButtonSendMessage.UseVisualStyleBackColor = true;
            this.ButtonSendMessage.Click += new System.EventHandler(this.ButtonSendMessage_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 55);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(354, 20);
            this.textBox1.TabIndex = 9;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 81);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(354, 20);
            this.textBox2.TabIndex = 10;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(12, 107);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(354, 178);
            this.textBox3.TabIndex = 11;
            // 
            // SmtpWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 452);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
    }
}

