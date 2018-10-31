namespace MailClient
{
    partial class Configuration
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
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonAccept = new System.Windows.Forms.Button();
            this.InPopHostname = new System.Windows.Forms.TextBox();
            this.GroupPop = new System.Windows.Forms.GroupBox();
            this.CheckPopSsl = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.InPopRefrate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.InPopPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.InPopUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.InPopPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GroupSmtp = new System.Windows.Forms.GroupBox();
            this.CheckSmtpSsl = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.InSmtpPassword = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.InSmtpLogin = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.InSmtpPort = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.InSmtpHostname = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.GroupPop.SuspendLayout();
            this.GroupSmtp.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hostname";
            // 
            // ButtonAccept
            // 
            this.ButtonAccept.Location = new System.Drawing.Point(365, 208);
            this.ButtonAccept.Name = "ButtonAccept";
            this.ButtonAccept.Size = new System.Drawing.Size(75, 23);
            this.ButtonAccept.TabIndex = 1;
            this.ButtonAccept.Text = "OK";
            this.ButtonAccept.UseVisualStyleBackColor = true;
            this.ButtonAccept.Click += new System.EventHandler(this.ButtonAccept_Click);
            // 
            // InPopHostname
            // 
            this.InPopHostname.Location = new System.Drawing.Point(94, 23);
            this.InPopHostname.Name = "InPopHostname";
            this.InPopHostname.Size = new System.Drawing.Size(100, 20);
            this.InPopHostname.TabIndex = 2;
            this.InPopHostname.TextChanged += new System.EventHandler(this.InHostname_TextChanged);
            // 
            // GroupPop
            // 
            this.GroupPop.Controls.Add(this.CheckPopSsl);
            this.GroupPop.Controls.Add(this.label6);
            this.GroupPop.Controls.Add(this.InPopRefrate);
            this.GroupPop.Controls.Add(this.label5);
            this.GroupPop.Controls.Add(this.InPopPassword);
            this.GroupPop.Controls.Add(this.label4);
            this.GroupPop.Controls.Add(this.InPopUsername);
            this.GroupPop.Controls.Add(this.label3);
            this.GroupPop.Controls.Add(this.InPopPort);
            this.GroupPop.Controls.Add(this.label2);
            this.GroupPop.Controls.Add(this.InPopHostname);
            this.GroupPop.Controls.Add(this.label1);
            this.GroupPop.Location = new System.Drawing.Point(12, 12);
            this.GroupPop.Name = "GroupPop";
            this.GroupPop.Size = new System.Drawing.Size(211, 190);
            this.GroupPop.TabIndex = 3;
            this.GroupPop.TabStop = false;
            this.GroupPop.Text = "POP3";
            // 
            // CheckPopSsl
            // 
            this.CheckPopSsl.AutoSize = true;
            this.CheckPopSsl.Location = new System.Drawing.Point(94, 155);
            this.CheckPopSsl.Name = "CheckPopSsl";
            this.CheckPopSsl.Size = new System.Drawing.Size(15, 14);
            this.CheckPopSsl.TabIndex = 12;
            this.CheckPopSsl.UseVisualStyleBackColor = true;
            this.CheckPopSsl.CheckedChanged += new System.EventHandler(this.ChecPopSsl_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Use SSL";
            // 
            // InPopRefrate
            // 
            this.InPopRefrate.Location = new System.Drawing.Point(94, 127);
            this.InPopRefrate.Name = "InPopRefrate";
            this.InPopRefrate.Size = new System.Drawing.Size(100, 20);
            this.InPopRefrate.TabIndex = 10;
            this.InPopRefrate.TextChanged += new System.EventHandler(this.InRefrate_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Refresh rate (s)";
            // 
            // InPopPassword
            // 
            this.InPopPassword.Location = new System.Drawing.Point(94, 101);
            this.InPopPassword.Name = "InPopPassword";
            this.InPopPassword.Size = new System.Drawing.Size(100, 20);
            this.InPopPassword.TabIndex = 8;
            this.InPopPassword.TextChanged += new System.EventHandler(this.InPassword_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Password";
            // 
            // InPopUsername
            // 
            this.InPopUsername.Location = new System.Drawing.Point(94, 75);
            this.InPopUsername.Name = "InPopUsername";
            this.InPopUsername.Size = new System.Drawing.Size(100, 20);
            this.InPopUsername.TabIndex = 6;
            this.InPopUsername.TextChanged += new System.EventHandler(this.InUsername_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Username";
            // 
            // InPopPort
            // 
            this.InPopPort.Location = new System.Drawing.Point(94, 49);
            this.InPopPort.Name = "InPopPort";
            this.InPopPort.Size = new System.Drawing.Size(100, 20);
            this.InPopPort.TabIndex = 4;
            this.InPopPort.TextChanged += new System.EventHandler(this.InPort_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port";
            // 
            // GroupSmtp
            // 
            this.GroupSmtp.Controls.Add(this.CheckSmtpSsl);
            this.GroupSmtp.Controls.Add(this.label7);
            this.GroupSmtp.Controls.Add(this.InSmtpPassword);
            this.GroupSmtp.Controls.Add(this.label9);
            this.GroupSmtp.Controls.Add(this.InSmtpLogin);
            this.GroupSmtp.Controls.Add(this.label10);
            this.GroupSmtp.Controls.Add(this.InSmtpPort);
            this.GroupSmtp.Controls.Add(this.label11);
            this.GroupSmtp.Controls.Add(this.InSmtpHostname);
            this.GroupSmtp.Controls.Add(this.label12);
            this.GroupSmtp.Location = new System.Drawing.Point(229, 12);
            this.GroupSmtp.Name = "GroupSmtp";
            this.GroupSmtp.Size = new System.Drawing.Size(211, 190);
            this.GroupSmtp.TabIndex = 13;
            this.GroupSmtp.TabStop = false;
            this.GroupSmtp.Text = "SMTP";
            // 
            // CheckSmtpSsl
            // 
            this.CheckSmtpSsl.AutoSize = true;
            this.CheckSmtpSsl.Location = new System.Drawing.Point(94, 155);
            this.CheckSmtpSsl.Name = "CheckSmtpSsl";
            this.CheckSmtpSsl.Size = new System.Drawing.Size(15, 14);
            this.CheckSmtpSsl.TabIndex = 12;
            this.CheckSmtpSsl.UseVisualStyleBackColor = true;
            this.CheckSmtpSsl.CheckedChanged += new System.EventHandler(this.CheckSmtpSsl_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 156);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Use SSL";
            // 
            // InSmtpPassword
            // 
            this.InSmtpPassword.Location = new System.Drawing.Point(94, 101);
            this.InSmtpPassword.Name = "InSmtpPassword";
            this.InSmtpPassword.Size = new System.Drawing.Size(100, 20);
            this.InSmtpPassword.TabIndex = 8;
            this.InSmtpPassword.TextChanged += new System.EventHandler(this.InSmtpPassword_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 104);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Password";
            // 
            // InSmtpLogin
            // 
            this.InSmtpLogin.Location = new System.Drawing.Point(94, 75);
            this.InSmtpLogin.Name = "InSmtpLogin";
            this.InSmtpLogin.Size = new System.Drawing.Size(100, 20);
            this.InSmtpLogin.TabIndex = 6;
            this.InSmtpLogin.TextChanged += new System.EventHandler(this.InSmtpLogin_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 78);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Username";
            // 
            // InSmtpPort
            // 
            this.InSmtpPort.Location = new System.Drawing.Point(94, 49);
            this.InSmtpPort.Name = "InSmtpPort";
            this.InSmtpPort.Size = new System.Drawing.Size(100, 20);
            this.InSmtpPort.TabIndex = 4;
            this.InSmtpPort.TextChanged += new System.EventHandler(this.InSmtpPort_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(26, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Port";
            // 
            // InSmtpHostname
            // 
            this.InSmtpHostname.Location = new System.Drawing.Point(94, 23);
            this.InSmtpHostname.Name = "InSmtpHostname";
            this.InSmtpHostname.Size = new System.Drawing.Size(100, 20);
            this.InSmtpHostname.TabIndex = 2;
            this.InSmtpHostname.TextChanged += new System.EventHandler(this.InSmtpHostname_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Hostname";
            // 
            // Configuration
            // 
            this.AcceptButton = this.ButtonAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 241);
            this.Controls.Add(this.GroupSmtp);
            this.Controls.Add(this.GroupPop);
            this.Controls.Add(this.ButtonAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Configuration";
            this.Text = "Setup";
            this.GroupPop.ResumeLayout(false);
            this.GroupPop.PerformLayout();
            this.GroupSmtp.ResumeLayout(false);
            this.GroupSmtp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ButtonAccept;
        private System.Windows.Forms.TextBox InPopHostname;
        private System.Windows.Forms.GroupBox GroupPop;
        private System.Windows.Forms.TextBox InPopRefrate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox InPopPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox InPopUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox InPopPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox CheckPopSsl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox GroupSmtp;
        private System.Windows.Forms.TextBox InSmtpPassword;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox InSmtpLogin;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox InSmtpPort;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox InSmtpHostname;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox CheckSmtpSsl;
        private System.Windows.Forms.Label label7;
    }
}