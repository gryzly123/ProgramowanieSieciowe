using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MailClient
{
    public partial class MainWindow : Form
    {
        private PopService Service;
        private const string ConfigFilename = "config.xml";
        private MailDirectory Inbox = new MailDirectory("Inbox");
        private bool IsPopRunning = false;
        PopConnectionSettings PopConfig;

        public MainWindow()
        {
            InitializeComponent();
            SetupConfig();
        }

        private void SetupConfig()
        {
            PopConfig = new PopConnectionSettings();
            if (!PopConfig.TryReadFromFile(ConfigFilename))
                MessageBox.Show("Config file could not be parsed (either missing or corrupted).\nPlease fill your information in the config menu.");
        }

        private void ButtonConnectPop_Click(object sender, EventArgs e)
        {
            PopConnectionSettings Config = new PopConnectionSettings();

            Service = new PopService();
            Service.PushNewConfig(Config);
            Service.OnConnectionOpened += OnPopConnectionEstablished;
            Service.OnLineSentOrReceived += ParseDebugMessage;
            Inbox.OnMessageReceived += AddMessageToInbox;

            //startuję połączenie, a następnie każę klientowi zalogować się i pobrać wiadomości
            try
            {
                Service.RequestStartService();
                Service.PushNewCommand(new PcAuthorize());

                PcListMessages ReceiveCmd = new PcListMessages(Inbox);
                ReceiveCmd.OnNewMessagesReceived += HandleOnMessageListReceived;
                Service.PushNewCommand(ReceiveCmd);
                ButtonConnectPop.Enabled = false;
            }
            catch (Exception E)
            {
                MessageBox.Show("Could not connect to the server. Reason: " + E.ToString());
            }
        }

        private void AddMessageToInbox(MailDirectory AtDirectory, MailMessage AtMessage)
        {
            ListboxMessages.Invoke(new Action(() =>
            {
            if (AtDirectory == Inbox)
                ListboxMessages.Items.Insert(0, AtMessage.PopUid);
            }));
        }

        private void ParseDebugMessage(bool IsIncoming, string Message)
        {
            ListboxLog.Invoke(new Action(() =>
            {
                ListboxLog.Items.Add((IsIncoming ? "Server" : "Client") + ": " + Message);
                ListboxLog.SelectedIndex = ListboxLog.Items.Count - 1;
            }));
        }

        private void OnPopConnectionEstablished(object sender, EventArgs e)
        {
            IsPopRunning = true;
            ButtonConnectPop.Enabled = true;
            ButtonConnectPop.Text = "Stop connection";
        }

        private void HandleOnMessageListReceived(Dictionary<int, string> NewMessages)
        {
            foreach (KeyValuePair<int, string> Message in NewMessages)
            {
                //MessageBox.Show(Message.Key.ToString() + " -- " + Message.Value);
                Service.PushNewCommand(new PcFetchMessage(Inbox, Message.Key, Message.Value));
            }
        }

        private void ListboxMessages_SelectedMessage(object sender, EventArgs e)
        {
            string Uid = ListboxMessages.Items[ListboxMessages.SelectedIndex].ToString();
            MailMessage Message = Inbox.GetMessage(Uid);
            if (Message == null)
            {
                MessageBox.Show("internal error while reading message from memory");
                return;
            }

            MessageBox.Show(Message.Message);
        }

        private void ButtonConfig_Click(object sender, EventArgs e)
        {
            new Configuration(PopConfig).ShowDialog();
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(Service != null) Service.RequestStopService();

            if(!PopConfig.SaveConfig(ConfigFilename))
                MessageBox.Show("Could not save your config (most likely due\nto permission issues).");
        }
    }
}
