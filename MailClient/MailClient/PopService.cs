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

    public class PopService
    {
        private PopConnectionSettings CurrentConfig = new PopConnectionSettings();
        private PopConnection Connection = new PopConnection();
        private ConcurrentQueue<PopCommand> CommandQueue = new ConcurrentQueue<PopCommand>();
        private BackgroundWorker ServerThread;
        private bool ShutdownRequested = false;
        private PopState State = PopState.Off;

        public EventHandler OnConnectionOpened;
        public EventHandler OnConnectionClosed;

        public void RequestStartService()
        {
            if (CurrentConfig == null) throw new Exception("no_config_provided");
            if(Connection.IsConnected()) throw new Exception("connection_exists");

            //może rzucić wyjątkiem, ale obecna metoda też powinna być używana z try-catchem
            Connection.StartConnection(CurrentConfig);

            ServerThread = new BackgroundWorker();
            ServerThread.DoWork += ServerLoop;
            ServerThread.RunWorkerAsync();

            OnConnectionOpened(this, new EventArgs());
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
            bool Success = true;
            while(!ShutdownRequested && Success)
            {
                if (CommandQueue.Count > 0)
                {
                    PopCommand Cmd = null;
                    bool Deq = CommandQueue.TryDequeue(out Cmd);
                    Success = Connection.ExecuteCommand(Cmd);
                } 
                else System.Threading.Thread.Sleep(1000);
            }

            ShutdownRequested = false;
            Connection.CloseConnection();
            Connection = null;
            OnConnectionClosed(this, new EventArgs());
        }
    }
}
