using System;
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
            Service.OnLineSentOrReceived += ParseDebugMsg;
            Service.RequestStartService();
        }

        private void ParseDebugMsg(bool IsIncoming, string Message)
        {
            listBox1.Invoke(new Action(() =>
            {
                listBox1.Items.Add((IsIncoming ? "Server" : "Client") + ": " + Message);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }));
        }

        private void OnPopConnectionEstablished(object sender, EventArgs e)
        {
            button1.Enabled = false;
        }

        private bool xD = false;
        private void button2_Click(object sender, EventArgs e)
        {
            if(xD) Service.PushNewCommand(new PcListMessages());
            else Service.PushNewCommand(new PcAuthorize());

            xD = true;
        }
    }
}
