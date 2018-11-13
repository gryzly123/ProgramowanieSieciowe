namespace FtpClient
{
    public class FcChangeDirectory : FtpCommand
    {
        private FtpDirectory CurrentDir;
        private FtpDirectory TargetDir;
        private bool CommandSent = false;

        public DirectoryEvent OnDirectoryChanged;

        public FcChangeDirectory(FtpDirectory CurrentDir, FtpDirectory TargetDir)
        {
            this.CurrentDir = TargetDir;
            this.TargetDir = TargetDir;
        }

        internal override string BuildVerb()
        {
            CommandSent = true;
            return string.Format("CWD {0}{1}", TargetDir.PathString(), EOL);
        }

        internal override bool ParseResponse(string Response)
        {
            bool Success = Response.StartsWith("250");
            OnDirectoryChanged(Success, Success ? TargetDir : CurrentDir);
            return true;
        }

        internal override int VerbsLeft()
        {
            return CommandSent ? 0 : 1;
        }

        internal override bool IsMultiline()
        {
            return true;
        }
    }
}
