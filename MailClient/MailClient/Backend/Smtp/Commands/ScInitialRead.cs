using System;

namespace MailClient
{
    public class ScInitialRead : SmtpCommand
    {
        //ta komenda nie wysyła nic do serwera, jedynie oczekuje odpowiedzi
        internal override bool OmmitVerb() { return true; }
        internal override string BuildVerb() { throw new NotImplementedException(); }
        internal override int VerbsLeft() { return 0; }

        internal override bool IsMultiline()
        {
            return false;
        }

        internal override bool ParseResponse(string Response)
        {
            return false;
        }
    }
}
