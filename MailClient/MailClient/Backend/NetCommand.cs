namespace MailClient
{
    public abstract class NetCommand
    {
        internal abstract string BuildVerb();
        internal abstract bool ParseResponse(string Response);
        internal abstract int VerbsLeft();
        internal abstract bool OmmitVerb();
        internal abstract bool IsMultiline();
        internal virtual bool ExpectsResponse() { return true; }
        internal abstract string GetMultilineTerminator();
    }

    public abstract class PopCommand : NetCommand
    {
        protected const string EOL = "\r\n";
        protected const string OK = "+OK";
        protected const string ERROR = "-ERR";
        internal const string MultilineTerminator = "\r\n.\r\n";
        internal const string Whitespace = " ";

        protected PopService ParentService;
        internal void SetPopService(PopService InService) { ParentService = InService; }
        internal override string GetMultilineTerminator() { return MultilineTerminator; }
        internal override bool OmmitVerb() { return false; }
    }

    public abstract class SmtpCommand : NetCommand
    {
        protected const string OK = "250 OK";
        internal const string Whitespace = " ";

        protected SmtpService ParentService;
        internal void SetSmtpService(SmtpService InService) { ParentService = InService; }
        internal override string GetMultilineTerminator() { return ""; }
        internal override bool OmmitVerb() { return false; }
    }
}
