using System;
using System.Windows.Forms;

namespace MailClient
{
    public partial class SmtpWindow : Form
    {
        private SmtpService Service;
        private bool IsSmtpRunning = false;
        SmtpConnectionSettings SmtpConfig;

        public SmtpWindow()
        {
            InitializeComponent();
        }

        public void SetupConfig(SmtpConnectionSettings InConfig)
        {
            SmtpConfig = new SmtpConnectionSettings();
            SmtpConfig.CloneFrom(InConfig);
        }
        private MailMessage GetMailMessage()
        {
            MailMessage Result = new MailMessage();
            Result.Recipients = TextBoxRecipients.Text.Split(new char[] {',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Result.Subject = TextBoxSubject.Text;
            Result.Message = TextBoxMessage.Text.Replace(PopCommand.MultilineTerminator, string.Empty);
            Result.IsOutMessage = true;
            return Result;
        }

        #region SMTP connection
        private void ButtonSendMessage_Click(object sender, EventArgs e)
        {
            StartConnection();
        }
        private bool StartConnection()
        {
            if (!IsSmtpRunning)
            {
                ButtonSendMessage.Enabled = false;
                Service = new SmtpService();
                Service.PushNewConfig(SmtpConfig);
                Service.OnConnectionOpened += OnSmtpConnectionEstablished;
                Service.OnConnectionClosed += OnSmtpConnectionClosed;
                Service.OnLineSentOrReceived += ParseDebugMessage;
                try { Service.RequestStartService(); return true; }
                catch (Exception E)
                {
                    ButtonSendMessage.Enabled = true;
                    MessageBox.Show("Could not connect to the server. Reason: " + E.ToString());
                }
            }
            else throw new Exception("connection_exists");
            return false;
        }
        private void ForceServiceShutdown(string Reason)
        {
            Invoke(new Action(() =>
            {
                Service.RequestStopService();
                Service = null;
                if (Reason.Length > 0)
                {
                    ListboxLog.Items.Add("Issue: " + Reason);
                    ListboxLog.SelectedIndex = ListboxLog.Items.Count - 1;
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

        #region SMTP event handling - begin & end connection
        private void OnSmtpConnectionEstablished()
        {
            IsSmtpRunning = true;
            ButtonSendMessage.Invoke(new Action(() =>
            {
                ListboxLog.Items.Add("-- connection started --");
                ListboxLog.SelectedIndex = ListboxLog.Items.Count - 1;
            }));

            //push auth command here
            ScHello HelloCmd = new ScHello();
            HelloCmd.OnHandshakeReceived += OnServerHandshakeReceived;
            Service.PushNewCommand(HelloCmd);

        }
        private void OnSmtpConnectionClosed(bool CleanShutdown)
        {
            ButtonSendMessage.Invoke(new Action(() =>
            {
                IsSmtpRunning = false;
                ButtonSendMessage.Enabled = true;

                ListboxLog.Items.Add(CleanShutdown
                    ? "-- connection ended --"
                    : "-- connection failed --");
                ListboxLog.SelectedIndex = ListboxLog.Items.Count - 1;
            }));
        }
        #endregion

        #region SMTP event handling - dialog
        //on initial handshake
        private void OnServerHandshakeReceived(bool Success)
        {
            if (Success)
            {
                ScAuthorize AuthCmd = new ScAuthorize();
                AuthCmd.OnUserLogin += OnServerAuthorizationComplete;
                Service.PushNewCommand(AuthCmd);
            }
            else
                ForceServiceShutdown("Incorrect handshake");
        }
        //on logged in
        private void OnServerAuthorizationComplete(bool Success)
        {
            if(Success)
            {
                ScSendMessage SendCmd = new ScSendMessage(GetMailMessage());
                SendCmd.OnMessageSent += OnServerTransactionComplete;
                Service.PushNewCommand(SendCmd);
            }
            else
                ForceServiceShutdown("Incorrect username or password");
        }
        //on message sent
        private void OnServerTransactionComplete(bool Success)
        {
            Invoke(new Action(() =>
            {
                if (Success)
                    MessageBox.Show(Success ? "Message sent successfully!" : "Send operation failed");
                    ScQuit QuitCmd = new ScQuit();
                    QuitCmd.OnServerQuit += OnCleanShutdown;
                    Service.PushNewCommand(QuitCmd);
            }));
        }
        //on goodbye handshake
        private void OnCleanShutdown()
        {
            Service.RequestStopService();
            Service = null;
        }
        #endregion
    }
}
