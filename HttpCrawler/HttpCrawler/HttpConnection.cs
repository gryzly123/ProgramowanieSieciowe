﻿using System;
using System.Text;
using System.Net.Sockets;
using System.Net.Security;
using System.Collections.Generic;

namespace HttpCrawler
{
    public class HttpConnection
    {
        private TcpClient NetSocket;
        private string Hostname;
        private System.IO.Stream NetStream;
        private bool ConnectionOpened = false;

        public bool IsConnected()
        {
            return NetSocket == null && ConnectionOpened;
        }

        public bool StartConnection(string TargetHostname, bool UseSsl)
        {
            //jeśli jesteśmy już połączeni, nie łączymy się jeszcze raz
            if (ConnectionOpened)
                throw new Exception("already_connected");

            Hostname = TargetHostname;

            try
            {
                NetSocket = new TcpClient(Hostname, 80);
                if(UseSsl)
                {
                    SslStream Ssl = new SslStream(NetSocket.GetStream());
                    Ssl.AuthenticateAsClient(Hostname);
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
            catch { System.Diagnostics.Debug.WriteLine("Connection was not closed gracefully."); }

            ConnectionOpened = false;
            NetSocket = null;
            return true;
        }

        public bool ExecuteRequest(string RelativePath, out List<byte> Response)
        {
            string FinalRequest = string.Format("GET {0} HTTP/1.1\r\nHost: {1}\r\n\r\n", RelativePath, Hostname);
            Response = new List<byte>();

            byte[] CommandBytes = Encoding.ASCII.GetBytes(FinalRequest);
            NetStream.Write(CommandBytes, 0, CommandBytes.Length);
            do
            {
                byte[] PartialResponse = new byte[NetSocket.ReceiveBufferSize];
                int ResponseLen = NetStream.Read(PartialResponse, 0, NetSocket.ReceiveBufferSize);

                if (ResponseLen < NetSocket.ReceiveBufferSize)
                {
                    byte[] PartialTrim = new byte[ResponseLen];
                    Array.Copy(PartialResponse, 0, PartialTrim, 0, ResponseLen);
                    Response.AddRange(PartialTrim);
                    break;
                }

                Response.AddRange(PartialResponse);
            } while (true);

            Response.TrimExcess();
            return true;
        }
    }
}
