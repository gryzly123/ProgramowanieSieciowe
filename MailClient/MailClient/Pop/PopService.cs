using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace MailClient
{
    public enum PopState
    {
        Off,
        Authorization,
        Transaction,
        Update
    }

    public delegate void PopEvent();
    public delegate void PopEventRetVal(bool Success);

    public class PopService
    {
        private PopConnectionSettings CurrentConfig = new PopConnectionSettings();
        private PopConnection Connection = new PopConnection();
        private ConcurrentQueue<PopCommand> CommandQueue = new ConcurrentQueue<PopCommand>();
        private BackgroundWorker ServerThread;
        private bool ShutdownRequested = false;
        internal PopState State = PopState.Off;

        public PopEvent OnConnectionOpened;
        public PopEventRetVal OnConnectionClosed;

        public DebugMessaging OnLineSentOrReceived;
        public void OnLineSentPassthrough(bool In, string Msg) { OnLineSentOrReceived(In, Msg); }


        public void RequestStartService()
        {
            if (CurrentConfig == null) throw new Exception("no_config_provided");
            if(Connection.IsConnected()) throw new Exception("connection_exists");

            //może rzucić wyjątkiem, ale obecna metoda też powinna być używana z try-catchem
            Connection.OnLineSentOrReceived += OnLineSentPassthrough;

            ServerThread = new BackgroundWorker();
            ServerThread.RunWorkerCompleted += ServerThread_RunWorkerCompleted;
            ServerThread.DoWork += ServerLoop;
            ServerThread.RunWorkerAsync();
        }

        private void ServerThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Error != null)
            {
                System.Windows.Forms.MessageBox.Show("Connection thread failed. Exception: " + e.Error.ToString());
                OnConnectionClosed(false);
            }
        }

        public void RequestStopService()
        {
            ShutdownRequested = true;
        }

        public void PushNewConfig(PopConnectionSettings NewConfig)
        {
            if (Connection.IsConnected() || ServerThread != null) throw new Exception("cannot_change_config_at_service_runtime");
            CurrentConfig = NewConfig;
        }

        public void PushNewCommand(PopCommand NewCommand)
        {
            NewCommand.SetPopService(this);
            CommandQueue.Enqueue(NewCommand);
        }

        public PopConnectionSettings GetConfig()
        {
            return CurrentConfig;
        }

        public DebugMessaging ExposeDebugDelegate() { return Connection.OnLineSentOrReceived; }

        public void ClearCommandQueue()
        {
            CommandQueue = new ConcurrentQueue<PopCommand>();
        }

        private void ServerLoop(object sender, DoWorkEventArgs e)
        {
            if(!Connection.StartConnection(CurrentConfig))
            {
                OnConnectionClosed(false);
                return;
            }

            State = PopState.Authorization;
            OnConnectionOpened();

            while (!ShutdownRequested)
            {
                if (CommandQueue.Count > 0)
                {
                    bool Deq = CommandQueue.TryDequeue(out PopCommand Cmd);
                    if(!Connection.ExecuteCommand(Cmd))
                        OnLineSentOrReceived(false, "PopCommand error for " + Cmd.ToString());
                    System.Threading.Thread.Sleep(100);
                }
                else System.Threading.Thread.Sleep(100);
            }

            ShutdownRequested = false;
            State = PopState.Off;
            Connection.CloseConnection();
            Connection = null;
            OnConnectionClosed(true);
        }
    }
}
