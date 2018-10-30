using System;

namespace MailClient
{
    public class ScHello : SmtpCommand
    {
        private bool CommandSent = false;
        private bool InitHandshakeReceived = false;

        public NetEventRetVal OnHandshakeReceived;

        //ta komenda nie wysyła nic do serwera, jedynie oczekuje odpowiedzi
        internal override string BuildVerb() { CommandSent = true; return "EHLO " + EOL; }
        internal override bool OmmitVerb()
        {
            return !InitHandshakeReceived;
        }
        internal override int VerbsLeft() { return CommandSent ? 0 : 1; }
        internal override bool IsMultiline() { return true; }

        internal override bool ParseResponse(string Response)
        {
            if (!InitHandshakeReceived)
            {
                bool Success = Response.StartsWith("220");
                InitHandshakeReceived = Success;
                return Success;
            }
            else
            {
                bool Success = Response.StartsWith("250");
                OnHandshakeReceived(Success);
                return Success;
            }
        }
    }
}
