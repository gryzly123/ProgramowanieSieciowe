using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FtpClient
{
    public partial class MainWindow : Form
    {
        private FtpService Service;
        private const string ConfigFilenamePop = "PopConfig.xml";
        private const string ConfigFilenameSmtp = "SmtpConfig.xml";
        private MailDirectory Inbox = new MailDirectory("Inbox");
        private bool IsPopRunning = false;
        private int NewMessageCounter = 0;
        private int FetchMessageCounter = 0;
        FtpConnectionSettings PopConfig;

        public MainWindow()
        {
            InitializeComponent();
            SetupConfig();
            Inbox.OnMessageReceived += AddMessageToInbox;
        }
        private void SetupConfig()
        {
            PopConfig = new FtpConnectionSettings();
            if (!PopConfig.TryReadFromFile(ConfigFilenamePop))
                MessageBox.Show("POP3 config file could not be parsed (either missing or corrupted).\nPlease fill your information in the config menu.");
        }

        #region Form events
        private void ButtonConnectPop_Click(object sender, EventArgs e)
        {
            if (TimerPopRefresh.Enabled)
                DisableService();
            else
                EnableService();
        }

        private void EnableService()
        {
            ButtonConfig.Enabled = false;
            NewMessageCounter = 0;
            if (StartConnection())
            {
                TimerPopRefresh.Interval = (int)(Service.GetConfig().RefreshRateSeconds * 1000);
                TimerPopRefresh.Start();
            }
        }

        private void DisableService()
        {
            Invoke(new Action(() =>
            {
                if (IsPopRunning) Service.RequestStopService();
                ButtonConnectPop.Text = "Start client";
                TimerPopRefresh.Stop();
                ButtonConfig.Enabled = true;
                ListboxLog.Items.Add("Inbox refresh disabled. Messages received since service started: " + NewMessageCounter.ToString());
                ListboxLog.SelectedIndex = ListboxLog.Items.Count - 1;
            }));
        }

        private void ButtonConfig_Click(object sender, EventArgs e)
        {
            new Configuration(PopConfig).ShowDialog();
            PopConfig.SaveConfig(ConfigFilenamePop);
        }
        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(Service != null) Service.RequestStopService();

            if(!PopConfig.SaveConfig(ConfigFilenamePop))
                MessageBox.Show("Could not save your FTP config (most likely due\nto permission issues).");
        }
        private void TimerPopRefresh_Tick(object sender, EventArgs e)
        {
            //pomijam tego ticka jeśli połączenie nie zakończyło się
            if (IsPopRunning) return;
            StartConnection();
        }

        private void ButtonSendMessage_Click(object sender, EventArgs e)
        {
        }
        #endregion

        #region POP3 connection
        private bool StartConnection()
        {
            if (!IsPopRunning)
            {
                Service = new FtpService();
                Service.PushNewConfig(PopConfig);
                Service.OnConnectionOpened += OnPopConnectionEstablished;
                Service.OnConnectionClosed += OnPopConnectionClosed;
                Service.OnLineSentOrReceived += ParseDebugMessage;
                ButtonConnectPop.Enabled = false;
                FetchMessageCounter = 0;
                try { Service.RequestStartService(); return true; }
                catch (Exception E)
                {
                    ButtonConnectPop.Enabled = true;
                    ButtonConnectPop.Enabled = true;
                    MessageBox.Show("Could not connect to the server. Reason: " + E.ToString());
                }
            }
            else throw new Exception("connection_exists");
            return false;
        }
        #endregion

        #region POP3 event handling - begin & end connection
        private void OnPopConnectionEstablished()
        {
            IsPopRunning = true;
            ButtonConnectPop.Invoke(new Action(() =>
            {
                ButtonConnectPop.Enabled = true;
                ButtonConnectPop.Text = "Stop client";
                ListboxLog.Items.Add("-- connection started --");
                ListboxLog.SelectedIndex = ListboxLog.Items.Count - 1;
            }));

            FcAuthorize AuthCmd = new FcAuthorize();
            AuthCmd.OnUserLoginSuccess += OnPopUserLoggedIn;
            AuthCmd.OnUserLoginFailed += OnPopUserFailedToAuth;
            Service.PushNewCommand(AuthCmd);
        }
        private void OnPopConnectionClosed(bool CleanShutdown)
        {
            ButtonConnectPop.Invoke(new Action(() =>
            {
                IsPopRunning = false;
                if (!CleanShutdown)
                {
                    ButtonConnectPop.Enabled = true;
                    TimerPopRefresh.Stop();
                    ButtonConfig.Enabled = true;
                }

                ButtonConnectPop.Text = CleanShutdown
                ? "Waiting..."
                : "Start client";

                ListboxLog.Items.Add(CleanShutdown
                    ? "-- connection ended --"
                    : "-- connection failed --");
                ListboxLog.Items.Add("Messages received in this ping: " + FetchMessageCounter.ToString());
                ListboxLog.SelectedIndex = ListboxLog.Items.Count - 1;
                if (FetchMessageCounter > 0)
                    MessageBox.Show("You have " + FetchMessageCounter.ToString() + " new messages!");
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
        #endregion
       
        #region POP3 event handling - authorize
        private void OnPopUserLoggedIn()
        {
            FcListDirectory Cmd = new FcListDirectory(Inbox);
            Cmd.OnNewMessagesReceived += HandleOnMessageListReceived;
            Service.PushNewCommand(Cmd);
        }
        private void OnPopUserFailedToAuth()
        {
            MessageBox.Show("Connection succeeded but login failed.\nCheck your credentials.");
            DisableService();
        }
        #endregion
        
        #region POP3 event handling - messages
        private void AddMessageToInbox(MailDirectory AtDirectory, MailMessage AtMessage)
        {
            ListboxMessages.Invoke(new Action(() =>
            {
            if (AtDirectory == Inbox)
                ListboxMessages.Items.Insert(0, AtMessage.PopUid);
            }));
        }
        private void HandleOnMessageListReceived(Dictionary<int, string> NewMessages)
        {
            foreach (KeyValuePair<int, string> Message in NewMessages)
            {
                Service.PushNewCommand(new FcChangeDirectory(Inbox, Message.Key, Message.Value));
                ++FetchMessageCounter;
                ++NewMessageCounter;
            }
            Service.PushNewCommand(new FcQuit());
        }
        private void ListboxMessages_SelectedMessage(object sender, EventArgs e)
        {
            if (ListboxMessages.SelectedIndex == -1) return;
            string Uid = ListboxMessages.Items[ListboxMessages.SelectedIndex].ToString();
            MailMessage Message = Inbox.GetMessage(Uid);
            if (Message == null)
            {
                MessageBox.Show("internal error while reading message from memory");
                return;
            }

            MessageBox.Show(Message.Message);
        }
        #endregion
    }
}
