namespace MailClient
{
    public abstract class NetCommand
    {
        protected const string EOL = "\r\n";

        internal abstract string BuildVerb();
        internal abstract bool ParseResponse(string Response);
        internal abstract int VerbsLeft();
        internal abstract bool OmmitVerb();
        internal abstract bool IsMultiline();
        internal virtual bool ExpectsResponse() { return true; }
        internal abstract bool  IsMultilineTerminated(string Msg);
    }

    public abstract class PopCommand : NetCommand
    {
        protected const string OK = "+OK";
        protected const string ERROR = "-ERR";
        internal const string MultilineTerminator = "\r\n.\r\n";
        internal const string Whitespace = " ";

        protected PopService ParentService;
        internal void SetPopService(PopService InService) { ParentService = InService; }
        internal override bool IsMultilineTerminated(string Msg) { return Msg.EndsWith(MultilineTerminator); }
        internal override bool OmmitVerb() { return false; }
    }

    public abstract class SmtpCommand : NetCommand
    {
        protected const string OK = "250 OK";
        internal const string Whitespace = " ";

        protected SmtpService ParentService;
        internal void SetSmtpService(SmtpService InService) { ParentService = InService; }
        internal override bool IsMultilineTerminated(string Msg)
        {
            string[] Splits = Msg.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
            if (Splits.Length == 0) return false;
            if (Splits[Splits.Length - 1].Length < 4) return false;
            return Splits[Splits.Length - 1][3] == ' ';
        }
        internal override bool OmmitVerb() { return false; }
    }
}
