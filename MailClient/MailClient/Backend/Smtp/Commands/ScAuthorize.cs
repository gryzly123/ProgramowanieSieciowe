using System;

namespace MailClient
{
    public class ScAuthorize : SmtpCommand
    {
        enum State
        {
            NotStarted,
            SentAuthType,
            AcceptedAuthType,
            SentB64,
            AcceptedB64,
            Failed
        }

        public NetEventRetVal OnUserLogin;
        private State AuthState;
        SmtpConnectionSettings Creds = new SmtpConnectionSettings();
        public ScAuthorize() { }

        internal override string BuildVerb()
        {
            Creds.CloneFrom(ParentService.GetConfig());

            switch (AuthState)
            {
                case State.NotStarted:
                    AuthState = State.SentAuthType;
                    return "AUTH PLAIN " + EOL;

                case State.AcceptedAuthType:
                    AuthState = State.SentB64;
                    return System.Convert.ToBase64String(
                        System.Text.Encoding.ASCII.GetBytes
                            ("\0" + Creds.UserLogin + "\0" + Creds.UserPassword))
                        + EOL;

                default:
                    throw new Exception("no_verbs_left");
            }
        }
        internal override int VerbsLeft()
        {
            switch (AuthState)
            {
                case State.NotStarted: return 2;
                case State.AcceptedAuthType: return 1;
                default: return 0;
            }
        }

        internal override bool IsMultiline() { return true; }

        internal override bool ParseResponse(string Response)
        {
            bool Success = false;
            switch (AuthState)
            {
                case State.SentAuthType:
                    Success = Response.StartsWith("334");
                    if (Success) AuthState = State.AcceptedAuthType;
                    return Success;
                case State.SentB64:
                    Success = Response.StartsWith("235");
                    if (Success) AuthState = State.AcceptedB64;
                    return Success;

                default: return false;
            }
        }
    }
}
