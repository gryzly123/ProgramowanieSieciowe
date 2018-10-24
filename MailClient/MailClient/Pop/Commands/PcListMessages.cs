using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient
{
    public delegate void NewMessagesReceived(Dictionary<int, string> NewMessages);

    public class PcListMessages : PopCommand
    {
        private bool CommandSent = false;
        private MailDirectory CurrentDir;
        private Dictionary<int, string> NewMessages = new Dictionary<int, string>();

        public NewMessagesReceived OnNewMessagesReceived;

        public PcListMessages(MailDirectory TargetDir)
        {
            CurrentDir = TargetDir;
        }

        internal override string BuildVerb()
        {
            CommandSent = true;
            return "UIDL " + EOL;
        }

        internal override bool ParseResponse(string Response)
        {
            Response = Response.Remove(Response.Length - MultilineTerminator.Length - 1);
            string[] Lines = Response.Split(new string[] { EOL }, StringSplitOptions.None);
            int LineCount = Lines.Length;

            if (!Lines[0].StartsWith(OK)) return false;

            for(int i = 1; i < LineCount; ++i)
            {
                string[] Verbs = Lines[i].Split(new string[] { Whitespace }, StringSplitOptions.None);

                int It = int.Parse(Verbs[0]);
                string Uid = Verbs[1];

                //szukam wiadomości w folderze, pomijam dodanie jej
                //do listy ściąganych jeśli już istnieje i jest pobrana
                MailMessage Msg = CurrentDir.GetMessage(Uid);
                if (Msg == null)
                {
                    Msg = new MailMessage();
                    CurrentDir.AddMessage(Uid, Msg);
                }
                else if (Msg.PopReceived) continue;
                
                Msg.PopUid = Uid;
                NewMessages[It] = Uid;
            }

            ParentService.State = PopState.Update;
            OnNewMessagesReceived(NewMessages);
            return true;
        }

        internal override int VerbsLeft()
        {
            return (!CommandSent && ParentService.State == PopState.Transaction) ? 1 : 0;
        }

        internal override bool IsMultiline()
        {
            return false;
        }
    }
}
