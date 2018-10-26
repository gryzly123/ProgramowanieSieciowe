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
            this.InHostname = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.InRefrate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.InPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.InUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.InPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CheckSsl = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
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
            this.ButtonAccept.Location = new System.Drawing.Point(148, 208);
            this.ButtonAccept.Name = "ButtonAccept";
            this.ButtonAccept.Size = new System.Drawing.Size(75, 23);
            this.ButtonAccept.TabIndex = 1;
            this.ButtonAccept.Text = "OK";
            this.ButtonAccept.UseVisualStyleBackColor = true;
            this.ButtonAccept.Click += new System.EventHandler(this.ButtonAccept_Click);
            // 
            // InHostname
            // 
            this.InHostname.Location = new System.Drawing.Point(94, 23);
            this.InHostname.Name = "InHostname";
            this.InHostname.Size = new System.Drawing.Size(100, 20);
            this.InHostname.TabIndex = 2;
            this.InHostname.TextChanged += new System.EventHandler(this.InHostname_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CheckSsl);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.InRefrate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.InPassword);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.InUsername);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.InPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.InHostname);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(211, 190);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "POP3";
            // 
            // InRefrate
            // 
            this.InRefrate.Location = new System.Drawing.Point(94, 127);
            this.InRefrate.Name = "InRefrate";
            this.InRefrate.Size = new System.Drawing.Size(100, 20);
            this.InRefrate.TabIndex = 10;
            this.InRefrate.TextChanged += new System.EventHandler(this.InRefrate_TextChanged);
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
            // InPassword
            // 
            this.InPassword.Location = new System.Drawing.Point(94, 101);
            this.InPassword.Name = "InPassword";
            this.InPassword.Size = new System.Drawing.Size(100, 20);
            this.InPassword.TabIndex = 8;
            this.InPassword.TextChanged += new System.EventHandler(this.InPassword_TextChanged);
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
            // InUsername
            // 
            this.InUsername.Location = new System.Drawing.Point(94, 75);
            this.InUsername.Name = "InUsername";
            this.InUsername.Size = new System.Drawing.Size(100, 20);
            this.InUsername.TabIndex = 6;
            this.InUsername.TextChanged += new System.EventHandler(this.InUsername_TextChanged);
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
            // InPort
            // 
            this.InPort.Location = new System.Drawing.Point(94, 49);
            this.InPort.Name = "InPort";
            this.InPort.Size = new System.Drawing.Size(100, 20);
            this.InPort.TabIndex = 4;
            this.InPort.TextChanged += new System.EventHandler(this.InPort_TextChanged);
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Use SSL";
            // 
            // CheckSsl
            // 
            this.CheckSsl.AutoSize = true;
            this.CheckSsl.Location = new System.Drawing.Point(94, 155);
            this.CheckSsl.Name = "CheckSsl";
            this.CheckSsl.Size = new System.Drawing.Size(15, 14);
            this.CheckSsl.TabIndex = 12;
            this.CheckSsl.UseVisualStyleBackColor = true;
            this.CheckSsl.CheckedChanged += new System.EventHandler(this.CheckSsl_CheckedChanged);
            // 
            // Configuration
            // 
            this.AcceptButton = this.ButtonAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 241);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ButtonAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Configuration";
            this.Text = "Setup";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ButtonAccept;
        private System.Windows.Forms.TextBox InHostname;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox InRefrate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox InPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox InUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox InPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox CheckSsl;
        private System.Windows.Forms.Label label6;
    }
}