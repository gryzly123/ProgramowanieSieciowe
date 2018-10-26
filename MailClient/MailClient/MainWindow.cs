﻿using System;
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

        #region Form events
        private void ButtonConnectPop_Click(object sender, EventArgs e)
        {
            if (TimerPopRefresh.Enabled)
            {
                if (IsPopRunning) StopConnection();
                TimerPopRefresh.Stop();
                ButtonConfig.Enabled = true;
            }
            else
            {
                ButtonConfig.Enabled = false;
                StartConnection();
                TimerPopRefresh.Interval = (int)(Service.GetConfig().RefreshRateSeconds * 1000);
                TimerPopRefresh.Start();
            }
        }
        private void ButtonConfig_Click(object sender, EventArgs e)
        {
            new Configuration(PopConfig).ShowDialog();
            PopConfig.SaveConfig(ConfigFilename);
        }
        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(Service != null) Service.RequestStopService();

            if(!PopConfig.SaveConfig(ConfigFilename))
                MessageBox.Show("Could not save your config (most likely due\nto permission issues).");
        }
        private void TimerPopRefresh_Tick(object sender, EventArgs e)
        {
            //pomijam tego ticka jeśli połączenie nie zakończyło się
            if (IsPopRunning) return;
            StartConnection();
        }
        #endregion

        #region POP3 connection
        private void StartConnection()
        {
            if (!IsPopRunning)
            {
                Service = new PopService();
                Service.PushNewConfig(PopConfig);
                Service.OnConnectionOpened += OnPopConnectionEstablished;
                Service.OnConnectionClosed += OnPopConnectionClosed;
                Service.OnLineSentOrReceived += ParseDebugMessage;
                Inbox.OnMessageReceived += AddMessageToInbox;
                ButtonConnectPop.Enabled = false;
                try { Service.RequestStartService(); }
                catch (Exception E)
                {
                    ButtonConnectPop.Enabled = true;
                    MessageBox.Show("Could not connect to the server. Reason: " + E.ToString());
                }
            }
            else throw new Exception("connection_exists");
        }
        private void StopConnection()
        {
            if (IsPopRunning && Service != null)
            {
                ButtonConnectPop.Enabled = false;
                Service.RequestStopService();
            }
            else throw new Exception("no_working_connections");
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
            }));

            PcAuthorize AuthCmd = new PcAuthorize();
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
            PcListMessages Cmd = new PcListMessages(Inbox);
            Cmd.OnNewMessagesReceived += HandleOnMessageListReceived;
            Service.PushNewCommand(Cmd);
        }
        private void OnPopUserFailedToAuth()
        {
            MessageBox.Show("Connection succeeded but login failed.\nCheck your credentials.");
            Service.RequestStopService();
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
                Service.PushNewCommand(new PcFetchMessage(Inbox, Message.Key, Message.Value));
            }
            Service.PushNewCommand(new PcQuit());
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
