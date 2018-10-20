using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MailClient
{
    public delegate void DebugMessaging(bool IsIncoming, string Message);

    public class PopConnection
    {
        private Socket PopSocket;
        private bool ConnectionOpened = false;
        private bool ConnectionActive = false;
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

            //pobieramy adres IP
            IPAddress Ip;
            try { Ip = IPAddress.Parse(WithConfig.Hostname); }
            catch(Exception E)
            {
                //jeśli hostname nie jest adresem IP, musimy go rozwiązać
                IPAddress[] ResolvedHostname = Dns.GetHostAddresses(WithConfig.Hostname);
                if(ResolvedHostname.Length == 0)
                {
                    throw new Exception("couldnt_resolve_hostname");
                }
                Ip = ResolvedHostname[0];
            }

            //tworzymy endpointa i socketa
            IPEndPoint EndPoint = new IPEndPoint(Ip, WithConfig.Port);

            try
            {
                PopSocket = new Socket(Ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                PopSocket.Connect(EndPoint);
                
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
                PopSocket.Shutdown(SocketShutdown.Both);
                PopSocket.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Connection was not closed gracefully.");
            }

            ConnectionOpened = false;
            ConnectionActive = false;
            PopSocket = null;
            return true;
        }

        public bool ExecuteCommand(PopCommand Command)
        {
            bool Success = true;
            while (Command.VerbsLeft() > 0 && Success)
            {
                string Msg = Command.BuildVerb();
                PopSocket.Send(Encoding.ASCII.GetBytes(Msg));
                OnLineSentOrReceived(false, Msg);

                Msg = string.Empty;

                do
                {
                    byte[] Response = new byte[PopSocket.ReceiveBufferSize];
                    int ResponseLen = PopSocket.Receive(Response);
                    Msg += Encoding.ASCII.GetString(Response, 0, ResponseLen);
                }
                while (Command.IsMultiline() && !Msg.EndsWith(PopCommand.MultilineTerminator));

                Success = Command.ParseResponse(Msg);
                OnLineSentOrReceived(true, Msg);
            }

            return Success;
        }
    }
}
