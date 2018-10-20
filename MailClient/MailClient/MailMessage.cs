using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient
{
    public class MailStorage
    {
        public List<MailMessage> Messages = new List<MailMessage>();
    }


    public class MailMessage
    {
        public string   From;
        public string[] To;
        public string   Topic;
        public string   Message;
        public string   PopUid;
        public bool     PopReceived;
        public bool     Deleted;
        public bool     IsOutMessage;

        public EventHandler OnMessageSent;
        public EventHandler OnMessageReceived;
    }
}
