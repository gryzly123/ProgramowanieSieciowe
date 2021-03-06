﻿using System;

namespace MailClient
{
    public class PcFetchMessage : PopCommand
    {
        private MailDirectory CurrentDir;
        private int    CurrentId;
        private string CurrentUid;

        private bool CommandSent = false;

        public PcFetchMessage(MailDirectory TargetDir, int TargetId, string TargetUid)
        {
            CurrentDir = TargetDir;
            CurrentId = TargetId;
            CurrentUid = TargetUid;
        }

        internal override string BuildVerb()
        {
            CommandSent = true;
            return "RETR" + Whitespace + CurrentId.ToString() + EOL;
        }

        internal override bool ParseResponse(string Response)
        {
            Response = Response.Remove(Response.Length - MultilineTerminator.Length);
            string[] Lines = Response.Split(new string[] { EOL }, StringSplitOptions.None);
            int LineCount = Lines.Length;

            if (LineCount == 0) return false;
            if (!Lines[0].StartsWith(OK)) return false;

            MailMessage CurrentMsg = CurrentDir.GetMessage(CurrentUid);
            if (CurrentMsg == null) return false;
            if (CurrentMsg.PopReceived) return true;

            for (int i = 1; i < LineCount; ++i) CurrentMsg.Message += Lines[i] + EOL;
            CurrentMsg.PopReceived = true;
            CurrentMsg.OnMessageUpdated(CurrentMsg, new EventArgs());
            return true;
        }

        internal override int VerbsLeft()
        {
            return (!CommandSent && ParentService.State == PopState.Transaction) ? 1 : 0;
        }

        internal override bool IsMultiline()
        {
            return true;
        }
    }
}
