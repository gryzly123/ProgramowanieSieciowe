using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient
{
    public class SmtpService
    {
        private SmtpConnectionSettings CurrentConfig = new SmtpConnectionSettings();
        private NetConnection Connection = new NetConnection();
        private ConcurrentQueue<SmtpCommand> CommandQueue = new ConcurrentQueue<SmtpCommand>();
        private BackgroundWorker ServerThread;
        private bool ShutdownRequested = false;

        public NetEvent OnConnectionOpened;
        public NetEventRetVal OnConnectionClosed;
        public DebugMessaging OnLineSentOrReceived;

        public void OnLineSentPassthrough(bool In, string Msg) { OnLineSentOrReceived(In, Msg); }

        public void RequestStartService()
        {
            if (CurrentConfig == null) throw new Exception("no_config_provided");
            if (Connection.IsConnected()) throw new Exception("connection_exists");

            //może rzucić wyjątkiem, ale obecna metoda też powinna być używana z try-catchem
            Connection.OnLineSentOrReceived += OnLineSentPassthrough;

            ServerThread = new BackgroundWorker();
            ServerThread.RunWorkerCompleted += ServerThread_RunWorkerCompleted;
            ServerThread.DoWork += ServerLoop;
            ServerThread.RunWorkerAsync();
        }

        private void ServerThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                System.Windows.Forms.MessageBox.Show("Connection thread failed. Exception: " + e.Error.ToString());
                OnConnectionClosed(false);
            }
        }

        public void PushNewConfig(SmtpConnectionSettings NewConfig)
        {
            if (Connection.IsConnected() || ServerThread != null) throw new Exception("cannot_change_config_at_service_runtime");
            CurrentConfig = NewConfig;
        }

        public void PushNewCommand(SmtpCommand NewCommand)
        {
            NewCommand.SetSmtpService(this);
            CommandQueue.Enqueue(NewCommand);
        }

        public SmtpConnectionSettings GetConfig()
        {
            return CurrentConfig;
        }

        public DebugMessaging ExposeDebugDelegate() { return Connection.OnLineSentOrReceived; }

        public void ClearCommandQueue()
        {
            CommandQueue = new ConcurrentQueue<SmtpCommand>();
        }

        private void ServerLoop(object sender, DoWorkEventArgs e)
        {
            if (!Connection.StartConnection(CurrentConfig))
            {
                OnConnectionClosed(false);
                return;
            }

            OnConnectionOpened();

            while (!ShutdownRequested)
            {
                if (CommandQueue.Count > 0)
                {
                    SmtpCommand Cmd = null;
                    bool Deq = CommandQueue.TryDequeue(out Cmd);
                    if (!Connection.ExecuteCommand(Cmd))
                        OnLineSentOrReceived(false, "SmtpCommand error for " + Cmd.ToString());
                    System.Threading.Thread.Sleep(100);
                }
                else System.Threading.Thread.Sleep(100);
            }

            ShutdownRequested = false;
            Connection.CloseConnection();
            Connection = null;
            OnConnectionClosed(true);
        }

        internal void RequestStopService()
        {
            ShutdownRequested = true;
        }
    }
}
