using System;

namespace FtpClient
{
    public class FcHandshake : FtpCommand
    {
        bool HandshakeReceived = false;

        public FcHandshake() { }

        internal override string BuildVerb()
        {
            return EOL;
        }

        internal override bool ParseResponse(string Response)
        {
            System.Windows.Forms.MessageBox.Show(Response);
            return false;
        }

        internal override int VerbsLeft()
        {
            return HandshakeReceived ? 0 : 1;
        }

        internal override bool OmmitVerb()
        {
            return true;
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
