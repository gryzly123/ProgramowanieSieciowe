using System;

namespace MailClient
{
    public class PcQuit : PopCommand
    {
        public PcQuit() { }

        internal override string BuildVerb()
        {
            return "QUIT " + EOL;
        }

        internal override bool ParseResponse(string Response)
        {
            if (Response.StartsWith(OK) || Response == string.Empty)
            {
                switch (ParentService.State)
                {
                    case PopState.Transaction:
                        ParentService.State = PopState.Update;
                        break;

                    default:
                        ParentService.State = PopState.Off;
                        ParentService.RequestStopService();
                        break;
                }
                return true;
            }
            return false;
        }

        internal override int VerbsLeft()
        {
            switch (ParentService.State)
            {
                case PopState.Authorization:
                    return 1;

                case PopState.Transaction:
                    return 2;

                case PopState.Update:
                    return 1;

                default:
                    return 0;
            }
        }

        internal override bool IsMultiline()
        {
            return false;
        }

        internal override bool ExpectsResponse()
        {
            return (ParentService.State == PopState.Transaction);
        }

    }
}
