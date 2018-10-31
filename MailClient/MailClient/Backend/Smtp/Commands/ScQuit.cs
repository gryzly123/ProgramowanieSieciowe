namespace MailClient
{
    public class ScQuit : SmtpCommand
    {
        private bool CommandSent = false;
        private bool QuitHandshakeReceived = false;

        public NetEvent OnServerQuit;

        //ta komenda nie wysyła nic do serwera, jedynie oczekuje odpowiedzi
        internal override string BuildVerb() { CommandSent = true; return "QUIT" + EOL; }
        internal override int VerbsLeft() { return CommandSent ? 0 : 1; }
        internal override bool IsMultiline() { return true; }
        internal override bool ParseResponse(string Response)
        {
            bool Success = Response.StartsWith("221");
            QuitHandshakeReceived = Success;
            OnServerQuit();
            return Success;
        }
    }
}
