using System;
using System.Windows.Forms;

namespace FtpClient
{
    public partial class Configuration : Form
    {
        private FtpConnectionSettings InFtpSrettings, TempFtpSettings;
        public Configuration(FtpConnectionSettings InFtp)
        {
            InitializeComponent();

            this.InFtpSrettings = InFtp;

            TempFtpSettings = new FtpConnectionSettings();
            TempFtpSettings.CloneFrom(InFtp);

            InFtpHostname.Text  = TempFtpSettings.Hostname;
            InFtpPort    .Text  = TempFtpSettings.Port.ToString();
            InFtpUsername.Text  = TempFtpSettings.UserLogin;
            InFtpPassword.Text  = TempFtpSettings.UserPassword;
            CheckFtpSsl.Checked = TempFtpSettings.UseSsl;
        }

        private void InHostname_TextChanged(object sender, EventArgs e)
        {
            TempFtpSettings.Hostname = InFtpHostname.Text;
        }

        private void InPort_TextChanged(object sender, EventArgs e)
        {
            if (!UInt16.TryParse(InFtpPort.Text, out TempFtpSettings.Port))
                InFtpPort.Text = TempFtpSettings.GetDefaultPort().ToString();
        }

        private void InUsername_TextChanged(object sender, EventArgs e)
        {
            TempFtpSettings.UserLogin = InFtpUsername.Text;
        }

        private void InPassword_TextChanged(object sender, EventArgs e)
        {
            TempFtpSettings.UserPassword = InFtpPassword.Text;
        }

        private void InRefrate_TextChanged(object sender, EventArgs e)
        {
        }

        private void InSmtpHostname_TextChanged(object sender, EventArgs e)
        {
        }

        private void InSmtpPort_TextChanged(object sender, EventArgs e)
        {
        }

        private void InSmtpLogin_TextChanged(object sender, EventArgs e)
        {
        }

        private void InSmtpPassword_TextChanged(object sender, EventArgs e)
        {
        }

        private void CheckSmtpSsl_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void ChecFtpSsl_CheckedChanged(object sender, EventArgs e)
        {
            TempFtpSettings.UseSsl = CheckFtpSsl.Checked;
        }

        private void ButtonAccept_Click(object sender, EventArgs e)
        {
            InFtpSrettings.CloneFrom(TempFtpSettings);
            Close();
        }

    }
}
