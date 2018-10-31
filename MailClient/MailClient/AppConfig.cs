using System;
using System.Windows.Forms;

namespace MailClient
{
    public partial class Configuration : Form
    {
        private PopConnectionSettings InPopSrettings, TempPopSettings;
        private SmtpConnectionSettings InSmtpSettings, TempSmtpSettings;

        public Configuration(PopConnectionSettings InPop, SmtpConnectionSettings InSmtp)
        {
            InitializeComponent();

            this.InPopSrettings = InPop;
            this.InSmtpSettings = InSmtp;

            TempPopSettings = new PopConnectionSettings();
            TempSmtpSettings = new SmtpConnectionSettings();
            TempPopSettings.CloneFrom(InPop);
            TempSmtpSettings.CloneFrom(InSmtp);

            InPopHostname.Text  = TempPopSettings.Hostname;
            InPopPort    .Text  = TempPopSettings.Port.ToString();
            InPopUsername.Text  = TempPopSettings.UserLogin;
            InPopPassword.Text  = TempPopSettings.UserPassword;
            InPopRefrate .Text  = TempPopSettings.RefreshRateSeconds.ToString();
            CheckPopSsl.Checked = TempPopSettings.UseSsl;

            InSmtpHostname.Text  = TempSmtpSettings.Hostname;
            InSmtpPort    .Text  = TempSmtpSettings.Port.ToString();
            InSmtpLogin   .Text  = TempSmtpSettings.UserLogin;
            InSmtpPassword.Text  = TempSmtpSettings.UserPassword;
            CheckSmtpSsl.Checked = TempSmtpSettings.UseSsl;
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
            double NewRefrate = 0;
            double.TryParse(InPopRefrate.Text, out NewRefrate);
            TempPopSettings.RefreshRateSeconds = NewRefrate;
            InPopRefrate.Text = TempPopSettings.RefreshRateSeconds.ToString();
        }

        private void InSmtpHostname_TextChanged(object sender, EventArgs e)
        {
            TempSmtpSettings.Hostname = InSmtpHostname.Text;
        }

        private void InSmtpPort_TextChanged(object sender, EventArgs e)
        {
            if (!Int16.TryParse(InSmtpPort.Text, out TempSmtpSettings.Port))
                InSmtpPort.Text = TempSmtpSettings.GetDefaultPort().ToString();
        }

        private void InSmtpLogin_TextChanged(object sender, EventArgs e)
        {
            TempSmtpSettings.UserLogin = InSmtpLogin.Text;
        }

        private void InSmtpPassword_TextChanged(object sender, EventArgs e)
        {
            TempSmtpSettings.UserPassword = InSmtpPassword.Text;
        }

        private void CheckSmtpSsl_CheckedChanged(object sender, EventArgs e)
        {
            TempSmtpSettings.UseSsl = CheckSmtpSsl.Checked;
        }

        private void ChecPopSsl_CheckedChanged(object sender, EventArgs e)
        {
            TempPopSettings.UseSsl = CheckPopSsl.Checked;
        }

        private void ButtonAccept_Click(object sender, EventArgs e)
        {
            InPopSrettings.CloneFrom(TempPopSettings);
            InSmtpSettings.CloneFrom(TempSmtpSettings);
            Close();
        }

    }
}
