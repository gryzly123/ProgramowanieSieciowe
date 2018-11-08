using System;

namespace FtpClient
{
    public class FcQuit : FtpCommand
    {
        bool QuitSent = false;

        public FcQuit() { }

        internal override string BuildVerb()
        {
            QuitSent = true;
            return "QUIT " + EOL;
        }

        internal override bool ParseResponse(string Response)
        {
            System.Windows.Forms.MessageBox.Show(Response);
            return false;
        }

        internal override int VerbsLeft()
        {
            return QuitSent ? 0 : 1;
        }

        internal override bool IsMultiline()
        {
            return false;
        }

        internal override bool ExpectsResponse()
        {
            return true;
        }

    }
}
