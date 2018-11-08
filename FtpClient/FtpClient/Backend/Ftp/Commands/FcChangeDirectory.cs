using System;

namespace FtpClient
{
    public class FcChangeDirectory : FtpCommand
    {
        private MailDirectory CurrentDir;
        private int    CurrentId;
        private string CurrentUid;

        private bool CommandSent = false;

        public FcChangeDirectory(MailDirectory TargetDir, int TargetId, string TargetUid)
        {
            CurrentDir = TargetDir;
            CurrentId = TargetId;
            CurrentUid = TargetUid;
        }

        internal override string BuildVerb()
        {
            CommandSent = true;
            return "RETR" + EOL;
        }

        internal override bool ParseResponse(string Response)
        {
            System.Windows.Forms.MessageBox.Show(Response);
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
