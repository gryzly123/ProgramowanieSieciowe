using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MailClient
{
    public class PopConnection
    {
        TcpClient TcpConnection;
        bool IsConnected = false;
        BackgroundWorker TcpAsync;
        List<string> MessageQueue;

        public bool StartConnection(PopConnectionSettings WithConfig, ref string FailReason)
        {
            //jeśli jesteśmy już połączeni, nie łączymy się jeszcze raz
            if (IsConnected)
            {
                FailReason = "already_connected";
                return false;
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
                    FailReason = "couldnt_resolve_hostname " + WithConfig.Hostname;
                    return false;
                }
                Ip = ResolvedHostname[0];
            }

            //tworzymy endpointa i socketa
            IPEndPoint EndPoint = new IPEndPoint(Ip, WithConfig.Port);
            Socket SendSocket = new Socket(Ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                SendSocket.Connect(EndPoint);
                string TestMessage = "USER " + WithConfig.UserLogin + "\r\n";

                SendSocket.Send(Encoding.ASCII.GetBytes(TestMessage));
                SendSocket.Send(Encoding.ASCII.GetBytes(TestMessage));

                System.Threading.Thread.Sleep(1000);

                byte[] Response = new byte[1024];
                SendSocket.Receive(Response);
                FailReason = (Encoding.ASCII.GetString(Response));
            }
            catch (Exception e)
            {
                FailReason = ("Connection error: " + e.ToString());
                return false;
            }

            return true;
        }


    }
}
