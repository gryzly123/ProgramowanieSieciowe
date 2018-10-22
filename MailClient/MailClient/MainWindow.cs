using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MailClient
{
    public partial class MainWindow : Form
    {
        private PopService Service;
        private MailDirectory Directory = new MailDirectory("Inbox");

        public MainWindow()
        {
            InitializeComponent();
            PopConnectionSettings Settings = new PopConnectionSettings("c:\\cfg.xml");
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

            Directory.OnMessageReceived += HandleChageInInbox;

            Service.PushNewCommand(new PcAuthorize());
            PcListMessages ReceiveCmd = new PcListMessages(Directory);
            ReceiveCmd.OnNewMessagesReceived += HandleMsgs;
            Service.PushNewCommand(ReceiveCmd);

        }

        private void HandleChageInInbox(MailDirectory AtDirectory, MailMessage AtMessage)
        {
            listBox2.Invoke(new Action(() =>
            {
            if (AtDirectory == Directory)
                listBox2.Items.Insert(0, AtMessage.PopUid);
            }));
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

        private void HandleMsgs(Dictionary<int, string> NewMessages)
        {
            foreach (KeyValuePair<int, string> Message in NewMessages)
            {
                //MessageBox.Show(Message.Key.ToString() + " -- " + Message.Value);
                Service.PushNewCommand(new PcFetchMessage(Directory, Message.Key, Message.Value));
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Uid = listBox2.Items[listBox2.SelectedIndex].ToString();
            MailMessage Message = Directory.GetMessage(Uid);
            if(Message == null)
            {
                MessageBox.Show("internal error while reading message from memory");
                return;
            }

            MessageBox.Show(Message.Message);
        }
    }
}
