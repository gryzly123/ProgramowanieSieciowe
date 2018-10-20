﻿using System;
using System.Collections.Generic;
using System.IO;
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
          public string Message;
        //public string   From;
        //public string[] To;
        //public string   Topic;
        //public string   Message;
        //public DateTime Received;
        //public bool     Deleted;

        public string   PopUid;
        public bool     PopReceived;
        public bool     IsOutMessage;

        public EventHandler OnMessageSent;
        public EventHandler OnMessageUpdated;
        public EventHandler OnMessageReceived;
    }

    public delegate void MessageEvent(MailDirectory AtDirectory, MailMessage AtMessage);

    public class MailDirectory
    {
        private string DirName = "";
        public Dictionary<string, MailMessage> Messages = new Dictionary<string, MailMessage>();

        public MessageEvent OnMessageReceived;
        public MessageEvent OnMessageUpdated;
        public MessageEvent OnMessageDeleted;

        public MailDirectory(string Name)
        {
            DirName = Name;
        }

        public bool AddMessage(string PopUid, MailMessage Message)
        {
            Messages.TryGetValue(PopUid, out MailMessage Msg);
            if (Msg != null) return false;

            if (Message == null) return false;

            Messages.Add(PopUid, Message);
            Message.OnMessageReceived += HandleChangeInDirectoryByReceive;
            Message.OnMessageSent     += HandleChangeInDirectoryBySent;
            Message.OnMessageUpdated  += HandleChangeInDirectoryByUpdated;
            return true;
        }

        private void HandleChangeInDirectoryByReceive(object sender, EventArgs e)
        {
            OnMessageReceived(this, (MailMessage)sender);
        }

        private void HandleChangeInDirectoryBySent(object sender, EventArgs e)
        {
            OnMessageReceived(this, (MailMessage)sender);
        }

        private void HandleChangeInDirectoryByUpdated(object sender, EventArgs e)
        {
            OnMessageReceived(this, (MailMessage)sender);
        }

        public MailMessage GetMessage(string PopUid)
        {
            Messages.TryGetValue(PopUid, out MailMessage Msg);
            return Msg;
        }

        public List<string> GetAllMessagesByUid()
        {
            List<string> Result = new List<string>();
            foreach (KeyValuePair<string, MailMessage> MsgEntry in Messages)
                Result.Add(MsgEntry.Key);
            return Result;
        }

        public void ReadFromFile(StreamReader FromFile) { throw new NotImplementedException(); }
        public void SaveToFile(StreamWriter OutFile)    { throw new NotImplementedException(); }

    }
}
