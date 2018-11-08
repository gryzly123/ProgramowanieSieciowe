using System;

namespace FtpClient
{
    public class FcHandshake : FtpCommand
    {
        bool HandshakeReceived = false;
        public NetEvent OnHandshakeReceived;

        public FcHandshake() { }

        internal override string BuildVerb()
        {
            return EOL;
        }

        internal override bool ParseResponse(string Response)
        {
            System.Windows.Forms.MessageBox.Show(Response);
            string[] Lines = Response.Split(new string[] { EOL }, StringSplitOptions.RemoveEmptyEntries);
            if (Lines.Length == 0) return false;
            HandshakeReceived = Lines[Lines.Length - 1].StartsWith("220");

            if(HandshakeReceived) OnHandshakeReceived();
            return HandshakeReceived;
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
