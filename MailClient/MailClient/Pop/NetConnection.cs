using System;
using System.Text;
using System.Net.Sockets;
using System.Net.Security;

namespace MailClient
{
    public delegate void DebugMessaging(bool IsIncoming, string Message);

    public class NetConnection
    {
        private TcpClient PopSocket;
        private System.IO.Stream PopStream;
        private bool ConnectionOpened = false;
        public DebugMessaging OnLineSentOrReceived;

        public bool IsConnected()
        {
            return PopSocket == null && ConnectionOpened;
        }

        public bool StartConnection(NetConnectionSettings WithConfig)
        {
            //jeśli jesteśmy już połączeni, nie łączymy się jeszcze raz
            if (ConnectionOpened)
            {
                throw new Exception("already_connected");
            }

            try
            {
                PopSocket = new TcpClient(WithConfig.Hostname, WithConfig.Port);
                if(WithConfig.UseSsl)
                {
                    SslStream Ssl = new SslStream(PopSocket.GetStream());
                    Ssl.AuthenticateAsClient(WithConfig.Hostname);
                    PopStream = Ssl;
                }
                else PopStream = PopStream = PopSocket.GetStream();
            }
            catch (Exception e)
            {
                throw new Exception("connection_init_error: " + e.ToString());
            }

            return true;
        }

        public bool CloseConnection()
        {
            if (!IsConnected()) return true;
            try
            {
                PopStream.Close();
                PopSocket.Close();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Connection was not closed gracefully.");
            }

            ConnectionOpened = false;
            PopSocket = null;
            return true;
        }

        public bool ExecuteCommand(NetCommand Command)
        {
            bool ErrorEncountered = false;
            while (Command.VerbsLeft() > 0 && !ErrorEncountered)
            {
                if (!Command.OmmitVerb())
                {
                    string Msg = Command.BuildVerb();
                    {
                        byte[] CommandBytes = Encoding.ASCII.GetBytes(Msg);
                        PopStream.Write(CommandBytes, 0, CommandBytes.Length);
                    }
                    OnLineSentOrReceived(false, Msg);
                }

                string RMsg = string.Empty;
                if(Command.ExpectsResponse())
                do
                {
                    byte[] Response = new byte[PopSocket.ReceiveBufferSize];
                    int ResponseLen = PopStream.Read(Response, 0, PopSocket.ReceiveBufferSize);
                    RMsg += Encoding.ASCII.GetString(Response, 0, ResponseLen);
                }
                while (Command.IsMultiline() && !RMsg.EndsWith(PopCommand.MultilineTerminator));

                ErrorEncountered = !Command.ParseResponse(RMsg);
                OnLineSentOrReceived(true, RMsg);
            }

            return !ErrorEncountered;
        }
    }
}
