﻿using System;
using System.Text;
using System.Net.Sockets;
using System.Net.Security;
using System.Collections.Generic;
using System.Threading;

namespace FtpClient
{
    public delegate void DebugMessaging(bool IsIncoming, string Message);

    public class NetConnection
    {
        private TcpClient NetSocket;
        private System.IO.Stream NetStream;
        private bool ConnectionOpened = false;
        public DebugMessaging OnLineSentOrReceived;

        public bool IsConnected()
        {
            return NetSocket == null && ConnectionOpened;
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
                NetSocket = new TcpClient(WithConfig.Hostname, WithConfig.Port);
                if(WithConfig.UseSsl)
                {
                    SslStream Ssl = new SslStream(NetSocket.GetStream());
                    Ssl.AuthenticateAsClient(WithConfig.Hostname);
                    NetStream = Ssl;
                }
                else NetStream = NetStream = NetSocket.GetStream();
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
                NetStream.Close();
                NetSocket.Close();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Connection was not closed gracefully.");
            }

            ConnectionOpened = false;
            NetSocket = null;
            return true;
        }

        private bool RawReadEnabled = false;
        private List<byte> RawArray;
        private Thread RawWorker;

        public bool StartRawDataRead()
        {
            if (RawReadEnabled) return false;

            RawReadEnabled = true;
            RawArray = new List<byte>();
            RawWorker = new Thread(() => RawRead());
            RawWorker.Start();
            return true;
        }

        public bool StopRawDataRead(out List<byte> ReceivedData)
        {
            if(RawWorker != null) RawWorker.Abort();
            ReceivedData = RawArray;

            if (!RawReadEnabled) return false;
            RawReadEnabled = false;
            return true;
        }

        private void RawRead()
        {
                byte[] PartialResponse = new byte[NetSocket.ReceiveBufferSize];
                int ResponseLen = NetStream.Read(PartialResponse, 0, NetSocket.ReceiveBufferSize);
                RawArray.AddRange(PartialResponse);
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
                        NetStream.Write(CommandBytes, 0, CommandBytes.Length);
                    }
                    OnLineSentOrReceived(false, Msg);
                }

                string RMsg = string.Empty;
                if(Command.ExpectsResponse())
                do
                {
                    byte[] Response = new byte[NetSocket.ReceiveBufferSize];
                    int ResponseLen = NetStream.Read(Response, 0, NetSocket.ReceiveBufferSize);
                    RMsg += Encoding.ASCII.GetString(Response, 0, ResponseLen);
                }
                while (Command.IsMultiline() && !Command.IsMultilineTerminated(RMsg));

                ErrorEncountered = !Command.ParseResponse(RMsg);
                OnLineSentOrReceived(true, RMsg);
            }

            return !ErrorEncountered;
        }
    }
}
