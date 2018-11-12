using System;
using System.Net;

namespace FtpClient
{
    public class FcModeToggle : FtpCommand
    {
        bool Success = false;
        UInt16 TargetPort;
        string TargetHost;

        internal FtpService.Mode NewMode = FtpService.Mode.PassiveV6;
        public NetEvent OnModeSwitched;

        internal override string BuildVerb()
        {
            switch(NewMode)
            {
                case FtpService.Mode.PassiveV4:
                    return "PASV" + EOL;
                case FtpService.Mode.PassiveV6:
                    return "EPSV" + EOL;
            }
            throw new Exception();
        }

        internal override bool ParseResponse(string Response)
        {
            Success = Response.StartsWith("229");
            if(!Success)
                switch(NewMode)
                {
                    case FtpService.Mode.PassiveV4:
                        NewMode = FtpService.Mode.None;
                        return true;

                    case FtpService.Mode.PassiveV6:
                        NewMode = FtpService.Mode.PassiveV4;
                        return true;

                    default: return false;
                }
            else
            {
                string IpData = Response.Split('(', ')')[1];
                switch (NewMode)
                {
                    case FtpService.Mode.PassiveV4:
                        throw new NotImplementedException();

                    case FtpService.Mode.PassiveV6:
                        string Port = IpData.Split(new char[] { '|' }, StringSplitOptions.None)[3];
                        if (!UInt16.TryParse(Port, out TargetPort)) return false;
                        TargetHost = ParentService.GetConfig().Hostname;
                        break;
                }
            }

            FtpConnectionSettings DataSettings = new FtpConnectionSettings();
            DataSettings.Hostname = TargetHost;
            DataSettings.Port = TargetPort;
            DataSettings.UseSsl = false;
            ParentService.DataConfig = DataSettings;
            return true;
        }

        internal override int VerbsLeft()
        {
            return (Success || NewMode == FtpService.Mode.None) ? 0 : 1;
        }

        internal override bool IsMultiline()
        {
            return false;
        }
    }
}
