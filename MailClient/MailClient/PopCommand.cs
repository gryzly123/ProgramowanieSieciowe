using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient
{
    //public delegate bool MessageParsed(PopCommand Self);

    public abstract class PopCommand
    {
        public const string EOL = "\r\n";
        public const string OK = "+OK ";
        public const string ERROR = "+ERR";
        public abstract string BuildVerb();
        public abstract bool ParseResponse(string Response);
        public abstract int VerbsLeft();
        //public MessageParsed OnMessageParsed;
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

        public PcAuthorize(PopConnectionSettings Config)
        {
            Creds = Config;
            AuthState = AuthorizationState.NotStarted;
        }

        public override string BuildVerb()
        {
            switch(AuthState)
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

        public override bool ParseResponse(string Response)
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
                    
                    return true;
            }

            return false;
        }

        public override int VerbsLeft()
        {
            switch (AuthState)
            {
                case AuthorizationState.NotStarted: return 2;

                case AuthorizationState.AcceptedLogin:
                case AuthorizationState.SentLogin:  return 1;

                default:                            return 0;
            }
        }
    }

}
