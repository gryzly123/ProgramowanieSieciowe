using System;
using System.Collections.Generic;
using System.Text;

namespace FtpClient
{
    public delegate void DirectoryEvent(bool Success, FtpDirectory Directory);

    public class FcListDirectory : FtpCommand
    {
        private bool CommandSent = false;
        private bool TransferEnded = false;
        private FtpDirectory CurrentDir;
        public DirectoryEvent OnDirectoryListed;
        NetConnection DataConnection;

        public FcListDirectory(FtpDirectory TargetDir)
        {
            CurrentDir = TargetDir;
        }

        internal override bool OmmitVerb() { return CommandSent && !TransferEnded; }

        internal override string BuildVerb()
        {
            CommandSent = true;
            return string.Format
                ("LIST {0}{1}",
                CurrentDir.PathString(),
                EOL
                );
        }

        internal override bool ParseResponse(string Response)
        {
            //Komenda 1 - rozpoczęcie
            if (DataConnection == null)
            {
                if (!Response.StartsWith("150"))
                {
                    TransferEnded = true;
                    OnDirectoryListed(false, CurrentDir);
                    return true;
                }

                DataConnection = new NetConnection();
                DataConnection.StartConnection(ParentService.DataConfig);
                DataConnection.StartRawDataRead();
                return true;
            }

            //Komenda 2 - zakończenie
            TransferEnded = true;

            if (!Response.StartsWith("226"))
            {
                OnDirectoryListed(false, null);
                return true;
            }

            List<byte> Data;
            DataConnection.StopRawDataRead(out Data);
            string DirectoryListing = Encoding.ASCII.GetString(Data.ToArray());

            string[] Lines = DirectoryListing.Split(new string[] { EOL }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string Line in Lines)
            {
                string[] Params = Line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (Params.Length < 4) continue;
                if (Params[2].Equals("<DIR>")) CurrentDir.AddSubdirectory(Params[3]);
                else CurrentDir.AddFile(Params[3]);
            }
            OnDirectoryListed(true, CurrentDir);

            return true;
        }

        internal override int VerbsLeft()
        {
            return CommandSent ? (TransferEnded ? 0 : 1) : 2;
        }

        internal override bool IsMultiline()
        {
            return true;
        }
    }
}
