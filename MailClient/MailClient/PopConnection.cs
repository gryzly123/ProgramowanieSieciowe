using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;

namespace MailClient
{
    class PopConnection
    {
        TcpClient TcpConnection;
        bool IsConnected = false;
        BackgroundWorker TcpAsync;
        List<string> MessageQueue;

        bool StartConnection(PopConnectionSettings WithConfig, ref string FailReason)
        {
            //jeśli jesteśmy już połączeni, nie łączymy się jeszcze raz
            if (IsConnected)
            {
                FailReason = "already_connected";
                return false;
            }

            //  //pobieramy adres IP
            //  IPAddress Ip;
            //  try { Ip = IPAddress.Parse(WithConfig.Hostname); }
            //  catch(Exception E)
            //  {
            //      //jeśli hostname nie jest adresem IP, musimy go rozwiązać
            //      IPAddress[] ResolvedHostname = Dns.GetHostAddresses(WithConfig.Hostname);
            //      if(ResolvedHostname.Length == 0)
            //      {
            //          FailReason = "couldnt_resolve_hostname " + WithConfig.Hostname;
            //          return false;
            //      }
            //      Ip = ResolvedHostname[0];
            //  }
            //  
            //  //tworzymy obiekt połączenia TCP
            //  try { TcpConnection = new TcpClient(Ip.ToString(), WithConfig.Port); }
            //  catch(Exception E)
            //  {
            //      FailReason = "tcp_init_failed // " + E.ToString();
            //      return false;
            //  }

            try { TcpConnection.ConnectAsync(WithConfig.Hostname, WithConfig.Port); }
            catch (Exception E)
            {
                FailReason = "tcp_init_failed" + E.ToString();
                return false;
            }
            

            return false;
        }


    }
}
