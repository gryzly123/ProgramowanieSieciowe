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
    public partial class MainWindow : Form
    {
        private PopService Service;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PopConnectionSettings Config = new PopConnectionSettings();

            //Config.Hostname = "pop3.wp.pl";
            //Config.Port = 995;
            //Config.UserLogin = "ps45575@wp.pl";

            Config.Hostname = "127.0.0.1";
            Config.Port = 110;
            Config.UserLogin = "newuser";
            Config.UserPassword = "bepis";
            Config.RefreshRateSeconds = 2.0f;

            Service = new PopService();
            Service.PushNewConfig(Config);
            Service.OnConnectionOpened += OnPopConnectionEstablished;
            Service.RequestStartService();
        }

        private void ParseDebugMsg(bool IsIncoming, string Message)
        {
            listBox1.Items.Insert(0, (IsIncoming ? "Server" : "Client") + ": Message");
        }

        private void OnPopConnectionEstablished(object sender, EventArgs e)
        {
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Service.PushNewCommand(new PcAuthorize(Service.GetConfig()));
        }
    }
}
