﻿namespace FtpClient
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
            this.InFtpHostname = new System.Windows.Forms.TextBox();
            this.GroupFtp = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.EnableUnixListing = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.CheckFtpSsl = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.InFtpPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.InFtpUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.InFtpPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GroupFtp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EnableUnixListing)).BeginInit();
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
            this.ButtonAccept.Location = new System.Drawing.Point(148, 217);
            this.ButtonAccept.Name = "ButtonAccept";
            this.ButtonAccept.Size = new System.Drawing.Size(75, 23);
            this.ButtonAccept.TabIndex = 1;
            this.ButtonAccept.Text = "OK";
            this.ButtonAccept.UseVisualStyleBackColor = true;
            this.ButtonAccept.Click += new System.EventHandler(this.ButtonAccept_Click);
            // 
            // InFtpHostname
            // 
            this.InFtpHostname.Location = new System.Drawing.Point(94, 23);
            this.InFtpHostname.Name = "InFtpHostname";
            this.InFtpHostname.Size = new System.Drawing.Size(100, 20);
            this.InFtpHostname.TabIndex = 2;
            this.InFtpHostname.TextChanged += new System.EventHandler(this.InHostname_TextChanged);
            // 
            // GroupFtp
            // 
            this.GroupFtp.Controls.Add(this.label8);
            this.GroupFtp.Controls.Add(this.EnableUnixListing);
            this.GroupFtp.Controls.Add(this.label7);
            this.GroupFtp.Controls.Add(this.CheckFtpSsl);
            this.GroupFtp.Controls.Add(this.label6);
            this.GroupFtp.Controls.Add(this.InFtpPassword);
            this.GroupFtp.Controls.Add(this.label4);
            this.GroupFtp.Controls.Add(this.InFtpUsername);
            this.GroupFtp.Controls.Add(this.label3);
            this.GroupFtp.Controls.Add(this.InFtpPort);
            this.GroupFtp.Controls.Add(this.label2);
            this.GroupFtp.Controls.Add(this.InFtpHostname);
            this.GroupFtp.Controls.Add(this.label1);
            this.GroupFtp.Location = new System.Drawing.Point(12, 12);
            this.GroupFtp.Name = "GroupFtp";
            this.GroupFtp.Size = new System.Drawing.Size(211, 199);
            this.GroupFtp.TabIndex = 3;
            this.GroupFtp.TabStop = false;
            this.GroupFtp.Text = "FTP";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(152, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "FileZilla";
            // 
            // EnableUnixListing
            // 
            this.EnableUnixListing.Location = new System.Drawing.Point(85, 147);
            this.EnableUnixListing.Maximum = 1;
            this.EnableUnixListing.Name = "EnableUnixListing";
            this.EnableUnixListing.Size = new System.Drawing.Size(61, 45);
            this.EnableUnixListing.TabIndex = 14;
            this.EnableUnixListing.Scroll += new System.EventHandler(this.EnableUnixListing_Scroll);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 156);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Microsoft FTP";
            // 
            // CheckFtpSsl
            // 
            this.CheckFtpSsl.AutoSize = true;
            this.CheckFtpSsl.Location = new System.Drawing.Point(94, 127);
            this.CheckFtpSsl.Name = "CheckFtpSsl";
            this.CheckFtpSsl.Size = new System.Drawing.Size(15, 14);
            this.CheckFtpSsl.TabIndex = 12;
            this.CheckFtpSsl.UseVisualStyleBackColor = true;
            this.CheckFtpSsl.CheckedChanged += new System.EventHandler(this.ChecFtpSsl_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 128);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Use SSL";
            // 
            // InFtpPassword
            // 
            this.InFtpPassword.Location = new System.Drawing.Point(94, 101);
            this.InFtpPassword.Name = "InFtpPassword";
            this.InFtpPassword.Size = new System.Drawing.Size(100, 20);
            this.InFtpPassword.TabIndex = 8;
            this.InFtpPassword.TextChanged += new System.EventHandler(this.InPassword_TextChanged);
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
            // InFtpUsername
            // 
            this.InFtpUsername.Location = new System.Drawing.Point(94, 75);
            this.InFtpUsername.Name = "InFtpUsername";
            this.InFtpUsername.Size = new System.Drawing.Size(100, 20);
            this.InFtpUsername.TabIndex = 6;
            this.InFtpUsername.TextChanged += new System.EventHandler(this.InUsername_TextChanged);
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
            // InFtpPort
            // 
            this.InFtpPort.Location = new System.Drawing.Point(94, 49);
            this.InFtpPort.Name = "InFtpPort";
            this.InFtpPort.Size = new System.Drawing.Size(100, 20);
            this.InFtpPort.TabIndex = 4;
            this.InFtpPort.TextChanged += new System.EventHandler(this.InPort_TextChanged);
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
            // Configuration
            // 
            this.AcceptButton = this.ButtonAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 246);
            this.Controls.Add(this.GroupFtp);
            this.Controls.Add(this.ButtonAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Configuration";
            this.Text = "Setup";
            this.GroupFtp.ResumeLayout(false);
            this.GroupFtp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EnableUnixListing)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ButtonAccept;
        private System.Windows.Forms.TextBox InFtpHostname;
        private System.Windows.Forms.GroupBox GroupFtp;
        private System.Windows.Forms.TextBox InFtpPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox InFtpUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox InFtpPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox CheckFtpSsl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar EnableUnixListing;
    }
}