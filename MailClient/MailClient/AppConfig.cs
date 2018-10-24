using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailClient
{
    public partial class Configuration : Form
    {
        private PopConnectionSettings InSettings, TempSettings;

        public Configuration(PopConnectionSettings InSettings)
        {
            InitializeComponent();

            this.InSettings = InSettings;
            TempSettings = new PopConnectionSettings();
            TempSettings.CloneFrom(InSettings);

            InHostname.Text = TempSettings.Hostname;
            InPort    .Text = TempSettings.Port.ToString();
            InUsername.Text = TempSettings.UserLogin;
            InPassword.Text = TempSettings.UserPassword;
            InRefrate .Text = TempSettings.RefreshRateSeconds.ToString();
        }

        private void InHostname_TextChanged(object sender, EventArgs e)
        {
            TempSettings.Hostname = InHostname.Text;
        }

        private void InPort_TextChanged(object sender, EventArgs e)
        {
            if (!Int16.TryParse(InPort.Text, out TempSettings.Port))
                InPort.Text = TempSettings.Port.ToString();
        }

        private void InUsername_TextChanged(object sender, EventArgs e)
        {
            TempSettings.UserLogin = InUsername.Text;
        }

        private void InPassword_TextChanged(object sender, EventArgs e)
        {
            TempSettings.UserPassword = InPassword.Text;
        }

        private void InRefrate_TextChanged(object sender, EventArgs e)
        {
            double NewRefrate = 0;
            double.TryParse(InRefrate.Text, out NewRefrate);
            TempSettings.RefreshRateSeconds = NewRefrate;
            InRefrate.Text = TempSettings.RefreshRateSeconds.ToString();
        }

        private void ButtonAccept_Click(object sender, EventArgs e)
        {
            InSettings.CloneFrom(TempSettings);
            Close();
        }

    }
}
