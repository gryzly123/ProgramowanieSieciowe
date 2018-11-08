using System;
using System.Collections.Generic;

namespace FtpClient
{
    public delegate void NewMessagesReceived(Dictionary<int, string> NewMessages);

    public class FcListDirectory : FtpCommand
    {
        private bool CommandSent = false;
        private MailDirectory CurrentDir;
        private Dictionary<int, string> NewMessages = new Dictionary<int, string>();

        public NewMessagesReceived OnNewMessagesReceived;

        public FcListDirectory(MailDirectory TargetDir)
        {
            CurrentDir = TargetDir;
        }

        internal override string BuildVerb()
        {
            CommandSent = true;
            return "LIST " + EOL;
        }

        internal override bool ParseResponse(string Response)
        {
            System.Windows.Forms.MessageBox.Show(Response);
            //Response = Response.Remove(Response.Length - MultilineTerminator.Length);
            //string[] Lines = Response.Split(new string[] { EOL }, StringSplitOptions.None);
            //int LineCount = Lines.Length;
            //
            //if (!Lines[0].StartsWith(OK)) return false;
            //for(int i = 1; i < LineCount; ++i)
            //{
            //    string[] Verbs = Lines[i].Split(new string[] { Whitespace }, StringSplitOptions.None);
            //    int It = 0;
            //    
            //    //Poczta WP odsyła dwie okejki, więc spradzam czy pominąć linijkę
            //    if (!int.TryParse(Verbs[0], out It)) continue;
            //    string Uid = Verbs[1];
            //
            //    //szukam wiadomości w folderze, pomijam dodanie jej
            //    //do listy ściąganych jeśli już istnieje i jest pobrana
            //    MailMessage Msg = CurrentDir.GetMessage(Uid);
            //    if (Msg == null)
            //    {
            //        Msg = new MailMessage();
            //        CurrentDir.AddMessage(Uid, Msg);
            //    }
            //    
            //    Msg.PopUid = Uid;
            //    if (!Msg.PopReceived) NewMessages[It] = Uid;
            //}
            //
            //OnNewMessagesReceived(NewMessages);
            //return true;
            return false;
        }

        internal override int VerbsLeft()
        {
            return CommandSent ? 1 : 0;
        }

        internal override bool IsMultiline()
        {
            return true;
        }
    }
}
