using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Net.Security;

namespace MailClient
{
    public delegate void DebugMessaging(bool IsIncoming, string Message);

    public class PopConnection
    {
        private TcpClient PopSocket;
        private System.IO.Stream PopStream;
        private bool ConnectionOpened = false;
        public DebugMessaging OnLineSentOrReceived;// = new DebugMessaging();

        public bool IsConnected()
        {
            return PopSocket == null && ConnectionOpened;
        }

        public bool StartConnection(PopConnectionSettings WithConfig)
        {
            //jeśli jesteśmy już połączeni, nie łączymy się jeszcze raz
            if (ConnectionOpened)
            {
                throw new Exception("already_connected");
            }

            try
            {
                PopSocket = new TcpClient(WithConfig.Hostname, WithConfig.Port);
                //PopSocket.Connect(EndPoint);
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
            catch (Exception e)
            {
                MessageBox.Show("Connection was not closed gracefully.");
            }

            ConnectionOpened = false;
            PopSocket = null;
            return true;
        }

        public bool ExecuteCommand(PopCommand Command)
        {
            bool ErrorEncountered = false;
            while (Command.VerbsLeft() > 0 && !ErrorEncountered)
            {
                string Msg = Command.BuildVerb();
                {
                    byte[] CommandBytes = Encoding.ASCII.GetBytes(Msg);
                    PopStream.Write(CommandBytes, 0, CommandBytes.Length);
                }
                OnLineSentOrReceived(false, Msg);
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
