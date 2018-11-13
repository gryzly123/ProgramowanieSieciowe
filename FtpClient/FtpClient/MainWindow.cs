using System;
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
            MarkInterfaceBusy(true);
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
        private void ButtonConfig_Click(object sender, EventArgs e)
        {
            new Configuration(FtpConfig).ShowDialog();
            FtpConfig.SaveConfig(ConfigFilenameFtp);
        }
        private void ButtonGoUp_Click(object sender, EventArgs e)
        {
            FtpDirectory Parent = CurrentDir.GetParentDir();
            if (Parent != null) RequestChangeDirectory(Parent);
        }
        private void ButtonScanRecursive_Click(object sender, EventArgs e)
        {
            MarkInterfaceBusy(true);
            CommandsLeft = 0;

            ++CommandsLeft;
            Service.PushNewCommand(new FcRequestDynamicPort());
            RecursiveTree = new FtpDirectory();
            FcListDirectory Dir = new FcListDirectory(RecursiveTree);
            Dir.OnDirectoryListed += RecursiveOnDirectoryListed;
            Service.PushNewCommand(Dir);
        }
        private void ListboxDirContents_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Idx = ListboxDirContents.SelectedIndex;
            if (Idx < 0) return;

            string Target = (string)ListboxDirContents.Items[Idx];
            bool IsDir = Target.EndsWith("/");

            if(IsDir)
            {
                string[] AbsPath = Target.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                FtpDirectory NewDir = CurrentDir;
                for (int i = 0; i < AbsPath.Length; ++i)
                {
                    NewDir = NewDir.GetSubdirectory(AbsPath[i]);
                    if (NewDir == null)
                    {
                        MessageBox.Show("Internal error: selected directory doesn't exist in memory");
                        return;
                    }
                }
                RequestChangeDirectory(NewDir);
            }
        }
        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(Service != null) Service.RequestStopService();

            if(!FtpConfig.SaveConfig(ConfigFilenameFtp))
                MessageBox.Show("Could not save your FTP config (most likely due\nto permission issues).");
        }
        private void MarkInterfaceBusy(bool NewBusy)
        {
            ButtonGoUp.Enabled = !NewBusy;
            ButtonScanRecursive.Enabled = !NewBusy;
            ListboxDirContents.Enabled = !NewBusy;
        }
        #endregion

        #region FTP connection
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
                MarkInterfaceBusy(true);
            }));
        }
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
        private void OnFtpConnectionClosed(bool CleanShutdown)
        {
            ButtonConnectFtp.Invoke(new Action(() =>
            {
                if (!CleanShutdown)
                {
                    ButtonConnectFtp.Enabled = true;
                    ButtonConfig.Enabled = true;
                }

                ButtonConnectFtp.Text = "Start client";

                ListboxLog.Items.Add(CleanShutdown
                    ? "-- connection ended --"
                    : "-- connection failed --");
                ListboxLog.SelectedIndex = ListboxLog.Items.Count - 1;

                Service = null;
            }));
        }
        private void OnHandshakeReceived()
        {
            FcAuthorize AuthCmd = new FcAuthorize();
            AuthCmd.OnUserLogin += OnFtpUserLoggedIn;
            Service.PushNewCommand(AuthCmd);
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

        #region FTP event handling - authorization, directory browser
        private void OnFtpUserLoggedIn(bool Success)
        {
            if (Success)
            {
                CurrentDir = new FtpDirectory();
                RequestChangeDirectory(CurrentDir);
            }
            else
            {
                MessageBox.Show("Connection succeeded but login failed.\nCheck your credentials.");
                DisableService();
            }
        }
        private void RequestChangeDirectory(FtpDirectory NewDir)
        {
            Invoke(new Action(() => { MarkInterfaceBusy(true); }));
            FcChangeDirectory Cmd = new FcChangeDirectory(CurrentDir, NewDir);
            Cmd.OnDirectoryChanged += OnFtpDirectoryChanged;
            Service.PushNewCommand(Cmd);
        }
        private void OnFtpDirectoryChanged(bool Success, FtpDirectory Dir)
        {
            Invoke(new Action(() =>
            {
                if (!Success)
                    MessageBox.Show("Could not change to a new directory, keeping current");
                else
                {
                    LabelCurrentPath.Text = Dir.PathString();
                }
            }));

            CurrentDir = Dir;
            RequestListDirectory(CurrentDir);
        }
        private void RequestListDirectory(FtpDirectory NewDir)
        {
            FcRequestDynamicPort PasvCmd = new FcRequestDynamicPort();
            Service.PushNewCommand(PasvCmd);

            FcListDirectory Cmd = new FcListDirectory(NewDir);
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
                ListboxDirContents.Items.Clear();
                foreach (FtpDirectory Sub in Dir.Subdirectories)
                    ListboxDirContents.Items.Add(Sub.DirName + "/");

                foreach (FtpFile File in Dir.Files)
                    ListboxDirContents.Items.Add(File.FileName);

                MarkInterfaceBusy(false);
            }));
        }
        #endregion

        #region FTP event handling - recursive directory tree
        private int CommandsLeft = 0;
        private FtpDirectory RecursiveTree;
        private void RecursiveOnDirectoryListed(bool Success, FtpDirectory Directory)
        {
            --CommandsLeft;

            foreach (FtpDirectory Subdir in Directory.Subdirectories)
            {
                ++CommandsLeft;
                Service.PushNewCommand(new FcRequestDynamicPort());
                FcListDirectory Dir = new FcListDirectory(Subdir);
                Dir.OnDirectoryListed += RecursiveOnDirectoryListed;
                Service.PushNewCommand(Dir);
            }

            if(CommandsLeft == 0)
            {
                Invoke(new Action(() =>
                {
                    ListboxDirContents.Items.Clear();
                    CurrentDir = RecursiveTree;
                    RecursivePrintDirectory(RecursiveTree);
                }));
            }
        }
        private void RecursivePrintDirectory(FtpDirectory Directory)
        {
            foreach(FtpDirectory Subdir in Directory.Subdirectories)
                RecursivePrintDirectory(Subdir);
            ListboxDirContents.Items.Add(Directory.PathString());
            foreach(FtpFile File in Directory.Files)
                ListboxDirContents.Items.Add(File.GetDirectory().PathString() + File.FileName);

            MarkInterfaceBusy(false);
        }
        #endregion
    }
}
