using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FtpClient
{
    public partial class MainWindow : Form
    {
        private FtpService Service;
        private FtpDirectory CurrentDir;
        private const string ConfigFilenameFtp = "FtpConfig.xml";
        FtpConnectionSettings FtpConfig;

        public MainWindow()
        {
            InitializeComponent();
            SetupConfig();
        }
        private void SetupConfig()
        {
            FtpConfig = new FtpConnectionSettings();
            if (!FtpConfig.TryReadFromFile(ConfigFilenameFtp))
                MessageBox.Show("FTP config file could not be parsed (either missing or corrupted).\nPlease fill your information in the config menu.");
        }

        #region Form events
        private void ButtonConnectFtp_Click(object sender, EventArgs e)
        {
            if (Service != null)
                DisableService();
            else
                EnableService();
        }

        private void EnableService()
        {
            ButtonConfig.Enabled = false;
            StartConnection();
        }

        private void DisableService()
        {
            Invoke(new Action(() =>
            {
                Service.RequestStopService();
                ButtonConnectFtp.Text = "Start client";
                ButtonConfig.Enabled = true;
            }));
        }

        private void ButtonConfig_Click(object sender, EventArgs e)
        {
            new Configuration(FtpConfig).ShowDialog();
            FtpConfig.SaveConfig(ConfigFilenameFtp);
        }
        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(Service != null) Service.RequestStopService();

            if(!FtpConfig.SaveConfig(ConfigFilenameFtp))
                MessageBox.Show("Could not save your FTP config (most likely due\nto permission issues).");
        }
        #endregion

        #region FTP connection
        private bool StartConnection()
        {
            if (Service == null)
            {
                Service = new FtpService();
                Service.PushNewConfig(FtpConfig);
                Service.OnConnectionOpened += OnFtpConnectionEstablished;
                Service.OnConnectionClosed += OnFtpConnectionClosed;
                Service.OnLineSentOrReceived += ParseDebugMessage;
                ButtonConnectFtp.Enabled = false;
                try { Service.RequestStartService(); return true; }
                catch (Exception E)
                {
                    ButtonConnectFtp.Enabled = true;
                    ButtonConnectFtp.Enabled = true;
                    MessageBox.Show("Could not connect to the server. Reason: " + E.ToString());
                }
            }
            else throw new Exception("connection_exists");
            return false;
        }
        #endregion

        #region FTP event handling - begin & end connection
        private void OnFtpConnectionEstablished()
        {
            ButtonConnectFtp.Invoke(new Action(() =>
            {
                ButtonConnectFtp.Enabled = true;
                ButtonConnectFtp.Text = "Stop client";
                ListboxLog.Items.Add("-- connection started --");
                ListboxLog.SelectedIndex = ListboxLog.Items.Count - 1;
            }));

            FcHandshake HsCmd = new FcHandshake();
            HsCmd.OnHandshakeReceived += OnHandshakeReceived;
            Service.PushNewCommand(HsCmd);
        }

        private void OnHandshakeReceived()
        {
            FcAuthorize AuthCmd = new FcAuthorize();
            AuthCmd.OnUserLogin += OnFtpUserLoggedIn;
            Service.PushNewCommand(AuthCmd);
        }

        private void OnFtpConnectionClosed(bool CleanShutdown)
        {
            ButtonConnectFtp.Invoke(new Action(() =>
            {
                if (!CleanShutdown)
                {
                    ButtonConnectFtp.Enabled = true;
                    ButtonConfig.Enabled = true;
                }

                ButtonConnectFtp.Text = CleanShutdown
                ? "Waiting..."
                : "Start client";

                ListboxLog.Items.Add(CleanShutdown
                    ? "-- connection ended --"
                    : "-- connection failed --");
                ListboxLog.SelectedIndex = ListboxLog.Items.Count - 1;
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
       
        #region FTP event handling - authorize
        private void OnFtpUserLoggedIn(bool Success)
        {
            FcModeToggle PasvCmd = new FcModeToggle();
            Service.PushNewCommand(PasvCmd);

            CurrentDir = new FtpDirectory();
            FcChangeDirectory Cmd = new FcChangeDirectory(CurrentDir, CurrentDir);
            Cmd.OnDirectoryChanged += OnFtpDirectoryChanged;
            Service.PushNewCommand(Cmd);
        }

        private void OnFtpDirectoryChanged(bool Success, FtpDirectory Dir)
        {
            if (!Success)
            {
                Invoke(new Action(() =>
                {
                    MessageBox.Show("Could not change to a new directory, keeping current");
                }));
                return;
            }

            CurrentDir = Dir;
            FcListDirectory Cmd = new FcListDirectory(Dir);
            Cmd.OnDirectoryListed += OnFtpDirectoryListed;
            Service.PushNewCommand(Cmd);
        }

        private void OnFtpDirectoryListed(bool Success, FtpDirectory Dir)
        {
            if (!Success)
            {
                Invoke(new Action(() =>
                {
                    MessageBox.Show("Could not list directory");
                }));
                return;
            }

            Invoke(new Action(() =>
            {
                MessageBox.Show("Listing in form not implemented");
            }));
        }

        private void OnFtpUserFailedToAuth()
        {
            MessageBox.Show("Connection succeeded but login failed.\nCheck your credentials.");
            DisableService();
        }
        #endregion
    }
}
