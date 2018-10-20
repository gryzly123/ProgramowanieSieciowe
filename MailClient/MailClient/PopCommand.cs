using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient
{
    public abstract class PopCommand
    {
        protected const string EOL = "\r\n";
        protected const string OK = "+OK ";
        protected const string ERROR = "-ERR";
        internal  const string MultilineTerminator = "\r\n.\r\n";

        protected PopService ParentService;
        internal void SetPopService(PopService InService) { ParentService = InService; }

        internal abstract string BuildVerb();
        internal abstract bool ParseResponse(string Response);
        internal abstract int VerbsLeft();
        internal abstract bool IsMultiline();
    }

    public class PcAuthorize : PopCommand
    {
        enum AuthorizationState
        {
            NotStarted,
            SentLogin,
            AcceptedLogin,
            SentPassword,
            AcceptedPassword,
            Failed
        } private AuthorizationState AuthState;

        public EventHandler OnUserLoginSuccess;
        public EventHandler OnUserLoginFailed;
        PopConnectionSettings Creds;

        public PcAuthorize()
        {
            AuthState = AuthorizationState.NotStarted;
        }

        internal override string BuildVerb()
        {
            Creds = ParentService.GetConfig();

            switch (AuthState)
            {
                case AuthorizationState.NotStarted:
                    AuthState = AuthorizationState.SentLogin;
                    return "USER " + Creds.UserLogin + EOL;

                case AuthorizationState.AcceptedLogin:
                    AuthState = AuthorizationState.SentPassword;
                    return "PASS " + Creds.UserPassword + EOL;
                default:
                    throw new Exception("no_verbs_left");
            }
        }

        internal override bool ParseResponse(string Response)
        {
            if(!Response.StartsWith(OK))
            {
                AuthState = AuthorizationState.Failed;
            }

            switch(AuthState)
            {
                case AuthorizationState.SentLogin:
                    AuthState = AuthorizationState.AcceptedLogin;
                    return true;

                case AuthorizationState.SentPassword:
                    AuthState = AuthorizationState.AcceptedPassword;
                    ParentService.State = PopState.Transaction;
                    return true;
            }

            return false;
        }

        internal override int VerbsLeft()
        {
            //nie zaczynamy autoryzacji jeśli serwer tego nie oczekuje
            if (ParentService.State != PopState.Authorization) return 0;

            switch (AuthState)
            {
                case AuthorizationState.NotStarted: return 2;

                case AuthorizationState.AcceptedLogin:
                case AuthorizationState.SentLogin:  return 1;

                default:                            return 0;
            }
        }

        internal override bool IsMultiline()
        {
            return false;
        }
    }
    
    public class PcListMessages : PopCommand
    {
        private bool MessageSent = false;
        private bool ResponseTerminated = false;

        internal override string BuildVerb()
        {
            return "UIDL " + EOL;
        }

        internal override bool ParseResponse(string Response)
        {
            
            return false;
        }

        internal override int VerbsLeft()
        {
            return MessageSent ? 0 : 1;
        }

        internal override bool IsMultiline()
        {
            return true;
        }
    }
}
