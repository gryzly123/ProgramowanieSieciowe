using System;
using System.Windows.Forms;

namespace FtpClient
{
    public partial class Configuration : Form
    {
        private FtpConnectionSettings InPopSrettings, TempPopSettings;
        public Configuration(FtpConnectionSettings InPop, FtpConnectionSettings InSmtp)
        {
            InitializeComponent();

            this.InPopSrettings = InPop;

            TempPopSettings = new FtpConnectionSettings();
            TempPopSettings.CloneFrom(InPop);

            InPopHostname.Text  = TempPopSettings.Hostname;
            InPopPort    .Text  = TempPopSettings.Port.ToString();
            InPopUsername.Text  = TempPopSettings.UserLogin;
            InPopPassword.Text  = TempPopSettings.UserPassword;
            CheckPopSsl.Checked = TempPopSettings.UseSsl;
        }

        private void InHostname_TextChanged(object sender, EventArgs e)
        {
            TempPopSettings.Hostname = InPopHostname.Text;
        }

        private void InPort_TextChanged(object sender, EventArgs e)
        {
            if (!Int16.TryParse(InPopPort.Text, out TempPopSettings.Port))
                InPopPort.Text = TempPopSettings.GetDefaultPort().ToString();
        }

        private void InUsername_TextChanged(object sender, EventArgs e)
        {
            TempPopSettings.UserLogin = InPopUsername.Text;
        }

        private void InPassword_TextChanged(object sender, EventArgs e)
        {
            TempPopSettings.UserPassword = InPopPassword.Text;
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

        private void ChecPopSsl_CheckedChanged(object sender, EventArgs e)
        {
            TempPopSettings.UseSsl = CheckPopSsl.Checked;
        }

        private void ButtonAccept_Click(object sender, EventArgs e)
        {
            InPopSrettings.CloneFrom(TempPopSettings);
            Close();
        }

    }
}
