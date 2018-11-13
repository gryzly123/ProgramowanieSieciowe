using System;
using System.Collections.Generic;
using System.Text;

namespace FtpClient
{
    public delegate void DirectoryEvent(bool Success, FtpDirectory Directory);

    public class FcListDirectory : FtpCommand
    {
        private bool CommandSent = false;
        private bool TransferStarted = false;
        private bool TransferEnded = false;
        private bool ParseUnixListing = false;
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
            ParseUnixListing = ParentService.GetConfig().UnixListing;
            DataConnection = new NetConnection();

            if (ParseUnixListing)
            {
                DataConnection.StartConnection(ParentService.DataConfig);
                DataConnection.StartRawDataRead();
            }

            return string.Format
                ("LIST {0}{1}",
                CurrentDir.PathString(),
                EOL
                );
        }

        internal override bool ParseResponse(string Response)
        {
            if (!TransferStarted)
            {
                if (!Response.StartsWith("150"))
                {
                    TransferEnded = true;
                    OnDirectoryListed(false, CurrentDir); 
                    DataConnection.CloseConnection();
                    return true;
                }

                if (!ParseUnixListing)
                {
                    DataConnection.StartConnection(ParentService.DataConfig);
                    DataConnection.StartRawDataRead();
                }
                TransferStarted = true;
            }

            if (Response.Contains("226")) TransferEnded = true;
            if (!TransferEnded) return true;

            List<byte> Data;
            System.Threading.Thread.Sleep(50);
            DataConnection.StopRawDataRead(out Data);
            DataConnection.CloseConnection();
            
            string DirectoryListing = Encoding.ASCII.GetString(Data.ToArray());

            string[] Lines = DirectoryListing.Split(new string[] { EOL }, StringSplitOptions.RemoveEmptyEntries);
            if (!ParseUnixListing)
            {
                foreach (string Line in Lines)
                {
                    string[] Params = Line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (Params.Length < 4) continue;
                    if (Params[2].Equals("<DIR>")) CurrentDir.AddSubdirectory(Params[3]);
                    else CurrentDir.AddFile(Params[3]);
                }
            }
            else
            {
                foreach (string Line in Lines)
                {
                    string[] Params = Line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (Params.Length < 9) continue;
                    if (Params[0].StartsWith("d")) CurrentDir.AddSubdirectory(Params[8]);
                    else CurrentDir.AddFile(Params[8]);
                }
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
