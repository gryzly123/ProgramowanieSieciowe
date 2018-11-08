namespace FtpClient
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

    public abstract class FtpCommand : NetCommand
    {
        //protected const string OK = "+OK";
        //protected const string ERROR = "-ERR";
        //internal const string MultilineTerminator = "\r\n.\r\n";
        //internal const string Whitespace = " ";

        protected FtpService ParentService;
        internal void SetFtpService(FtpService InService) { ParentService = InService; }
        internal override bool IsMultilineTerminated(string Msg) { return false; }// Msg.EndsWith(MultilineTerminator); }
        internal override bool OmmitVerb() { return false; }
    }
}
